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

# read the contents of your README file
from os import path
this_directory = path.abspath(path.dirname(__file__))
with open(path.join(this_directory, 'README.rst'), encoding='utf-8') as f:
    long_description = f.read()

setup(
    name="mod-net",
    version="v2.0.0-alpha1",
    description="Make SET and SEU fault injections in hierarchical verilog netlists",
    long_description=long_description,
    long_description_content_type='text/x-rst',
    packages=find_packages(exclude=("tests",)),
    install_requires=[
        'Click',
    ],
    entry_points={
        'console_scripts': ['modnet=modnet.cli:main'],
    }
)
