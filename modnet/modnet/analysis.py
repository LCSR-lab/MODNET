import os
from pathlib import Path
from typing import Tuple

from .constants import Errors, ErrorTypes, Files, ModuleCst, Templates
from .module_parser import ModuleParser
from .utils import (
    create_file,
    get_name_component,
    is_combinational,
    is_sequential,
)

__all__ = ['Analysis']


class Analysis(object):
    """
    This class has the methods to analyze an input file,
    split it into files with their modules and
    modify these files so that they have injections.
    """

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

        self.path_file_source = Path(path_file_source)
        self.output_path = Path(output_path)
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

    def _set_and_create_paths(self) -> None:
        """
        Create the paths necesaries to operate.
        """

        self.src_path = self.output_path.joinpath('src')
        self.src_path.mkdir(parents=True, exist_ok=True)

        self.result_path = self.output_path.joinpath('output')
        self.result_path.mkdir(parents=True, exist_ok=True)

    def _remove_module_start(self, lines: list) -> Tuple[list, list]:
        """
        """
        list_module_start = []
        next_line_break = False
        for line in lines:
            if '(' in line:
                next_line_break = True
            elif next_line_break:
                break
            list_module_start.append(line)

        return lines[len(list_module_start):], list_module_start

    def _remove_list_port(self, lines: list) -> Tuple[list, list]:
        """
        """
        list_port = []
        next_line_break = False
        for line in lines:
            list_port.append(line)
            if ModuleCst.PORT_END_LINE in line:
                next_line_break = True
            elif next_line_break:
                break

        return lines[len(list_port):], list_port

    def _remove_list_io(self, lines: list) -> Tuple[list, list]:
        """
        """
        list_dec = []
        for line in lines:
            if ModuleCst.WIRE in line:
                break
            list_dec.append(line)
        return lines[len(list_dec):], list_dec

    def _remove_list_wire(self, lines: list) -> Tuple[list, list]:
        """
        """
        list_wire = []
        for line in lines:
            if ModuleCst.WIRE not in line:
                break
            list_wire.append(line)
        return lines[len(list_wire):], list_wire

    def _src_file_exists(self, name: str) -> bool:
        """
        Validate if the source file exists
        """
        filename = name + Files.EXTENSION
        file_path = self.src_path / filename
        return os.path.exists(file_path)

    def _make_injections(self, file_content: list) -> Tuple[int, str]:
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
                        line = Templates.UTT_COMPONENT.format(name, name)
                        if mod_count != 0:
                            line_inj_utr = Templates.INJ_UTT.format(
                                initial_value=injection_counter + mod_count - 1,
                                final_value=injection_counter,
                            )
                            if self.errtype == ErrorTypes.RAMB:
                                line_inj_utr += Templates.RAMB_INTRC
                            line += line_inj_utr
                        injection_counter += mod_count
                    analysis += line
                else:
                    analysis += line
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

        file_content, list_module_start = self._remove_module_start(file_content)

        # remove ports from original file
        file_content, list_line_port = self._remove_list_port(file_content)

        # remove io from original file
        file_content, list_line_io = self._remove_list_io(file_content)

        # remove wire from original file
        file_content, list_line_wire = self._remove_list_wire(file_content)

        # the file only have the instructions lines
        injection_counter, analysis = self._make_injections(file_content)

        content_output_file = self._get_content_output_file(
            list_line_port,
            list_line_io,
            list_line_wire,
            list_module_start,
            injection_counter,
            analysis,
        )
        create_file(
            self.result_path / filename,
            content_output_file
        )
        return injection_counter

    def _get_content_output_file(
        self,
        list_port: list,
        list_io: list,
        list_wire: list,
        list_module_start: list,
        injection_counter: int,
        analysis: str,
    ) -> str:
        """
        Generate the content for the component file with injections.
        """
        output = list_module_start[0]
        if self.errtype in [ErrorTypes.SEU, ErrorTypes.SET, ErrorTypes.RAMB]:
            output += Templates.INITIAL_LINE.format(list_module_start[1])

            if self.errtype == ErrorTypes.RAMB:
                output += Templates.INPUT_LINES_RAMB

            output += ''.join(list_port)

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

            output += ''.join(list_io + list_wire)
            output += analysis
        return output

    def run(self):
        """
        Execute all the methods to generate the files with injections from the input file.
        Two groups of files will be generated, ones in {output_folder}/src/ and others in {output_folder}/output/
        In the src folder are the modules with out injectios, in the output folder are the modules with the injections.
        """
        self._set_and_create_paths()
        modules = ModuleParser.split_file_in_modules(
            self.path_file_source,
            self.src_path,
        )
        for module in modules:
            name = ModuleParser.get_module_name(module)
            self.create_files_with_injections(name)
