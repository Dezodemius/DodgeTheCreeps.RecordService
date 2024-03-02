FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DodgeTheCreeps.RecordService/DodgeTheCreeps.RecordService.csproj", "DodgeTheCreeps.RecordService/"]
RUN dotnet restore "DodgeTheCreeps.RecordService/DodgeTheCreeps.RecordService.csproj"
COPY . .
WORKDIR "/src/DodgeTheCreeps.RecordService"
RUN dotnet build "DodgeTheCreeps.RecordService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DodgeTheCreeps.RecordService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DodgeTheCreeps.RecordService.dll"]
