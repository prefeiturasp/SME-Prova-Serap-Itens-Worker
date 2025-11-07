#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src
COPY ["src/SME.SERAp.Prova.Item.Worker/SME.SERAp.Prova.Item.Worker.csproj", "SME.SERAp.Prova.Item.Worker/"]
COPY ["src/SME.SERAp.Prova.Item.IoC/SME.SERAp.Prova.Item.IoC.csproj", "SME.SERAp.Prova.Item.IoC/"]
COPY ["src/SME.SERAp.Prova.Item.Aplicacao/SME.SERAp.Prova.Item.Aplicacao.csproj", "SME.SERAp.Prova.Item.Aplicacao/"]
COPY ["src/SME.SERAp.Prova.Item.Infra/SME.SERAp.Prova.Item.Infra.csproj", "SME.SERAp.Prova.Item.Infra/"]
COPY ["src/SME.SERAp.Prova.Item.Dados/SME.SERAp.Prova.Item.Dados.csproj", "SME.SERAp.Prova.Item.Dados/"]
COPY ["src/SME.SERAp.Prova.Item.Dominio/SME.SERAp.Prova.Item.Dominio.csproj", "SME.SERAp.Prova.Item.Dominio/"]

RUN dotnet restore "SME.SERAp.Prova.Item.Worker/SME.SERAp.Prova.Item.Worker.csproj"
COPY . .
WORKDIR "/src/src/SME.SERAp.Prova.Item.Worker"
RUN dotnet build "SME.SERAp.Prova.Item.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SME.SERAp.Prova.Item.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV TZ=America/Sao_Paulo

RUN apt-get update \
    && apt-get install -yq tzdata locales -y \
    && dpkg-reconfigure --frontend noninteractive tzdata \ 
    && locale-gen en_US.UTF-8 \
    && apt-get install -y --allow-unauthenticated \
    libc6-dev \
    libgdiplus \
    libx11-dev \
    && rm -rf /var/lib/apt/lists/*

RUN echo "\n[openssl_init]" >> /etc/ssl/openssl.cnf
RUN sed -i '/[openssl_init]/a ssl_conf = ssl_configuration' /etc/ssl/openssl.cnf

RUN echo "\n[ssl_configuration]" >> /etc/ssl/openssl.cnf \
    && echo "system_default = tls_system_default" >> /etc/ssl/openssl.cnf

RUN echo "\n[tls_system_default]" >> /etc/ssl/openssl.cnf \
    && echo "MinProtocol = TLSv1" >> /etc/ssl/openssl.cnf \
    && echo "CipherString = DEFAULT@SECLEVEL=0" >> /etc/ssl/openssl.cnf``

ENV ASPNETCORE_HTTP_PORTS=80

ENTRYPOINT ["dotnet", "SME.SERAp.Prova.Item.Worker.dll"]