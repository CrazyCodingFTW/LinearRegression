using LinearRegression.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] xData = { 1, 2, 3, 4.5 };
            double[] yData = { 4.5, 3, 2, 1 };

            var a1 = new Analysis("A1","Test",DateTime.Now,"X",xData,"Y",yData);
            var a2 = new Analysis("A2", "Test", DateTime.Now, "X", xData, "Y", yData);

            using (var db = new LinearRegressionDbContext())
            {
                db.AnalysisSet.AddRange(a1, a2);
                db.SaveChanges();
            }

            var analysis = new List<Analysis>();
            using (var db = new LinearRegressionDbContext())
            {
                analysis = db.AnalysisSet.ToList();
            }

            foreach(var anal in analysis)
            {
                Console.WriteLine($"{anal.Id} {anal.Title} {anal.Descrioption} {anal.GetDateTimeFromString().ToString()} {anal.XMeaning} " +
                    $"{string.Join("-", anal.GetDataFromStringObject(Contracts.ModelContracts.DataType.X))} {anal.YMeaning} " +
                    $"{string.Join("-",anal.GetDataFromStringObject(Contracts.ModelContracts.DataType.Y))}");
            }
        }
    }
}
