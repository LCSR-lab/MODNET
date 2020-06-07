from .constants import Logic

__all__ = [
    'get_name_component',
    'is_combinational',
    'is_sequential',
]


def get_name_component(line: str) -> str:
    return line.split(' ')[2]


def is_combinational(line: str) -> bool:
    name = get_name_component(line)
    return name in Logic.COMBINATIONAL


def is_sequential(line: str) -> bool:
    name = get_name_component(line)
    return name in Logic.SEQUENTIAL
