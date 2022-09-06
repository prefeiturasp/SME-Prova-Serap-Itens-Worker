#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SME.SERAp.Prova.Item.Worker/SME.SERAp.Prova.Item.Worker.csproj", "SME.SERAp.Prova.Item.Worker/"]
COPY ["src/SME.SERAp.Prova.Item.IoC/SME.SERAp.Prova.Item.IoC.csproj", "SME.SERAp.Prova.Item.IoC/"]
COPY ["src/SME.SERAp.Prova.Item.Aplicacao/SME.SERAp.Prova.Item.Aplicacao.csproj", "SME.SERAp.Prova.Item.Aplicacao/"]
COPY ["src/namespace SME.SERAp.Prova.Item.Infra/SME.SERAp.Prova.Item.Infra.csproj", "namespace SME.SERAp.Prova.Item.Infra/"]
COPY ["src/SME.SERAp.Prova.Item.Dados/SME.SERAp.Prova.Item.Dados.csproj", "SME.SERAp.Prova.Item.Dados/"]
COPY ["src/SME.SERAp.Prova.Item.Dominio/SME.SERAp.Prova.Item.Dominio.csproj", "SME.SERAp.Prova.Item.Dominio/"]
RUN dotnet restore "SME.SERAp.Prova.Item.Worker/SME.SERAp.Prova.Item.Worker.csproj"
COPY . .
WORKDIR "/src/src/SME.SERAp.Prova.Item.Worker"
RUN dotnet build "SME.SERAp.Prova.Item.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SME.SERAp.Prova.Item.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

ENV TZ=America/Sao_Paulo

RUN apt-get update \
    && apt-get install -yq tzdata locales -y \
    && dpkg-reconfigure --frontend noninteractive tzdata \ 
	  && locale-gen en_US.UTF-8
COPY --from=publish /app/publish .

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SME.SERAp.Prova.Item.Worker.dll"]