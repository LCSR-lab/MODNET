from pathlib import Path

from .constants import Errors, Files, ModuleCst
from .utils import create_file

__all__ = ['ModuleParser']


class FileWithoutModules(Exception):
    pass


class ModuleParser(object):
    """
    This class analyze a file and split it into a modules.
    """

    @classmethod
    def get_module_name(cls, module: str) -> str:
        """
        Read the first line of a module and get the name.
        """
        return module.split('\n')[1].split(' ')[1]

    @classmethod
    def _create_modules_files(cls, modules: list, output_path: Path) -> None:
        """
        Create the files in the output src for the modules.
        """
        for module in modules:
            name = cls.get_module_name(module) + Files.EXTENSION
            path_file = output_path / name
            create_file(path_file, module)

    @classmethod
    def split_file_in_modules(
        cls,
        path_file_source: Path,
        output_path: Path
    ) -> list:
        """
        Parce the input file in modules and create the files for them.
        """
        with open(path_file_source, 'r') as source_file:
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
        cls._create_modules_files(modules, output_path)
        return modules
