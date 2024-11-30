from fastapi import FastAPI, UploadFile, File, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from PIL import Image
import io
import uvicorn
from model import YOLOModel
from utils import save_imagefile

app = FastAPI()

#CORS
origins = ["*"]
app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

#return if is active
@app.get("/status/")
async def status():
    print("User checked API status")
    return { "message": "online"}

# YOLO models for SIAMv1 and SIAMv2
model_v1 = YOLOModel('runs/models/SIAMv1/best.pt')
model_v2 = YOLOModel('runs/models/SIAMv2/best.pt')

@app.post("/predict/SIAMv1/")
async def predict_siamv1(file: UploadFile = File(...)):
    return await predict(file, model_v1)

@app.post("/predict/SIAMv2/")
async def predict_siamv2(file: UploadFile = File(...)):
    return await predict(file, model_v2)

async def predict(file: UploadFile, model: YOLOModel):
    try:
        #Checking file type
        print(f"Received file: {file.filename}, type: {type(file)}")
        
        if not file.filename.endswith(('.jpg', '.jpeg', '.png')):
            raise HTTPException(status_code=400, detail="File format not supported.")
        
        #Read the image file
        image_data = await file.read()  #listen
        image = Image.open(io.BytesIO(image_data))
        
        #Inference
        detections, results = model.predict(image)

        #Custom saving method
        save_imagefile(image, results)
        
        #Return the detection resutls as JSON payload
        return {"predictions": detections}
    
    except Exception as e:
        print(f"An error occurred: {str(e)}")
        raise HTTPException(status_code=500, detail=str(e))

if __name__ == "__main__":
    #run api server
    uvicorn.run(app, host="0.0.0.0", port=8000)