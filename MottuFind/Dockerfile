# -----------------------------
# Fase base (runtime)
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Cria usuário não-root
RUN addgroup --system mottu && adduser --system --disabled-password --ingroup mottu mottu

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# -----------------------------
# Fase de build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos .csproj de cada camada
COPY ["MottuFind/MottuFind_C_.API.csproj", "MottuFind/"]
COPY ["MottuFind_C_.Application/MottuFind_C_.Application.csproj", "MottuFind_C_.Application/"]
COPY ["MottuFind_C_.Domain/MottuFind_C_.Domain.csproj", "MottuFind_C_.Domain/"]
COPY ["MottuFind_C_.Infrastructure/MottuFind_C_.Infrastructure.csproj", "MottuFind_C_.Infrastructure/"]

# Restaura dependências
RUN dotnet restore "MottuFind/MottuFind_C_.API.csproj"

# Copia todo o código
COPY . .

# Compila a API
WORKDIR "/src/MottuFind"
RUN dotnet build "MottuFind_C_.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# -----------------------------
# Fase de publicação
# -----------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MottuFind_C_.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# -----------------------------
# Fase final (produção)
# -----------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define usuário não-root
USER mottu

# Entry point
ENTRYPOINT ["dotnet", "MottuFind_C_.API.dll"]
