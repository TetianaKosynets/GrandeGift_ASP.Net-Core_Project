using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Services
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Profile> TblProfile { get; set; }
        public DbSet<Address> TblAddress { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<Hamper> TblHamper { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server = (localdb)\MSSQLLocalDB; Database = GrandeGiftDB; Trusted_Connection = true;");
        }
    }
}
