#!/bin/bash

pip install pytest==5.4.2 pytest-cov==2.8.1 coveralls==1.11.1
pytest --cov=modnet --cov-report html
coverage report