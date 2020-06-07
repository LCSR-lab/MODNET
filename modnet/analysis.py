import os
from pathlib import Path
from .constants import (
    Files,
    Errors,
    ModuleCst,
    Templates,
    ErrorTypes,
)
from .utils import (
    get_name_component,
    is_combinational,
    is_sequential,
)

__all__ = ['Analysis']


class FileWithoutModules(Exception):
    pass


class Analysis(object):

    def __init__(
        self,
        path_file_source: str,
        output_path: str,
        top_module: str,
        errtype='SET'
    ) -> None:
        """
        Validate and initiate class with instance attr.
        """

        self._validate_initial_params(
            path_file_source,
            output_path,
        )

        self.path_file_source = path_file_source
        self.output_path = output_path
        self.top_module = top_module
        self.errtype = errtype

    def _validate_initial_params(self, path_file_source: str, output_path: str) -> None:
        """
        Validate if the params are corrects, if not raise a Exception.
        """
        if not(path_file_source and output_path):
            raise ValueError(Errors.EMPTY_PARAMS)
        if not os.path.exists(path_file_source):
            raise ValueError(Errors.FILE_DOSNT_EXISTS)
        if not os.path.isfile(path_file_source):
            raise ValueError(Errors.IS_NOT_A_FILE)

    def _create_path(self, new_path: str) -> Path:
        """
        Create a path and his parents.
        """
        path = Path(self.output_path).joinpath(new_path)
        path.mkdir(parents=True, exist_ok=True)
        return path

    def _set_and_create_paths(self) -> None:
        """
        Create the paths necesaries to operate.
        """
        self.src_path = self._create_path('src')
        self.result_path = self._create_path('output')

    def _create_file(self, path: Path, content: str) -> None:
        with open(path, 'w') as new_file:
            new_file.write(content)

    def _create_modules_files(self, modules: list) -> None:
        """
        Create the files in the output src for the modules.
        """
        for module in modules:
            name = module.split('\n')[1].split(' ')[1] + Files.EXTENSION
            path_file = self.src_path / name
            self._create_file(path_file, module)

    def create_modules(self) -> list:
        """
        Parce the input file in modules and create the files for them.
        """
        with open(self.path_file_source, 'r') as source_file:
            lines = source_file.readlines()
        is_module = False
        module = ''
        modules = []
        for line in lines:
            if ModuleCst.TIMESCALE in line:
                timescale = line
            elif ModuleCst.END in line:
                module += line
                modules.append(module)
                module = ''
                is_module = False
            elif ModuleCst.INIT in line:
                module += timescale + line
                is_module = True
            elif is_module:
                module += line
        if not modules:
            raise FileWithoutModules(Errors.FILE_WITH_NO_MODULES)
        self._create_modules_files(modules)
        return modules

    def _get_list_port(self, lines: list) -> list:
        """
        Return a list with the lines with the port configuration.
        """
        list_port = []
        next_line_break = False
        for line in lines:
            list_port.append(line)
            if ModuleCst.PORT_END_LINE in line:
                next_line_break = True
            elif next_line_break:
                break
        return list_port

    def _get_list_io(self, lines: list) -> list:
        """
        Return a list with the lines with the input and ouput configuration.
        """
        list_dec = []
        for line in lines:
            if ModuleCst.WIRE in line:
                break
            list_dec.append(line)
        return list_dec

    def _get_list_wire(self, lines: list) -> list:
        list_wire = []
        for line in lines:
            if ModuleCst.WIRE not in line:
                break
            list_wire.append(line)
        return list_wire

    def _src_file_exists(self, name: str) -> bool:
        """
        Validate if the source file exists
        """
        filename = name + Files.EXTENSION
        file_path = self.src_path / filename
        return os.path.exists(file_path)

    def _make_injections(self, file_content: list) -> tuple(int, str):
        """
        This method analyse the kind and the id of a injection.
        """
        injection_counter = 0
        analysis = ''
        for line in file_content:
            if ModuleCst.COMPONENT_START in line:
                if (
                    is_combinational(line) and self.errtype == ErrorTypes.SET or
                    is_sequential(line) and self.errtype == ErrorTypes.SEU
                ):
                    name = get_name_component(line)
                    analysis += line.replace(name, name + Files.MOD)
                    analysis += Templates.INJ.format(injection_counter)
                    injection_counter += 1

                elif self.errtype in [ErrorTypes.SEU, ErrorTypes.SET, ErrorTypes.RAMB]:
                    name = get_name_component(line)
                    if self._src_file_exists(name):
                        mod_count = self.create_files_with_injections(name)
                        new_line = Templates.UTT_COMPONENT.format(name, name)
                        if mod_count != 0:
                            line_inj_utr = Templates.INJ_UTT.format(
                                initial_value=injection_counter + mod_count - 1,
                                final_value=injection_counter,
                            )
                            if self.errtype == ErrorTypes.RAMB:
                                line_inj_utr += Templates.RAMB_INTRC
                            new_line += line_inj_utr
                        analysis += new_line
                        injection_counter += mod_count
            else:
                analysis += line
        return injection_counter, analysis

    def create_files_with_injections(self, filename: str) -> int:
        """
        Generate the files with injections in the result path.
        This method can be recursive.
        """
        filename += Files.EXTENSION
        file_input_path = self.src_path / filename
        with open(file_input_path, 'r') as src_file:
            file_content = src_file.readlines()

        def _remove_lines(lines: list, lines_to_remove: list) -> list:
            return lines[len(lines_to_remove):]

        list_line_port = self._get_list_port(file_content)
        # remove ports from original file
        file_content = _remove_lines(file_content, list_line_port)

        list_line_io = self._get_list_io(file_content)
        # remove io from original file
        file_content = _remove_lines(file_content, list_line_io)

        list_line_wire = self._get_list_wire(file_content)
        # remove wire from original file
        file_content = _remove_lines(file_content, list_line_wire)

        # the file only have the instructions lines
        analysis, injection_counter = self._make_injections(file_content)

        content_output_file = self._get_content_output_file(
            list_line_port,
            list_line_io,
            list_line_wire,
            injection_counter,
            analysis,
        )
        self._create_file(
            self.result_path / filename,
            content_output_file
        )
        return injection_counter

    def _get_content_output_file(
        self,
        list_port: list,
        list_io: list,
        list_wire: list,
        injection_counter: int,
        analysis: str,
    ) -> str:
        """
        Generate the content for the component file with injections.
        """
        output = ""
        if self.errtype in [ErrorTypes.SEU, ErrorTypes.SET, ErrorTypes.RAMB]:
            output += Templates.INITIAL_LINE.format(list_port[0])

            if self.errtype == ErrorTypes.RAMB:
                output += Templates.INPUT_LINES_RAMB

            for port_line in list_port[1:]:
                output += port_line

            if injection_counter != 0:
                new_line = Templates.INPUT_ARRAY_INJ.format(
                    first=injection_counter-1,
                    second=0,
                )
            else:
                new_line = Templates.INPUT_INJ
            output += new_line

            if self.errtype == ErrorTypes.RAMB:
                output += Templates.INPUT_LINES_ARRAY_RAMB

            for line in list_io + list_wire:
                output += line
            output += analysis
        return output

    def run(self):
        self._set_and_create_paths()
        modules = self.create_modules()
        for module in modules:
            name = module.split('\n')[1].split(' ')[1]
            self.create_files_with_injections(name)
