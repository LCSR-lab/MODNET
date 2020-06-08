import sys

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

setup(
    name="mod-net",
    version="v2.0.0-beta3",
    description="Make SET and SEU fault injections in hierarchical verilog netlists",
    packages=find_packages(exclude=("tests",)),
    install_requires=[
        'Click',
    ],
    entry_points={
        'console_scripts': ['modnet=modnet.cli:main'],
    }
)
