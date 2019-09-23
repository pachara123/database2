using System;
using System.Collections.Generic;
using System.Text;
using databasr2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace databasr2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<book> books { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
