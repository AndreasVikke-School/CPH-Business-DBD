FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

RUN apt-get update && apt-get install -y dos2unix

COPY . ./
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

RUN dos2unix ./entrypoint.sh && apt-get --purge remove -y dos2unix && rm -rf /var/lib/apt/lists/*
RUN chmod +x ./entrypoint.sh
ENTRYPOINT ["./entrypoint.sh"]