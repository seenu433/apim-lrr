# Long Running API

## Deployment Instructions for the Long running HelloWorldApi

Login to Azure Container Registry

```powershell
az acr login --name <<acrname>>
```

Build Image

```powershell
cd .\HelloWorldApi\
docker build -t <<acrname>>.azurecr.io/<<imagename>>:<<tag>> .
docker push <<acrname>>.azurecr.io/<<imagename>>:<<tag>>
cd ..
```

Deploy to Azure Kubernetes Service

```powershell
kubectl apply -f .\deployment.yaml
```

## Running Instructions for the API Consumer Client

Update the api URI in line 26 of azure-dunction-consumer/program.cs

```csharp
dotnet run
```
# apim-lrr
