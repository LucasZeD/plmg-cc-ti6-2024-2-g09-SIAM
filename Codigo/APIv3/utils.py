from PIL import Image, ImageDraw
from datetime import datetime
import os

#RRead image using Pillow
def read_imagefile(file) -> Image.Image:
    return Image.open(file).convert("RGB")

def save_imagefile(image, detections):
    #Copy image
    draw_image = image.copy()
    draw = ImageDraw.Draw(draw_image)

    #Check for detections
    if detections and hasattr(detections, 'boxes'):
        boxes = detections.boxes.data

        #Check if the tensor is 2D before trying to index it
        if boxes.dim() == 2: 
            labels = []
             
            for box in boxes:
                #Yolov8 box parameters
                x1, y1, x2, y2, conf, cls = box
                #Drawbox
                draw.rectangle([x1.item(), y1.item(), x2.item(), y2.item()], outline="red", width=2)
                #add label for text
                labels.append(str(cls.item())) #<- new 28/11
                draw.text((x1.item(), y1.item()), f"{cls.item()} {conf.item():.2f}", fill="red")
        
            #Get label + date time
            label = labels[0] if labels else "unknown"
            timestamp = datetime.now().strftime("%d-%m-%Y_%H-%M-%S")
            
            #Save image 
            
            #filename = f"{label}_{timestamp}.jpg" #<- old (does not work properly fix later)
            filename = f"unknown_{timestamp}.jpg" #<- new
            
            #find save dir
            """
            # print("Current Working Directory:", os.getcwd())
            # save_dir = os.path.join(os.getcwd(), 'runs/history')
            code above does not work when running on docker
            
            code below ensures that the file name will be returned correctly regardless of it being run on or without docker
            use script's directory as a base
            """
            script_dir = os.path.dirname(os.path.abspath(__file__))
            save_dir = os.path.join(script_dir, 'runs/history')
            print("Current Working Directory:", save_dir)
            #path where file will be saved
            save_path = os.path.join(save_dir, filename)

            #create if needed
            if not os.path.exists(save_dir):
                print("Creating directory:", save_dir)
                os.makedirs(save_dir)
            
            try:
                draw_image.save(save_path)
                print(f"Image saved at: {save_path}")
                print(f"Directory: {save_dir}\nFile name: {filename}")
            except Exception as e:
                print(f"Error saving image: {e}")
        else:
            print("Detected boxes tensor is not 2D")
    else:
        print("No detections available.")
