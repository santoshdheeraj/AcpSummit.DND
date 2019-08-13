FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["../AcpSummitApi/AcpSummitApi.csproj", "../AcpSummitApi/"]
RUN dotnet restore "../AcpSummitApi/AcpSummitApi.csproj"
COPY . .
WORKDIR "/src/../AcpSummitApi"
RUN dotnet build "AcpSummitApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AcpSummitApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AcpSummitApi.dll"]