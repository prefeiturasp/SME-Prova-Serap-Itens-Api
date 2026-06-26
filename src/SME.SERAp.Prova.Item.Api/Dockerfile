# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY . .

RUN dotnet restore "src/SME.SERAp.Prova.Item.Api/SME.SERAp.Prova.Item.Api.csproj"
RUN dotnet build "src/SME.SERAp.Prova.Item.Api/SME.SERAp.Prova.Item.Api.csproj" -c Release -o /app/build --no-restore

# Etapa publish
FROM build AS publish
RUN dotnet publish "src/SME.SERAp.Prova.Item.Api/SME.SERAp.Prova.Item.Api.csproj" -c Release -o /app/publish

# Etapa final (runtime pronto)
FROM base AS final
WORKDIR /app

ENV TZ=America/Sao_Paulo

RUN apt-get update \
    && apt-get install -yq tzdata locales \
    && dpkg-reconfigure --frontend noninteractive tzdata \
    && locale-gen en_US.UTF-8

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SME.SERAp.Prova.Item.Api.dll"]
