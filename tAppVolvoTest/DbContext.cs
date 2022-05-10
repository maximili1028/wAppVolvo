using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using wAppVolvo.Context;

namespace tAppVolvoTest
{
    internal class DbContext
    {
        public CaminhaoDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CaminhaoDbContext>();
            optionsBuilder.UseSqlServer(
              "Server=(localdb)\\mssqllocaldb; Database=VolvoDB;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new CaminhaoDbContext(optionsBuilder.Options);
        }
    }
}
