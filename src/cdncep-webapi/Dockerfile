FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5184

ENV ASPNETCORE_URLS=http://+:5184

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/cdncep-webapi/cdncep-webapi.csproj", "src/cdncep-webapi/"]
RUN dotnet restore "src/cdncep-webapi/cdncep-webapi.csproj"
COPY . .
WORKDIR "/src/src/cdncep-webapi"
RUN dotnet build "cdncep-webapi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "cdncep-webapi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cdncep-webapi.dll"]
