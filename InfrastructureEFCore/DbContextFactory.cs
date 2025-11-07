using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfrastructureEFCore
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            //Récupération: app.settings.json / secret.json
            var cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets<DbContextFactory>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            //Environnement = Development (local) || Preproduction (onrender) || Production (...)
            var env = cfg.GetSection("AspNetCore_Environment");

            //Si var env = Development ? Alors var cs = Get "Chaine de connexion bdd "MySqlLocalApiMigrations" (local)
            //Si var env = Preproduction ? Alors var cs = Get "Chaine de connexion bdd "PostgreSqlOnRenderPreprodApi" (onrender)
            //Sinon var cs = Get "Chaine de connexion bdd "ProdConnection" (...)
            var cs = env.Value == "Development" ? cfg.GetConnectionString("MySqlLocalApiMigrations") : env.Value == "Preproduction" ? cfg.GetConnectionString("PostgreSqlOnRenderPreprodApi") : cfg.GetConnectionString("ProdConnection");

            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException(
                    "Aucune chaîne de connexion trouvée. " +
                    "Définis ConnectionStrings__MySqlLocalApiMigrations (ENV) ou ConnectionStrings__PostgreSqlOnRenderPreprodApi (ENV) ou ConnectionStrings__ProdConnection ou le tout dans secret.json (local) ...");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            //Si var env = Development ? Alors Moteur de bdd : MySQL 
            if (env.Value == "Development")
            {
                optionsBuilder.UseMySql(cs, ServerVersion.AutoDetect(cs));
            }
            //Si var env = Preproduction ? Alors Moteur de bdd : PostgreSQL 
            else if (env.Value == "Preproduction")
            {
                optionsBuilder.UseNpgsql(cs);
            }
            //Sinon Moteur de bdd : PostgreSQL
            else
            {
                optionsBuilder.UseNpgsql(cs);
            }

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
