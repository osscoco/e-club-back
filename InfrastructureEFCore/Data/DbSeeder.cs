using DomainModels.Entities;
using DomainModels.Enums;
using DomainModels.Security;
using InfrastructureEFCore;

public static class DbSeeder
{
    public static void SeedDatas(AppDbContext context, IPasswordHasher passwordHasher)
    {
        if (!context.Clubs.Any() && !context.Courts.Any() && !context.Users.Any())
        {
            // Clubs
            var club1Id = Guid.NewGuid();
            var club2Id = Guid.NewGuid();
            var club3Id = Guid.NewGuid();

            var clubs = new List<Club>
            {
                new Club { ClubId = club1Id, Name = "Tennis Club - Laval", CA = 100000 },
                new Club { ClubId = club2Id, Name = "Tennis Club - Saint Berthevin", CA = 90000 },
                new Club { ClubId = club3Id, Name = "Tennis Club - L'Huisserie", CA = 80000 }
            };

            context.Clubs.AddRange(clubs);

            context.SaveChanges();

            // Courts
            var court1Id = Guid.NewGuid();
            var court2Id = Guid.NewGuid();
            var court3Id = Guid.NewGuid();
            var court4Id = Guid.NewGuid();
            var court5Id = Guid.NewGuid();
            var court6Id = Guid.NewGuid();
            var court7Id = Guid.NewGuid();
            var court8Id = Guid.NewGuid();
            var court9Id = Guid.NewGuid();
            var court10Id = Guid.NewGuid();
            var court11Id = Guid.NewGuid();
            var court12Id = Guid.NewGuid();

            var courts = new List<Court>
            {
                new Court { CourtId = court1Id, Name = "Court 1 - Laval", ClubId = club1Id },
                new Court { CourtId = court2Id, Name = "Court 2 - Laval", ClubId = club1Id },
                new Court { CourtId = court3Id, Name = "Court 3 - Laval", ClubId = club1Id },
                new Court { CourtId = court4Id, Name = "Court 4 - Laval", ClubId = club1Id },
                new Court { CourtId = court5Id, Name = "Court 1 - Saint Berthevin", ClubId = club2Id },
                new Court { CourtId = court6Id, Name = "Court 2 - Saint Berthevin", ClubId = club2Id },
                new Court { CourtId = court7Id, Name = "Court 3 - Saint Berthevin", ClubId = club2Id },
                new Court { CourtId = court8Id, Name = "Court 4 - Saint Berthevin", ClubId = club2Id },
                new Court { CourtId = court9Id, Name = "Court 1 - L'Huisserie", ClubId = club3Id },
                new Court { CourtId = court10Id, Name = "Court 2 - L'Huisserie", ClubId = club3Id },
                new Court { CourtId = court11Id, Name = "Court 3 - L'Huisserie", ClubId = club3Id },
                new Court { CourtId = court12Id, Name = "Court 4 - L'Huisserie", ClubId = club3Id }
            };

            context.Courts.AddRange(courts);

            context.SaveChanges();

            // Types d'utilisateurs
            var userType1Id = Guid.NewGuid();
            var userType2Id = Guid.NewGuid();
            var userType3Id = Guid.NewGuid();
            var userType4Id = Guid.NewGuid();

            var userTypes = new List<UserType>
            {
                new UserType { UserTypeId = userType1Id, Name = "Player" },
                new UserType { UserTypeId = userType2Id, Name = "Coach" },
                new UserType { UserTypeId = userType3Id, Name = "Admin" },
                new UserType { UserTypeId = userType4Id, Name = "Undefined" }
            };

            context.UserTypes.AddRange(userTypes);

            context.SaveChanges();

            // Utilisateurs
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            var user3Id = Guid.NewGuid(); 
            var user4Id = Guid.NewGuid();

            var users = new List<User>
            {
                new User { UserId = user1Id, FirstName = "Prénom 1", LastName = "Nom 1", Age = 20, Email = "prenom1.nom1@gmail.com", PasswordHashed = passwordHasher.Hash("4Bf2HH9Ee4pzig9b87VE"), UserTypeId = userType1Id, Phone = "0712345601", ClubId = club1Id },
                new User { UserId = user2Id, FirstName = "Prénom 2", LastName = "Nom 2", Age = 21, Email = "prenom2.nom2@gmail.com", PasswordHashed = passwordHasher.Hash("HBE87VH44izg2E9p9ebf"), UserTypeId = userType1Id, Phone = "0712345602", ClubId = club1Id },
                new User { UserId = user3Id, FirstName = "Prénom 3", LastName = "Nom 3", Age = 22, Email = "prenom3.nom3@gmail.com", PasswordHashed = passwordHasher.Hash("9HE2g4Vz4HEi7B8pfe9b"), UserTypeId = userType2Id, Phone = "0712345602", ClubId = club1Id },
                new User { UserId = user4Id, FirstName = "Prénom 4", LastName = "Nom 4", Age = 23, Email = "prenom4.nom4@gmail.com", PasswordHashed = passwordHasher.Hash("74Ei8e9p2bHBgH94EVfz"), UserTypeId = userType3Id, Phone = "0712345604", ClubId = club1Id }
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}