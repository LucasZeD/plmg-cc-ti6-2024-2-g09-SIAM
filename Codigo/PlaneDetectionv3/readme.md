# App Desktop : SIAM: Sistema de Identificação de Aeronaves Militares

This desktop application is part of the **SIAM (Military Aircraft Identification System)** project, developed using **.NET 9** and **Avalonia UI**. The application allows users to identify military aircraft based on images, using a custom-trained YOLOv8 model deployed on AWS. The application communicates with the model via an API, providing predictions about potential threats based on the input image. The application uses Avalonia UI for a cross-platform desktop experience.

## Features

- Cross-platform desktop application built with Avalonia UI.
- Integration with a custom YOLOv8 model for image classification.
- API communication with FastAPI backend hosted on AWS.
- Ability to configure API server address for communication.

## Dependencies

- .NET 9 ou superior
- Avalonia
- Avalonia.Desktop
- Avalonia.Themes.Fluent
- Avalonia.Fonts.Inter
- CommunityToolkit.Mvvm
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Sqlite

## Setup Instructions

### 1. API Server Configuration
Follow the instructions below to set up the API server:

- Navigate to `Codigo/PlaneDetectionv3/Services/`.
- Modify the `BaseAddress` in the `ApiService` class to reflect the address and port of your server:

```csharp
BaseAddress = new Uri("http<s?>://<address>:<port>");
```

- Replace <address> and <port> with the appropriate values for your server.

## Build and Run the Application

- Open the project in your preferred IDE (e.g., Visual Studio).

- Restore the NuGet packages.
```bash
dotnet restore
```

- Compile the application.
```bash
dotnet build
```

- Run the application as per the instructions provided in the project’s README.md.
```bash
dotnet run
```

## Testing

You can test the application by uploading images of military aircraft. For testing purposes, you can use the images located in `Codigo/TestingPlanes`. Alternatively, you can test the system with aircraft images from other sources.

## License
This project is licensed under the Creative Commons License - see the LICENSE.md file for details.