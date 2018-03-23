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

            //First the metadata is created
            var analysisInfo = new AnalysisInformation(DateTime.Now, "TestAnalysis", "Meant for testing");

            using (var db = new LinearRegressionDbContext())
            {
                //Then it is added to the DbSet and saved in order to get its Id incremented
                db.AnalysisInformationSet.Add(analysisInfo);
                db.SaveChanges();

                //After that the AnalysisData object is created. It will link the analysisInfo's id to itself
                var analysisData = new AnalysisData("XTest", xData, "YTest", yData, analysisInfo);

                //It also gets saved so its id gets incremented
                db.AnalysisDataSet.Add(analysisData);
                db.SaveChanges();

                //Finally we link the analysis data id to the analysisInfo and save the changes
                analysisInfo.AnalysisDataId = analysisData.Id;
                db.SaveChanges();

                Console.WriteLine(db.AnalysisDataSet.Count());

                var ai = db.AnalysisInformationSet.First();
                Console.WriteLine($"{ai.Id} {ai.GetDateTimeFromString().ToShortDateString()} {ai.Title} {ai.Descrioption} {ai.AnalysisDataId}");
                var ad = db.AnalysisDataSet.Find(ai.AnalysisDataId);
                Console.WriteLine($"{ad.Id} {ad.XMeaning} {ad.YMeaning}");
            }
        }
    }
}
