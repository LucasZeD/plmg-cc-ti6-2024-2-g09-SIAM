#use python image as base
FROM python:3.10-slim

#
RUN apt-get update && apt-get install -y \
    libgl1-mesa-glx \
    libglib2.0-0 \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

#
WORKDIR /API

#python libraries
COPY requirements.txt .

#install libraries
RUN pip install --upgrade pip
RUN pip install --no-cache-dir -r requirements.txt

#copy the api applicaton
COPY . .

#expose
EXPOSE 8000

#
#RUN 

#
#USER

#
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]
