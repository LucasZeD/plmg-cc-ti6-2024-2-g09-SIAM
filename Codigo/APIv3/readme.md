# Plane Detection Application
This repository provides a FastAPI application wrapped in a Docker container.
It exposes an endpoint for object detection using YOLO model.

---

## Table of Contents
1. [Overview](#overview)  
2. [Prerequisites](#prerequisites)  
3. [Setup Instructions](#setup-instructions)  
    - [Using Docker](#using-docker)  
    - [Without Docker](#without-docker)  
4. [API Endpoints](#api-endpoints)  
    - [Status Endpoint](#status-endpoint)  
    - [Prediction Endpoint](#prediction-endpoint)  
5. [Known Issues](#known-issues) 

---

## Overview

This API provides endpoints to upload images and perform object detection using a YOLO model.
You can run the API locally with or without Docker.

---

## Prerequisites

- **Python**: Version 3.9+ (if running without Docker)  
- **Docker**: Installed and configured  
- **Postman or cURL**: For testing endpoints  

---

## Setup Instructions

### Using Docker

1. **build app**:
  ```bash
  docker build -t yolo-api .
  ```
  - Rebuild image from scratch
  ```bash
  docker build --no-cache -t yolo-api .
  ```

2. **Run the Docker container**:
  - Interactive mode:
    ```bash
    docker run -p 8000:8000 yolo-api
    ```
  - Detached mode (background)
    ```bash
    docker run -d 8000:8000 yolo-api
    ```

3. **Stop or remove the container**:
  - List running containers:
  ```bash
  docker ps
  ```
  - Stop a container:
  ```bash
  docker stop <#containerID>
  ```
  - Remove a container:
  ```bash
  docker rm <#containerID/name>
  ```
  - Check if an process are still listening to the port:
  ```bash
  netstat -ano | findstr :8000
  ```

4. **Remove the Docker Image**:
  ```bash
  docker rmi <#iamgeID/name>
  ```

5. **Check for processes still listening on the port**:
  ```bash
  netstat -ano | findstr :8000
  ```

### Without Docker

1. **Set up the virtual environment**:
  ```bash
  python -m venv venv
  ```
  **Activate it**:
  - Windows:
    ```bash
    venv\Scripts\activate
    ```
  - Linux/Mac:
    ```bash
    source venv/bin/activate
    ```

2. **Install dependencies**:
    ```bash
    pip install -r requirements.txt
    ```

3. **Run the server**:
  ```bash
  uvicorn main:app --host 0.0.0.0 --port 8000 --reload
  ```

4. **Deactivate the virtual environment**:
  ```bash
  deactivate
  ```


## API Endpoints

### Status Endpoint

- GET /status/
Returns the status of the API.

### Prediction Endpoint

- POST /predict/SIAMv1/
Accepts an image file and performs object detection using the SIAMv1 custom YOLO model.

- POST /predict/SIAMv2/
Accepts an image file and performs object detection using the SIAMv1 custom YOLO model.

Example using cURL:
  ```plaintext
  curl -X 'POST' \
    'http://localhost:8000/predict/SIAMv1/' \
    -H 'accept: application/json' \
    -H 'Content-Type: multipart/form-data' \
    -F 'file=@pathToFile/image.jpg'
  ```
Example using Postman:
  - Method: POST
  - URL: http://localhost:8000/predict/SIAMv1/ or http://localhost:8000/predict/SIAMv2/
  - Body:
    - Type: form-data
    - Key: file (as FILE)
    - Value: Select an image file from your system.

Swagger UI access:
  - http://127.0.0.1:8000/docs

## Known Issues

### Uvicorn Freezing

Occasionally, Uvicorn may freeze and prevent the server from restarting.

- **Error**:
  ```plaintext
  [Errno 10048] error while attempting to bind on address ('0.0.0.0', 8000)
  ```

- **Solution**:
  1. Open the task manager.
  2. Kill the Python background process.

- **Explanation**:
  As uvicorn runs as a multithreaded application when CTRL + C is pressed only the main proccess will be terminated.