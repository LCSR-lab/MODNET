from pathlib import Path

from .constants import Logic

__all__ = [
    'create_file',
    'get_name_component',
    'is_combinational',
    'is_sequential',
]


def create_file(path: Path, content: str) -> None:
    with open(path, 'w') as new_file:
        new_file.write(content)


def get_name_component(line: str) -> str:
    return line.split(' ')[2]


def is_combinational(line: str) -> bool:
    name = get_name_component(line)
    return name in Logic.COMBINATIONAL


def is_sequential(line: str) -> bool:
    name = get_name_component(line)
    return name in Logic.SEQUENTIAL
