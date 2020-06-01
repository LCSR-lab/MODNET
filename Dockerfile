FROM python:3.7

WORKDIR /modnet

COPY . .

RUN pip install -r requirements.txt