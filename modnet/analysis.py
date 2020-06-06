import os
from pathlib import Path
from .logic import Logic


class FileWithoutModules(Exception):
    pass


class Analysis(object):

    def __init__(self, path_file_source, path_output, top_module, errtype='SET'):
        if not(path_file_source and path_output):
            raise ValueError('The source and output cant be empty.')
        if not os.path.exists(path_file_source):
            raise ValueError('The file must exist in the location.')
        if not os.path.isfile(path_file_source):
            raise ValueError('The input must be a file.')
        if not os.path.exists(path_output):
            path = Path(path_output)
            path.mkdir(parents=True, exist_ok=True)
        self.path_file_source = path_file_source
        self.path_output = path_output
        self.top_module = top_module
        self.errtype = errtype
        self._set_and_create_source_path()
        self._set_and_create_output_path()

    def _set_and_create_source_path(self):
        path = Path(self.path_output).joinpath('src')
        path.mkdir(parents=True, exist_ok=True)
        self.src_path = path

    def _set_and_create_output_path(self):
        path = Path(self.path_output).joinpath('output')
        path.mkdir(parents=True, exist_ok=True)
        self.path_output = path

    def _create_modules_files(self, modules):
        for module in modules:
            name = module.split('\n')[1].split(' ')[1] + '.v'
            path_file = self.src_path / name
            with open(path_file, 'w') as module_file:
                module_file.write(module)

    def part_file(self):
        with open(self.path_file_source, 'r') as source_file:
            lines = source_file.readlines()
        is_module = False
        module = ''
        modules = []
        for line in lines:
            if 'timescale' in line:
                timescale = line
            elif 'endmodule ' in line:
                module += line
                modules.append(module)
                module = ''
                is_module = False
            elif 'module' in line:
                module += timescale + line
                is_module = True
            elif is_module:
                module += line
        if not modules:
            raise FileWithoutModules('Can find any module.')
        self._create_modules_files(modules)
        return modules

    def _get_list_port(self, lines):
        list_port = []
        next_line_break = False
        for line in lines:
            list_port.append(line)
            if ')' in line:
                next_line_break = True
            elif next_line_break:
                break
        return list_port

    def _get_list_io(self, lines):
        list_dec = []
        for line in lines:
            if 'wire' in line:
                break
            list_dec.append(line)
        return list_dec

    def _get_list_wire(self, lines):
        list_wire = []
        for line in lines:
            if 'wire' not in line:
                break
            list_wire.append(line)
        return list_wire

    def _src_file_exists(self, name):
        filename = name + '.v'
        file_path = self.src_path / filename
        return os.path.exists(file_path)

    def injection_analysis(self, filename):
        counter = 0
        analysis = ''
        filename += '.v'
        file_path = self.src_path / filename
        with open(file_path, 'r') as v_file:
            file_content = v_file.readlines()
        
        list_port = self._get_list_port(file_content)
        file_content = file_content[len(list_port):]

        list_io = self._get_list_io(file_content)
        file_content = file_content[len(list_io):]

        list_wire = self._get_list_wire(file_content)
        file_content = file_content[len(list_wire):]
        inj_str = ".inj(inj[{}]) ,\n"
        inj_utt_str = " .inj(inj[{initial_value} : {final_value} ]),\n"
        for line in file_content:
            if " (" in line:
                if (
                    Logic.is_combinational(line) and self.errtype == "SET" or
                    Logic.is_sequential(line) and self.errtype == "SEU"
                ):
                    name = Logic.get_name_component(line)
                    analysis += line.replace(name, name + "_mod")
                    analysis += inj_str.format(counter)
                    counter += 1

                elif self.errtype in ["SEU", "SET", "RAMB"]:
                    name = Logic.get_name_component(line)
                    if self._src_file_exists(name):
                        mod_count = self.injection_analysis(name)
                        new_line = name + " " + name + "_utt (\n"
                        if mod_count != 0:
                            line_inj_utr = inj_utt_str.format(
                                initial_value=counter + mod_count - 1,
                                final_value=counter,
                            )
                            if self.errtype == "RAMB":
                                line_inj_utr += ".data_mask(data_mask),\n.address(address),\n"
                            new_line += line_inj_utr
                        analysis += new_line
                        counter += mod_count
            else:
                analysis += line
            
        # import ipdb; ipdb.set_trace()
        content_output_file = ''
        if self.errtype in ["SEU", "SET"]:
            if counter != 0:
                content_output_file += (
                    list_port[0] +
                    "inj,\n"
                )
                for line in list_port[1:]:
                    content_output_file += line
                content_output_file += (
                    "\ninput [" +
                    str(counter - 1) +
                    ": 0] inj ;\n"
                )
                for line in list_io:
                    content_output_file += line
                for line in list_wire:
                    content_output_file += line

                content_output_file += analysis
            else:
                content_output_file += (
                    list_port[0] +
                    "\n inj,\n"
                )
                for line in list_port[1:]:
                    content_output_file += line
                content_output_file += "\ninput inj ;\n"
                for line in list_io:
                    content_output_file += line
                for line in list_wire:
                    content_output_file += line
                content_output_file += analysis
                print(content_output_file)

        elif self.errtype == "RAMB":
            if counter != 0:
                content_output_file += (
                    list_port[0] +
                    "inj,\ndata_mask,\naddress,\n"
                )
                for line in list_port[1:]:
                    content_output_file += line
                content_output_file += (
                    "\ninput [" +
                    str(counter - 1) +
                    ": 0] inj ;\ninput [35:0] data_mask;\ninput [8:0] address;\n"
                )
                for line in list_io:
                    content_output_file += line
                for line in list_wire:
                    content_output_file += line
                content_output_file += analysis
            else:
                content_output_file += (
                    list_port[0] +
                    "inj,\ndata_mask,\naddress,\n"
                )
                for line in list_port[1:]:
                    content_output_file += line
                content_output_file += "\ninput inj ;\ninput [35:0] data_mask;\ninput [8:0] address;\n"
                for line in list_io:
                    content_output_file += line
                for line in list_wire:
                    content_output_file += line
                content_output_file += analysis
        output_path = self.path_output / filename
        with open(output_path, 'w') as module_file:
            module_file.write(content_output_file)
        return counter

    def run(self):
        modules = self.part_file()
        
        for module in modules:
            name = module.split('\n')[1].split(' ')[1]
            # print(name)
            self.injection_analysis(name)