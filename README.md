# NetCoreCLI
Use .Net Core to Write a CLI tool

## Publish

1. Install .Net Core SDK 3.1.102

2. Clone Repository

   ```
   git clone https://github.com/ErikXu/NetCoreCLI.git
   ```

3. Build CLI tool

   cd NetCoreCLI

   Publish for Linux

   ```
   dotnet publish -c Release -r linux-x64 /p:PublishSingleFile=true
   ```

   Publish for Windows

   ```
   dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true
   ```