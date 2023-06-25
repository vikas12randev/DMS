FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/TT.Deliveries.Web.Api/TT.Deliveries.Web.Api.csproj", "Web/TT.Deliveries.Web.Api/"]
COPY ["Data/TT.Deliveries.Data/TT.Deliveries.Data.csproj", "Data/TT.Deliveries.Data/"]
COPY ["TT.Deliveries.Application/TT.Deliveries.Application.csproj", "TT.Deliveries.Application/"]
COPY ["TT.Delivery.Persistence/TT.Deliveries.Persistence.csproj", "TT.Delivery.Persistence/"]
RUN dotnet restore "Web/TT.Deliveries.Web.Api/TT.Deliveries.Web.Api.csproj"
COPY . .
WORKDIR "/src/Web/TT.Deliveries.Web.Api"
RUN dotnet build "TT.Deliveries.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TT.Deliveries.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TT.Deliveries.Web.Api.dll"]