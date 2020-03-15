FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
COPY . /app
WORKDIR /app
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000/

ENTRYPOINT ["dotnet", "WebApi.dll"]