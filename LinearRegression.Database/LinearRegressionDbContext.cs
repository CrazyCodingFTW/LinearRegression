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
    public class LinearRegressionDbContext : DbContext
    {
        //When adding new set please DO NAME the set as follows: [NameOfTheModelClass]Set
        public DbSet<AnalysisData> AnalysisDataSet { get; set; }
        public DbSet<AnalysisInformation> AnalysisInformationSet {get;set;}

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
