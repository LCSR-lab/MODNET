import pytest
from ..logic import Logic

def test_is_combinational():
    logic_unit = "  LUT4_L"
    assert Logic.is_combinational(logic_unit)

def test_is_sequential():
    logic_unit = "  FDD"
    assert Logic.is_sequential(logic_unit)
