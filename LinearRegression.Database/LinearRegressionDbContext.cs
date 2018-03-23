using LinearRegression.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Made by the example: https://www.onceinawhitemoon.net/2017/05/wpf-entityframeworkcore-and-sqlite/
namespace LinearRegression.Database
{
    internal class LinearRegressionDbContext : DbContext
    {
        internal DbSet<Analysis> AnalysisSet { get; set; }

        public LinearRegressionDbContext()
        {
            //If new data is added use the command: 'Add-Migration InitialCreate' in the nuget package manager console
            SQLitePCL.Batteries.Init();
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=AnalysisDatabase.sqlite");
        }
    }
}
