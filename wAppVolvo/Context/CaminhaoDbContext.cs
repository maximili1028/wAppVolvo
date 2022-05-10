using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace wAppVolvo.Context
{
    public class CaminhaoDbContext : DbContext
    {

        public CaminhaoDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Caminhao> Caminhoes { get; set; }

    }
}
