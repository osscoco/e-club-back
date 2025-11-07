# ====== BUILD ======
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier uniquement les csproj pour bénéficier du cache Docker
COPY EClubBackend/EClubBackend.csproj EClubBackend/
# (Si tu as un .sln qui référence tout, pas obligatoire de le copier ; on restaure sur le .csproj API)

# Restore sur le projet API (traîne les dépendances des autres projets référencés)
RUN dotnet restore EClubBackend/EClubBackend.csproj

# Copier le reste du code
COPY . .

# Publish en Release (framework net8.0) vers /app/out
RUN dotnet publish EClubBackend/EClubBackend.csproj -c Release -o /app/out --no-restore -p:UseAppHost=false

# ====== RUNTIME ======
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Render fournit la variable PORT. On écoute 0.0.0.0:${PORT}
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
# (optionnel) fixe le timezone :
# RUN apt-get update && apt-get install -y tzdata && ln -fs /usr/share/zoneinfo/Europe/Paris /etc/localtime

COPY --from=build /app/out ./

# Démarre l'API (nom du .dll = nom du projet API)
ENTRYPOINT ["dotnet", "EClubBackend.dll"]