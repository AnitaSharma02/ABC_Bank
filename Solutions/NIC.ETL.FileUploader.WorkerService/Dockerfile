#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SBL.ETL.FileUploader.WorkerService/SBL.ETL.FileUploader.WorkerService.csproj", "SBL.ETL.FileUploader.WorkerService/"]
RUN dotnet restore "SBL.ETL.FileUploader.WorkerService/SBL.ETL.FileUploader.WorkerService.csproj"
COPY . .
WORKDIR "/src/SBL.ETL.FileUploader.WorkerService"
RUN dotnet build "SBL.ETL.FileUploader.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SBL.ETL.FileUploader.WorkerService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SBL.ETL.FileUploader.WorkerService.dll"]