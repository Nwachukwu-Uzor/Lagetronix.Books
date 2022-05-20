using System;

namespace Lagetronix.Books.Data.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    }
}   
