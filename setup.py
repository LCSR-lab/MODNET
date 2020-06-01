import sys
from pip._internal.req import parse_requirements
from setuptools import find_packages, setup

CURRENT_PYTHON = sys.version_info[:2]
REQUIRED_PYTHON = (3, 7)

# Check python version
if CURRENT_PYTHON < REQUIRED_PYTHON:
    sys.stderr.write("""
==========================
Unsupported Python version
==========================
This version of modnet requires Python {}.{}, but you're trying to
install it on Python {}.{}.
""".format(*(REQUIRED_PYTHON + CURRENT_PYTHON)))
    sys.exit(1)


install_reqs = parse_requirements(
    'requirements.txt', session='webaio'
)
reqs = [str(ir.req) for ir in install_reqs]

setup(
    name="mod-net",
    version="2.0.0",
    description="Make SET and SEU fault injections in hierarchical verilog netlists",
    packages=find_packages(exclude=("tests",)),
    install_requires=reqs,
)