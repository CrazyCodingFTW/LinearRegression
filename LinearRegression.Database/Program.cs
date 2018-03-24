using LinearRegression.Database.ModelAdapters;
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

            var au = AnalysisInformation.GetEntity<AnalysisInformation>(1);
            Console.WriteLine(au);
            //au.Title = "New title";
            //au.Save();
            //var ai = AnalysisInformation.GetEntity<AnalysisInformation>(1);

            //Console.WriteLine($"{ai.Id} {ai.Title} {ai.Descrioption} {ai.CreationDate.ToString()}");
            //var ad = ai.Data;
            //Console.WriteLine($"{ad.Id} {ad.XMeaning} {string.Join("-",ad.XData)} {ad.YMeaning} {string.Join("-",ad.YData)}");

            Console.Read();
        }
    }
}
