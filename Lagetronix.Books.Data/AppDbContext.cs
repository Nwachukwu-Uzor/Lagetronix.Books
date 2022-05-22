using Lagetronix.Books.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagetronix.Books.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { 
                    Id = Guid.Parse("eb652d14-d402-49ec-aed3-fd59e871934c"),
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    Name = "Epic",
                    Description = "A collection of epic books"
                },
                new Category { 
                    Id = Guid.Parse("a8f45c1b-5f0c-4cb1-91d1-6c7e3a9ef4f1"),
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-07-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-08-05T00:00:00"),
                    Name = "Thrillers",
                    Description = "A collection of thrilling books"
                },
                new Category { 
                    Id = Guid.Parse("b921974d-810a-49f1-9727-7f63dc1f5126"),
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    Name = "Comic",
                    Description = "A collection of comic books"
                },
                new Category { 
                    Id = Guid.Parse("6bba998e-244e-42d0-8396-595faba0b16b"),
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    Name = "Religion",
                    Description = "A collection of religious books"
                }

            );

            builder.Entity<Book>().HasData(
                new Book { 
                    Id = Guid.Parse("4f337ec3-856f-44ee-a827-b8a12db50a21"),
                    Title = "Batman Origin",
                    Author = "Mark Spence",
                    Description = "A batman origin story",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("b921974d-810a-49f1-9727-7f63dc1f5126"),
                    IsFavorite = true
                },
                new Book { 
                    Id = Guid.Parse("e35a0340-9465-49fb-b8c3-e26e7d35a699"),
                    Title = "Superman",
                    Author = "Mark Spence",
                    Description = "A superman origin story",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("b921974d-810a-49f1-9727-7f63dc1f5126"),
                    IsFavorite = false
                },
                new Book { 
                    Id = Guid.Parse("e363a054-c03b-4aa9-bdd2-e06c182e27ab"),
                    Title = "Avengers",
                    Author = "Tony Stark",
                    Description = "An avengers origin story",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("b921974d-810a-49f1-9727-7f63dc1f5126"),
                    IsFavorite = true
                },
                new Book { 
                    Id = Guid.Parse("48fbc05e-f37a-4c14-bdb9-48202736bbfb"),
                    Title = "Fast Lane",
                    Author = "Will Smith",
                    Description = "Fast lane stories",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("a8f45c1b-5f0c-4cb1-91d1-6c7e3a9ef4f1"),
                    IsFavorite = true
                },
                new Book { 
                    Id = Guid.Parse("dafbe805-d276-442f-95d3-f29ff853239b"),
                    Title = "Breaking Bad",
                    Author = "Markus Helm",
                    Description = "Breakin bad",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("a8f45c1b-5f0c-4cb1-91d1-6c7e3a9ef4f1"),
                    IsFavorite = false
                },
                new Book { 
                    Id = Guid.Parse("41b8d6bd-3406-4c45-82d0-6ac19534bef0"),
                    Title = "Find God",
                    Author = "Chris Rock",
                    Description = "Finding God",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("6bba998e-244e-42d0-8396-595faba0b16b"),
                    IsFavorite = false
                },
                new Book { 
                    Id = Guid.Parse("af9c43b0-177b-4e6e-99e4-f61c0b481cd1"),
                    Title = "Hard Facts",
                    Author = "Sarah O'Connor",
                    Description = "Hard facts",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("6bba998e-244e-42d0-8396-595faba0b16b"),
                    IsFavorite = true
                },
                new Book { 
                    Id = Guid.Parse("41c95d90-7b37-4f15-b090-01c6250f3a91"),
                    Title = "Arise",
                    Author = "Mary Stead",
                    Description = "A transformation story",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("eb652d14-d402-49ec-aed3-fd59e871934c"),
                    IsFavorite = false
                },
                new Book { 
                    Id = Guid.Parse("2b25f9e2-764d-48d9-8f7b-257ad9ee7f4f"),
                    Title = "The Fight",
                    Author = "Jennifer Newman",
                    Description = "Fighting to stay alive",
                    Status = 1,
                    CreatedAt = DateTime.Parse("2021-06-05T00:00:00"),
                    ModifiedOn = DateTime.Parse("2021-07-05T00:00:00"),
                    CategoryId = Guid.Parse("eb652d14-d402-49ec-aed3-fd59e871934c"),
                    IsFavorite = true
                }
            );
        }
    }
}
