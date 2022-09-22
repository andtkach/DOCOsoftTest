using System;
using DOCOsoft.UserManagement.Domain.Common;

namespace DOCOsoft.UserManagement.Domain.Entities
{
    public class User: AuditableEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
