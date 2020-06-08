__all__ = [
    'Logic',
    'Files',
    'Errors',
    'ModuleCst',
    'Templates',
    'ErrorTypes',
]


class Templates(object):
    INJ = ".inj(inj[{}]) ,\n"
    INJ_UTT = " .inj(inj[{initial_value} : {final_value} ]),\n"
    UTT_COMPONENT = "{} {}_utt(\n"
    RAMB_INTRC = ".data_mask(data_mask),\n.address(address),\n"
    INITIAL_LINE = "{}inj,\n"
    INPUT_ARRAY_INJ = "\ninput [{first}:{second}] inj ;\n"
    INPUT_INJ = "\ninput inj ; \n"
    INPUT_LINES_RAMB = "data_mask,\naddress,\n"
    INPUT_LINES_ARRAY_RAMB = "input [35:0] data_mask;\ninput [8:0] address;\n"


class ModuleCst(object):
    END = 'endmodule '
    TIMESCALE = 'timescale'
    INIT = 'module'
    WIRE = 'wire'
    PORT_END_LINE = ')'
    COMPONENT_START = ' ('


class Errors(object):
    """
    Error msg for the exceptions.
    """
    EMPTY_PARAMS = 'The source and output cant be empty.'
    FILE_DOSNT_EXISTS = 'The file must exist in the location.'
    IS_NOT_A_FILE = 'The input must be a file.'
    FILE_WITH_NO_MODULES = 'Can find any module.'


class Files(object):
    EXTENSION = '.v'
    MOD = '_mod'


class ErrorTypes(object):
    RAMB = "RAMB"
    SEU = "SEU"
    SET = "SET"


class Logic(object):
    """
    Sequential is when you dont need a clock.
    Combinational you use a clock.
    """
    COMBINATIONAL = {
        "LUT4_L",
        "LUT5",
        "LUT5_L",
        "LUT6",
        "LUT6_L",
        "INV",
        "LUT3",
        "LUT2",
        "LUT4",
        "MUXF5",
        "MUXF8",
        "MUXF7",
        "LUT3_L",
        "BUFGP",
    }

    SEQUENTIAL = {
        "FDD",
        "FDE",
        "FDC_1",
        "FDC",
        "FDCE",
        "FD",
        "FDPE",
        "FDP",
        "FDRE",
        "FDRSE",
        "FDR",
        "FDS",
        "FDSE",
        "FDRS",
    }
