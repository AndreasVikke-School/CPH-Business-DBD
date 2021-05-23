FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh