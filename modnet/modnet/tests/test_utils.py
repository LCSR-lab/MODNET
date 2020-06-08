from ..utils import is_combinational, is_sequential


def test_is_combinational():
    logic_unit = "  LUT4_L"
    assert is_combinational(logic_unit)


def test_is_not_combinational():
    logic_unit = "  FDD"
    assert not is_combinational(logic_unit)


def test_is_sequential():
    logic_unit = "  FDD"
    assert is_sequential(logic_unit)


def test_is_not_sequential():
    logic_unit = "  LUT4_L"
    assert not is_sequential(logic_unit)
