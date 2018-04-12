using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database.ModelContracts
{
    public interface ISaveRemoveable <TDbContext> where TDbContext : DbContext
    {
        void Save();
        void Save(TDbContext db);

        void Delete();
        void Delete(TDbContext db);
    }
}
