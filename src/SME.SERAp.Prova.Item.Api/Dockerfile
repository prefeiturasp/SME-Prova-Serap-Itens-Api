# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/SME.SERAp.Prova.Item.Api/SME.SERAp.Prova.Item.Api.csproj", "SME.SERAp.Prova.Item.Api/"]
COPY ["src/SME.SERAp.Prova.Item.Aplicacao/SME.SERAp.Prova.Item.Aplicacao.csproj", "SME.SERAp.Prova.Item.Aplicacao/"]
COPY ["src/SME.SERAp.Prova.Item.Dados/SME.SERAp.Prova.Item.Dados.csproj", "SME.SERAp.Prova.Item.Dados/"]
COPY ["src/SME.SERAp.Prova.Item.Dominio/SME.SERAp.Prova.Item.Dominio.csproj", "SME.SERAp.Prova.Item.Dominio/"]
COPY ["src/SME.SERAp.Prova.Item.Infra/SME.SERAp.Prova.Item.Infra.csproj", "SME.SERAp.Prova.Item.Infra/"]
COPY ["src/SME.SERAp.Prova.Item.IoC/SME.SERAp.Prova.Item.IoC.csproj", "SME.SERAp.Prova.Item.IoC/"]

RUN dotnet restore "SME.SERAp.Prova.Item.Api/SME.SERAp.Prova.Item.Api.csproj"

COPY . .
WORKDIR "/src/src/SME.SERAp.Prova.Item.Api"
RUN dotnet build "SME.SERAp.Prova.Item.Api.csproj" -c Release -o /app/build --no-restore

# Etapa publish
FROM build AS publish
RUN dotnet publish "SME.SERAp.Prova.Item.Api.csproj" -c Release -o /app/publish --no-build

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