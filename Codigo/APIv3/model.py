from ultralytics import YOLO
from PIL import Image
import torch

class YOLOModel:
    def __init__(self, model_path):
        #load model
        self.model = YOLO(model_path)
    
    #yolov8
    def predict(self, image):
        #inference
        results = self.model(image)
        
        #ensure correct formatting
        if isinstance(results, list) and len(results) > 0:
            results = results[0]
        else:
            raise ValueError("Unexpected results format.")
        
        #ensure not null
        detections = []
        if hasattr(results, 'boxes'):
            for box in results.boxes:
                xmin, ymin, xmax, ymax, confidence, class_idx = box.xyxy[0].tolist() + [box.conf[0], box.cls[0]]
                
                class_label = self.model.names[int(class_idx)]
                segmentation = box.mask if hasattr(box, 'mask') else None
                
                detections.append({
                    "xmin": xmin,
                    "ymin": ymin,
                    "xmax": xmax,
                    "ymax": ymax,
                    "confidence": (confidence.item()),
                    "class": int(class_idx),
                    "label": class_label,
                    #to list if none
                    "segmentation": segmentation.tolist() if segmentation is not None else None
                    #correct way of doing it. Needs to have an, if for, for each values, will be done on third sprint
                    #https://dev.to/andreygermanov/how-to-implement-instance-segmentation-using-yolov8-neural-network-3if9
                    #https://docs.ultralytics.com/tasks/segment/
                    # ,"segmentation": segmentation
                    # ,"polygons": polygons
                })
        return detections, results
    
    #yolov5
    #model = torch.hub.load('ultralytics/yolov5', 'custom', path='runs/models/best.pt', force_reload=True)
    #Check if results are empty
    #if not results.xyxy:
    #    print("No objects detected.")
    #    raise HTTPException(status_code=204, detail="No objects detected.")
    ##Parse result
    #detection = results.pandas().xyxy[0].to_dict(orient = "records")
    #for *box, confidence, class_idx in results.xyxy[0].tolist():
    #    xmin, ymin, xmax, ymax = box
    #    class_label = model.names[int(class_idx)]
    #    detections.append(
    #        {
    #            "xmin" : xmin,
    #            "ymin" : ymin,
    #            "xmax" : xmax,
    #            "ymax" : ymax,
    #            "confidence"  : confidence,
    #            "class" : int(class_idx),
    #            "label" : class_label
    #        }
    #    )
    #results.save()  #save results to 'run/detect/exp' 