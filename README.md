## 🚀 E-Club

Gestion de club de tennis

![Licence](https://img.shields.io/badge/Licence-MIT-blue.svg)  
![Version](https://img.shields.io/badge/Version-1.0.0-brightgreen.svg)  
![Statut](https://img.shields.io/badge/Statut-En%20d%C3%A9veloppement-orange)

## 🧰 Technologies utilisées (Local)

![Docker](https://img.shields.io/badge/Docker%20-green)
![BackEnd](https://img.shields.io/badge/Backend-DotNetCore%20v8-purple)
![EFCore](https://img.shields.io/badge/ORM-EFCore%20v8-blue)
![MySQL](https://img.shields.io/badge/Base%20de%20donn%C3%A9es-MySQL-yellowgreen)

## 🧰 Technologies utilisées (Préproduction OnRender)

![Docker](https://img.shields.io/badge/Docker%20-green)
![BackEnd](https://img.shields.io/badge/Backend-DotNetCore%20v8-purple)
![EFCore](https://img.shields.io/badge/ORM-EFCore%20v8-blue)
![PostgreSQL](https://img.shields.io/badge/Base%20de%20donn%C3%A9es-PostgreSQL-yellowgreen)

## 📸 Logo

![screenshot](./Assets/images/logo.png)

## 🔶 Cheat Sheet - Git

#### 🔷 Repos Distant ➡️ Repos Local

```bash
# Clonage du dépôt
git clone https://github.com/nom-utilisateur/nom-projet.git

# Accès au dossier
cd nom-projet

# Création d'une sous branche de la branche "master"
git checkout -b sous-branche-de-master master
```

#### 🔷 Repos Local ➡️ Repos Distant

```bash
# Accès au dossier local fraichement créé
cd nom-projet

# Initialisation du dépôt
git init

# Signature sur le nom du repos local
git config --global user.name "votre prénom et votre nom"

# Affichage du nom de la signature du repos local
git config user.name

# Signature sur le mail du repos local
git config --global user.email "votre email"

# Affichage de l'email de la signature du repos local
git config user.email

# Liaison du repos local vers un repos distant fraichement initialisé
git remote add origin "https://github.com/nom-utilisateur/nom-projet.git"
```

#### 🔷 Suite logique

```bash
# Ajout/Modification/Suppression des fichiers sources 
...

# Affichage des fichiers modifiés 
git status

# Ajout de tous les fichiers modifiés dans la pile temporaire
git add .

# Ajout d'un fichier modifié "file" dans la pile temporaire
git add ./file

# Liaison du message "message" à la pile temporaire
git commit -m "message"

# Envoi de la pile temporaire du repos local vers le repos distant
git push -u origin sous-branche-de-master

# Interface Git (Attente de l'envoi de la pile temporaire vers la branche supérieur "master")
...

# Récupération de la mise à jour de la branche "master"
git checkout master
git pull origin master
```

## 📦 Installation

#### 🔷 Git

- Installation de Git Bash : https://git-scm.com/

#### 🔷 Backend

- Téléchargement de l'IDE Visual Studio 2022 (Community) : https://visualstudio.microsoft.com/fr/downloads/

- Dans Visual Studio Installer, installer :
    - Dans "Web et cloud" :
        - Développement web et ASP (dotnet)
    - Dans "Bureau et mobile" :
        - Développement dotnet Desktop
    - Dans "Autres ensembles d'outils" :
        - Stockage et traitement des données

```bash
# Clone du dépôt
git clone https://github.com/osscoco/e-club-back.git
```

###### 🔷 Base de données (Local)

- Télécharger et Installer Docker Desktop (Windows) : https://docs.docker.com/get-started/introduction/get-docker-desktop/
- Lancer Docker Desktop
```bash
# - Création du fichier ./DockerDbRedis/.env
```
```bash
# - Contenu de ./DockerDbRedis/.env :
MYSQL_ROOT_PASSWORD=ToUpDaTePaSsWoRd
MYSQL_DATABASE=ToUpDaTeDaTaBaSe
```
```bash
# - Création du fichier ./DockerDbRedis/docker/mysql-init/script-healthcheck-users-grants.sql
```
```bash
# - Contenu de ./DockerDbRedis/docker/mysql-init/script-healthcheck-users-grants.sql :
-- Creation base de données HealthCheck
CREATE DATABASE IF NOT EXISTS healthchecks_ui_db
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

-- Utilisateur avec droits SELECT
CREATE USER IF NOT EXISTS 'ToUpDaTeUsErReAdOnLy'@'%' IDENTIFIED BY '3JiaU9k3LGza4676b3eNPTWSZkZ4Y8vdw46p23iN';
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'ToUpDaTeUsErReAdOnLy'@'%';
REVOKE SHOW DATABASES ON *.* FROM 'ToUpDaTeUsErReAdOnLy'@'%';
GRANT SELECT ON ToUpDaTeDaTaBaSe.* TO 'ToUpDaTeUsErReAdOnLy'@'%';
GRANT SELECT ON healthchecks_ui_db.* TO 'ToUpDaTeUsErReAdOnLy'@'%';

-- Utilisateur avec droits SELECT, INSERT, UPDATE, DELETE
CREATE USER IF NOT EXISTS 'ToUpDaTeUsErReAdWrite'@'%' IDENTIFIED BY 'd7U33GYk48WSwk6L4b2i36J9vPz3Zae4pT6iaNZN';
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'ToUpDaTeUsErReAdWrite'@'%';
REVOKE SHOW DATABASES ON *.* FROM 'ToUpDaTeUsErReAdWrite'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE ON ToUpDaTeDaTaBaSe.* TO 'ToUpDaTeUsErReAdWrite'@'%';

-- Utilisateur avec droits SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, DROP, INDEX
CREATE USER IF NOT EXISTS 'ToUpDaTeUsErReAdWriteAdMiN'@'%' IDENTIFIED BY 'G23wikWp8eTL34ab63z694kiZ6a3U7PN4SYvZdNJ';
REVOKE ALL PRIVILEGES, GRANT OPTION FROM 'ToUpDaTeUsErReAdWriteAdMiN'@'%';
REVOKE SHOW DATABASES ON *.* FROM 'ToUpDaTeUsErReAdWriteAdMiN'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, DROP, INDEX, REFERENCES ON ToUpDaTeDaTaBaSe.* TO 'ToUpDaTeUsErReAdWriteAdMiN'@'%';
GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, INDEX, REFERENCES ON healthchecks_ui_db.* TO 'ToUpDaTeUsErReAdWriteAdMiN'@'%';

FLUSH PRIVILEGES;
```
```bash
# - Ouvrir un terminal dans ./DockerDbRedis/
# - Lancer la commande :
$ docker compose up
```
```bash
# - Ouvrir un navigateur web et rendez-vous sur l'url :
$ http://localhost:8080
```
```bash
# - Vous arrivez sur l'interface de PhpMyAdmin, connectez-vous :
$ Utilisateur : root
# Ici, il faut renseigner la valeur de l'index "MYSQL_ROOT_PASSWORD" du .env
$ Mot de passe : ... 

# Note : On peut également se connecter avec les comptes créés dans le script sql "script-healthcheck-users-grants.sql"
```
```bash
# - Une fois connecté, vous verrez votre base de données créée et vide pour le moment (elle portera le nom correspondant à la valeur de l'index "MYSQL_DATABASE" du .env)
```
- Maintenant, allez au point "EFCore (Migration vers la base de données)" de ce README :-)

###### 🔷 Base de données (Préproduction OnRender)

- Se rendre sur [OnRender](https://onrender.com/)
    - Se connecter
    - Créer un nouveau projet (Postgres) :
        - Name : e-club-bdd
        - Database : eclubbdd
        - User : eclubuser
        - Region : Frankfurt (EU Central)
        - PostgreSQL Version : 17
        - Datadog Region : US1 (default)
        - Plan Option (Instance Type) : Free (Recommandé)

- Créer la base de données et attendre que la base se créée pour utiliser les valeurs dans appsettings.json du point suivant :-)

###### 🔷 EFCore (Migration vers la base de données)

```bash
# - Ouvrir un terminal dans ./InfrastructureEFCore/
# - Si jamais dotnet-ef n'est pas déjà installé, lancer la commande :
$ dotnet tool install --global dotnet-ef
# - Si jamais le fichier "InitCreateTables" n'existe pas dans /InfrastructureEFCore/Migrations, lancer la commande depuis ./InfrastructureEFCore/ :
$ dotnet ef migrations add InitCreateTables --output-dir Migrations --context AppDbContext
# - Pour persister le fichier de migration, lancer cette commande toujours depuis ./InfrastructureEFCore/ :
$ dotnet ef database update --context AppDbContext
# Note : A chaque modification des models (et donc de la base de données), il faudra ajouter une nouvelle migration via cette commande :
$ dotnet ef migrations add SecondMigrationName

# Note : A chaque changement de provider (MySQL / PostgreSQL) :
# - Supprimer le dossier ./InfrastructureEFCore/Migrations
# - Modifier variable "AspNetCore_Environment" dans ./EClubBackend/WebAPI/appsettings.json, secrets.json associé et dans ./InfrastructureEFCore/appsettings.json, secrets.json associé
# - dotnet ef migrations add InitCreateTables --output-dir InfrastructureEFCore/Migrations --context AppDbContext
# - dotnet ef database update --context AppDbContext
```

###### 🔷 EFCore (Variables d'environnements)

```bash
# Création du fichier ./InfrastructureEFCore/appsettings.json
```

```bash
# Contenu de ./InfrastructureEFCore/appsettings.json :
{
  "AspNetCore_Environment": "Development",
  "ConnectionStrings": {
    "MySqlLocalApi": "Server=localhost;Port=3306;Database=ToUpDaTeDaTaBaSe;User id=ToUpDaTeUsErReAdWrite;Password=d7U33GYk48WSwk6L4b2i36J9vPz3Zae4pT6iaNZN;Persistsecurityinfo=True",
    "MySqlLocalApiMigrations": "Server=localhost;Port=3306;Database=ToUpDaTeDaTaBaSe;User id=ToUpDaTeUsErReAdWriteAdMiN;Password=G23wikWp8eTL34ab63z694kiZ6a3U7PN4SYvZdNJ;Persistsecurityinfo=True",
    "MySqlLocalHealthCheck": "Server=localhost;Port=3306;Database=ToUpDaTeDaTaBaSe;User id=ToUpDaTeUsErReAdOnLy;Password=3JiaU9k3LGza4676b3eNPTWSZkZ4Y8vdw46p23iN;Persistsecurityinfo=True",
    "MySqlLocalHealthCheckUI": "Server=localhost;Port=3306;Database=healthchecks_ui_db;User Id=ToUpDaTeUsErReAdWriteAdMiN;Password=G23wikWp8eTL34ab63z694kiZ6a3U7PN4SYvZdNJ;Persistsecurityinfo=True",
    "PostgreSqlOnRenderPreprodApi": "Server=...;User id=...;Password=...;Database=...;Persistsecurityinfo=True",
    "Redis": "localhost:6379"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
# Si "AspNetCore_Environment" = Development, alors la chaine de connexion prise en compte est "ConnectionStrings:MySqlLocalApi"
# - Pour "ConnectionStrings:MySqlLocalApi", "Password" correspond à la valeur de l'index "MYSQL_PASSWORD" du .env (./DockerDbRedis/.env)
# - Pour "ConnectionStrings:MySqlLocalApi", "Database" correspond à la valeur de l'index "MYSQL_DATABASE" du .env (./DockerDbRedis/.env)

# Si "AspNetCore_Environment" = Preproduction, alors la chaine de connexion prise en compte est "ConnectionStrings:PostgreSqlOnRenderPreprodApi"

# "Redis":"localhost:6379" correspond à l'instance pour générer le cache au lieu d'appeler tout le temps la base de données
```

```bash
# Possibilité de créer un secret.json via l'IDE Visual Studio 2022
```

###### 🔷 E-Club Backend (Variables d'environnements)

```bash
# Création du fichier ./EClubBackend/WebAPI/appsettings.json
```

```bash
# Contenu de ./EClubBackend/WebAPI/appsettings.json :
{
  "AspNetCore_Environment": "Development",
  "Security": {
    "PasswordPepper": "u_9-*U6YzA-_mx9nK8Q9#k7T5V*!BP_i6#b!9!gG4GUt4ag4k2"
  },
  "ConnectionStrings": {
    "MySqlLocalApi": "Server=localhost;Port=3306;Database=ToUpDaTeDaTaBaSe;User id=ToUpDaTeUsErReAdWrite;Password=d7U33GYk48WSwk6L4b2i36J9vPz3Zae4pT6iaNZN;Persistsecurityinfo=True",
    "MySqlLocalApiMigrations": "Server=localhost;Port=3306;Database=ToUpDaTeDaTaBaSe;User id=ToUpDaTeUsErReAdWriteAdMiN;Password=G23wikWp8eTL34ab63z694kiZ6a3U7PN4SYvZdNJ;Persistsecurityinfo=True",
    "MySqlLocalHealthCheck": "Server=localhost;Port=3306;Database=ToUpDaTeDaTaBaSe;User id=ToUpDaTeUsErReAdOnLy;Password=3JiaU9k3LGza4676b3eNPTWSZkZ4Y8vdw46p23iN;Persistsecurityinfo=True",
    "MySqlLocalHealthCheckUI": "Server=localhost;Port=3306;Database=healthchecks_ui_db;User Id=ToUpDaTeUsErReAdWriteAdMiN;Password=G23wikWp8eTL34ab63z694kiZ6a3U7PN4SYvZdNJ;Persistsecurityinfo=True",
    "PostgreSqlOnRenderPreprodApi": "Server=...;User id=...;Password=...;Database=...;Persistsecurityinfo=True",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "Key": "AgEnErEr",
    "Issuer": "https://localhost:5123",
    "Audience": "http://localhost:4200",
    "Subject": "JwtSubject"
  },
  "Smtp": {
    "Host": "sandbox.smtp.mailtrap.io",
    "Port": 587,
    "UseStartTls": true,
    "UserName": "...",
    "FromName": "...",
    "FromAddress": "..."
  },
  "AllowedHosts": "localhost",
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200"
    ]
  },
  "EnableSwagger": "true",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
# Si "AspNetCore_Environment" = Development, alors la chaine de connexion prise en compte est "ConnectionStrings:MySqlLocalApi"
# - Pour "ConnectionStrings:MySqlLocalApi", "Password" correspond à la valeur de l'index "MYSQL_PASSWORD" du .env (./DockerDbRedis/.env)
# - Pour "ConnectionStrings:MySqlLocalApi", "Database" correspond à la valeur de l'index "MYSQL_DATABASE" du .env (./DockerDbRedis/.env)

# Si "AspNetCore_Environment" = Preproduction, alors la chaine de connexion prise en compte est "ConnectionStrings:PostgreSqlOnRenderPreprodApi"

# "Redis":"localhost:6379" correspond à l'instance pour générer le cache au lieu d'appeler tout le temps la base de données

# "Security":"PasswordPepper" correspond à la clé de hashage à générer

# - Pour "Jwt:Key", la valeur peut rester la même sans soucis
# - Pour "Jwt:Issuer", la valeur correspond à l'url backend (à modifier en fonction de local ou préproduction)
# - Pour "Jwt:Audience", la valeur correspond à l'url frontend (à modifier en fonction de local ou préproduction)
# - Pour "Jwt:Subject", la valeur peut rester la même sans soucis

# - Pour "Smtp", il faut utiliser les valeurs de connexion d'un service d'envoi d'email (en local, il est recommandé d'utiliser MailTrap.io)

# - Pour "Cors:AllowedOrigins", les valeurs correspondent aux urls autorisés à interagir avec l'Api web (en local, saisir l'url du projet frontend local, etc...)
```

```bash
# Possibilité de créer un secret.json via l'IDE Visual Studio 2022
```

###### 🔷 E-Club Test (Health Checks)

```bash
# Au lancement de l'API web (Local - Point suivant), "Accéder" au "Health Checks UI" pour vérifier que tous les endpoints soient "Healthy"
```

```bash
# Tout est configurable dans "EClubBackend/WebAPI/Extensions/HealthCheck/Extension/HealthCheckSwaggerExtension.cs"
```

## ⚫ Lancement de l'API web

#### 🔷 Local

Via Visual Studio 2022, cliquez sur la flêche verte sur les onglets du menu en haut (choisir le profil, configurable dans ./EClubBackend/Properties/launchSettings.json)

#### 🔷 Préproduction
- Se rendre sur [OnRender](https://onrender.com/)
    - Se connecter
    - Créer un nouveau projet (Web Service) :
        - Linker au repository https://github.com/osscoco/e-club-back
        - Name : e-club-back
        - Branch Git : master
        - Langue : Docker
        - Region : Frankfurt (EU Central)
        - Dockerfile Path : .
        - Instance Type (For hobby projects) : Free (recommandé)
        - Auto-Deploy : On Commit
        - Environment Variables : 
            - AllowedHosts : e-cl....onrender.com;e-cl....onrender.com
            - AspNetCore_Environment : Preproduction
            - ConnectionStrings__DefaultConnection : Server=localhost;User id=root;Password=...;Database=...;Persistsecurityinfo=True
            - ConnectionStrings__OnrenderPreprodConnection : Server=...;User id=...;Password=...;Database=...;Persistsecurityinfo=True
            - Cors__AllowedOrigins__0 : https://....onrender.com
            - EnableSwagger : true
            - Jwt__Audience : https://e-cl....onrender.tld
            - Jwt__Issuer : https://e-cl....onrender.tld
            - Jwt__Key : EPGEHPIRGHSFGIHVOHEHIRBPGSPTIGHEYGPISGESPGHESHGHSGS
            - Jwt__Subject : Subject
            - Logging__LogLevel__Default : Information
            - Logging__LogLevel__Microsoft.AspNetCore : Warning
            - Smtp__Host : ...
            - Smtp__Password : ...
            - Smtp__Port : ...
            - Smtp__Username : ...