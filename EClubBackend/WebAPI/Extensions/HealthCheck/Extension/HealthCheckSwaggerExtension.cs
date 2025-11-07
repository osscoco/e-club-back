using DomainModels.Security;

namespace E_Club.WebAPI.Extensions.HealthCheck.Extension
{
    public static class HealthCheckSwaggerExtension
    {
        public static IHealthChecksBuilder AddApiEndpointsHealthChecks(this IHealthChecksBuilder builder, string baseUrl, IPasswordHasher passwordHasher)
        {
            // Base URL
            baseUrl = baseUrl.TrimEnd('/');

            // Swagger URL
            builder.AddUrlGroup(
                new Uri($"{baseUrl}/swagger/index.html"),
                name: "api-2-swagger-ui",
                tags: new[] { "swagger" });

            // API Auth Login URL
            builder.AddCheck(
            "api-3.1-auth-login",
            new HttpMethodHealthCheck(
                $"{baseUrl}/api/Auth/login",
                HttpMethod.Post,
                new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                }),
            tags: new[] { "api", "auth", "login" });

            // API Auth AuthMe URL
            builder.AddCheck(
            "api-3.2-auth-authme",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Auth/authMe",
                protectedMethod: HttpMethod.Get),
            tags: new[] { "api", "auth", "authMe" });

            // API Auth Logout URL
            builder.AddCheck(
            "api-3.3-auth-logout",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Auth/logout",
                protectedMethod: HttpMethod.Get),
            tags: new[] { "api", "auth", "logout" });

            // API Club GetClubs URL
            builder.AddCheck(
            "api-4.1-club-getClubs",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Club",
                protectedMethod: HttpMethod.Get),
            tags: new[] { "api", "club", "getClubs" });

            // API Club GetClubById URL
            builder.AddCheck(
            "api-4.2-club-getClubById",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Club/06fbf90f-9f72-4af0-9e05-2dac522f2c99",
                protectedMethod: HttpMethod.Get),
            tags: new[] { "api", "club", "getClubById" });

            // API Club CreateClub URL
            builder.AddCheck(
            "api-4.3-club-createClub",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Club",
                protectedMethod: HttpMethod.Post,
                new
                {
                    name = "Tennis Club - Test",
                    CA = 80000
                }),
            tags: new[] { "api", "club", "createClub" });

            // API Club UpdateClub URL
            builder.AddCheck(
            "api-4.4-club-updateClub",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Club/06fbf90f-9f72-4af0-9e05-2dac522f2c99",
                protectedMethod: HttpMethod.Put,
                new
                {
                    name = "Tennis Club - Test Modifié",
                    CA = 70000
                }),
            tags: new[] { "api", "club", "updateClub" });

            // API Club DeleteClub URL
            builder.AddCheck(
            "api-4.5-club-deleteClub",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/Club/06fbf90f-9f72-4af0-9e05-2dac522f2c99",
                protectedMethod: HttpMethod.Delete),
            tags: new[] { "api", "club", "deleteClub" });

            // API User GetUsers URL
            builder.AddCheck(
            "api-5.1-user-getUsers",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/User",
                protectedMethod: HttpMethod.Get),
            tags: new[] { "api", "user", "getUsers" });

            // API User GetUserById URL
            builder.AddCheck(
            "api-5.2-user-getUserById",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/User/a8a7adae-94de-4fe6-857f-38e917fd1b75",
                protectedMethod: HttpMethod.Get),
            tags: new[] { "api", "user", "getUserById" });

            // API User CreateUser URL
            builder.AddCheck(
            "api-5.3-user-createUser",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/User",
                protectedMethod: HttpMethod.Post,
                new
                {
                    firstName = "Prénom Test",
                    lastName = "Nom Test",
                    age = 25,
                    email = "prenomtest.nomtest@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE",
                    userType = 1,
                    phone = "0712345678",
                    clubId = "06fbf90f-9f72-4af0-9e05-2dac522f2c99"
                }),
            tags: new[] { "api", "user", "createUser" });

            // API User UpdateUser URL
            builder.AddCheck(
            "api-5.4-user-updateUser",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/User/a8a7adae-94de-4fe6-857f-38e917fd1b75",
                protectedMethod: HttpMethod.Put,
                new
                {
                    firstName = "Prénom Test",
                    lastName = "Nom Test",
                    age = 25,
                    email = "prenomtest.nomtest@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE",
                    userType = 1,
                    phone = "0712345678",
                    clubId = "06fbf90f-9f72-4af0-9e05-2dac522f2c99"
                }),
            tags: new[] { "api", "user", "updateUser" });

            // API User DeleteUser URL
            builder.AddCheck(
            "api-5.5-user-deleteUser",
            new AuthFlowHealthCheck(
                loginUrl: $"{baseUrl}/api/Auth/login",
                loginBody: new
                {
                    email = "prenom1.nom1@gmail.com",
                    passwordHashed = "4Bf2HH9Ee4pzig9b87VE"
                },
                protectedUrl: $"{baseUrl}/api/User/a8a7adae-94de-4fe6-857f-38e917fd1b75",
                protectedMethod: HttpMethod.Delete),
            tags: new[] { "api", "user", "deleteUser" });

            return builder;
        }
    }
}