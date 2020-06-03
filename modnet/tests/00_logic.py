import pytest
from .modnet import logic

def test_is_combinational(supply_logic):
    logic_unit = "LUT4"
    assert logic_unit in supply_logic[0]

def test_is_sequential(supply_logic):
    logic_unit = "FDD"
    assert logic_unit in supply_logic[1]
