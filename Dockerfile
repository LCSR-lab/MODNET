FROM python:3.7

WORKDIR /modnet

COPY requirements.txt .

RUN pip install -r requirements.txt

COPY . .