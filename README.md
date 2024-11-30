# SIAM: A Military Aircraft Identifier System

O SIAM (Sistema de Identificação de Aeronaves Militares) é uma solução desenvolvida para identificar aeronaves militares a partir de imagens enviadas por um aplicativo desktop multiplataforma. Este sistema, projetado no curso de Ciência da Computação da PUC Minas, utiliza um modelo de rede neural convolucional (CNN) customizado baseado no YOLOv8 para análise das imagens, com o objetivo de detectar e fornecer informações detalhadas sobre possíveis ameaças, contribuindo para estratégias de segurança mais eficazes.

O modelo foi treinado e otimizado com CUDA, garantindo alto desempenho, e está hospedado na infraestrutura do Amazon EC2, containerizado com Docker. A comunicação entre o aplicativo e o modelo ocorre por meio de uma API implementada em FastAPI, também hospedada no ambiente AWS. Dessa forma, o SIAM oferece uma ferramenta robusta para reconhecimento de campo e apoio à segurança, unindo inteligência artificial e computação distribuída.

## Alunos integrantes da equipe

* Bruno Henrique da Silva Brum
* Christian David Costa Vieira
* Daniel Lucas Murta
* Gabriel Luna dos Anjos
* Lucas Zegrine Duarte

## Professores responsáveis

* Alexei Manso Correa Machado 
* Fatima de Lima Procopio Duarte
* Henrique Cota de Freitas 

## Instruções de utilização

1. Siga as instruções na pasta Codigo/API para criar a instância do servidor da API.
2. Altere o BaseAddress na classe ApiService, localizada em Codigo/PlaneDetectionv3/Services/, com o endereço do servidor:
  ```csharp
  BaseAddress = new Uri("http<s?>://<address>:<port>");
  ```
  Substitua <endereco> e <porta> pelo endereço e porta correspondentes ao seu servidor.
3. Compile e execute o aplicativo desktop seguindo as instruções fornecidas em readme.md.
4. Utilize as imagens de aviões disponíveis na pasta Codigo/TestingPlanes para testar o funcionamento do aplicativo.
  Alternativamente, você pode realizar testes com fotos de aviões de outras fontes.

---

# SIAM: A Military Aircraft Identifier System

The SIAM (Military Aircraft Identification System) is a solution developed to identify military aircraft based on images uploaded through a cross-platform desktop application. This system, designed as part of the Computer Science program at PUC Minas, uses a custom convolutional neural network (CNN) model based on YOLOv8 to analyze images, aiming to detect and provide detailed information about potential threats, contributing to more effective security strategies.

The model was trained and optimized with CUDA for high performance and is hosted on Amazon EC2 infrastructure, containerized with Docker. Communication between the application and the model is facilitated through an API implemented in FastAPI, also hosted in the AWS environment. Thus, SIAM offers a robust tool for field recognition and security support, combining artificial intelligence and distributed computing.

## Team Members

* Bruno Henrique da Silva Brum
* Christian David Costa Vieira
* Daniel Lucas Murta
* Gabriel Luna dos Anjos
* Lucas Zegrine Duarte

## Supervising Professors

* Alexei Manso Correa Machado 
* Fatima de Lima Procopio Duarte
* Henrique Cota de Freitas 

## Usage Instructions

1. Follow the instructions in the folder Codigo/API to create the API server instance.
2. Update the BaseAddress in the class ApiService located at Codigo/PlaneDetectionv3/Services/ with the server address:
  ```csharp
  BaseAddress = new Uri("http<s?>://<address>:<port>");
  ```
  Replace <address> and <port> with the corresponding address and port of your server.
3. Compile and run the desktop application following the instructions provided in readme.md.
4. Use the airplane images from the folder Codigo/TestingPlanes to test the application's functionality.
  Alternatively, you can test it with airplane photos from other sources.
