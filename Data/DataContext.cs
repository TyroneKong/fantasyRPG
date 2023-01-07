using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Character> Characters => Set<Character>();
    }
}