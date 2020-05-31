import os
from pathlib import Path


class FileWithoutModules(Exception):
    pass


class Analysis(object):

    def __init__(self, path_file_source, path_output, errtype='SET'):
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
        self.errtype = errtype

    def create_modules_files(self, modules):
        path = Path(self.path_output).joinpath('src')
        path.mkdir(parents=True, exist_ok=True)
        for module in modules:
            name = module.split('\n')[1].split(' ')[1] + '.v'
            path_file = path / name
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
        self.create_modules_files(modules)
