
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Employees
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {
            
        }
        public DbSet<Employ> Employ => Set<Employ>();
    }
} 
