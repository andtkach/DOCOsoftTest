using DOCOsoft.UserManagement.Domain.Entities;
using DOCOsoft.UserManagement.Persistence;

namespace DOCOsoft.UserManagement.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(DocoSoftDbContext context)
        {
            context.Users.Add(new User
            {
                FirstName = "Well",
                LastName = "Known",
                Email = "user@email.com",
            });
            
            context.SaveChanges();
        }
    }
}
