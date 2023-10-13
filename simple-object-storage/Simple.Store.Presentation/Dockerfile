FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["Simple.Store.Domain/Simple.Store.Domain.csproj", "Simple.Store.Domain/"]
COPY ["Simple.Store.Presentation/Simple.Store.Presentation.csproj", "Simple.Store.Presentation/"]
COPY ["Simple.Store.Infrastructure/Simple.Store.Infrastructure.csproj", "Simple.Store.Infrastructure/"]
COPY ["Simple.Store.Application/Simple.Store.Application.csproj", "Simple.Store.Application/"]


RUN dotnet restore "Simple.Store.Presentation/Simple.Store.Presentation.csproj"
COPY . .
WORKDIR "/src/Simple.Object.Storage.Presentation"
RUN dotnet build "Simple.Store.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simple.Store.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simple.Store.Presentation.Presentation.dll"]
