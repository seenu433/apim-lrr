# Azure Function Consumer

This console application is designed to consume an Azure Function. It demonstrates how to make HTTP requests to an Azure Function and process the responses.

## Project Structure

```
azure-function-consumer
├── src
│   ├── Program.cs
│   ├── Services
│   │   └── AzureFunctionService.cs
│   └── Models
│       └── FunctionResponse.cs
├── azure-function-consumer.csproj
└── README.md
```

## Setup Instructions

1. Clone the repository:
   ```
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```
   cd azure-function-consumer
   ```

3. Restore the project dependencies:
   ```
   dotnet restore
   ```

4. Build the project:
   ```
   dotnet build
   ```

## Usage

To run the application, use the following command:
```
dotnet run --project src/azure-function-consumer.csproj
```

Make sure to replace `<repository-url>` with the actual URL of your repository. Adjust any necessary configurations in the `AzureFunctionService` to point to your Azure Function endpoint.