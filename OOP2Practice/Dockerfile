FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OOP2Practice/OOP2Practice.csproj", "OOP2Practice/"]
RUN dotnet restore "OOP2Practice/OOP2Practice.csproj"
COPY . .
WORKDIR "/src/OOP2Practice"
RUN dotnet build "OOP2Practice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OOP2Practice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OOP2Practice.dll"]
