# API

Quick instructions for API server instantiation.
For more information go to ./APIv3/readme.md/

## Overview

You can run the API locally with or without Docker.

## Setup Instructions

### Using Docker

1. **build app**:
  ```bash
  docker build -t yolo-api .
  ```

2. **Run the Docker container**:
  - Interactive mode:
    ```bash
    docker run -p 8000:8000 yolo-api
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

---

# Desktop APP