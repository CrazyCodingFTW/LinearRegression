using LinearRegression.Database.ModelAdapters;
using LinearRegression.Database.ModelContracts;
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
            //C.R.U.D. Example:

            //To make queries to the database we instantiate the Model Adapter Controller and we give it the namespace where the models are and the DbContext subclass
            var controller = new ModelController<LinearRegressionDbContext>("LinearRegression.Database.Model");

            //X and Y data for two entities
            var x = new double[] { 1, 2, 3, 4, 5 };  //can be any kind of Enumerable collection
            var y = new double[] { 6, 7, 8, 9, 10 };

            var x1 = new double[] { 1.1, 2.2, 3.3, 4.4, 5.5 };
            var y1 = new double[] { 6.6, 7.7, 8.8, 9.9, 10.10 };

            //Creating the entities.

            //First we create the AnalysisInformation entity. It is important to mention that we use Database.ModelAdapters not Database.Model
            var ai1 = new AnalysisInformation(DateTime.Now, "Analysis1", "Test entity", controller);
            var ai2 = new AnalysisInformation(DateTime.Now, "Analysis2", "Test entity", controller);

            //IT IS A MUST TO SAVE ALL THE CREATED ENTITIES RIGHT AFTER CREATION!!!!
            //That way they get their ID incremented
            ai1.Save();
            ai2.Save();

            //Right after that we are able to link the AnalysisData to the AnalysisInformation. For the perpouse we again use the class from Database.ModelAdapters
            var ad1 = new AnalysisData("X", x, "Y", y, ai1, controller);
            var ad2 = new AnalysisData("X", x1, "Y", y1, ai2, controller);

            //After that we immediately save the changes
            ad1.Save();
            ad2.Save();

            //Data can have comments added to it
            var comment = new Comment(ai1.Id, "Benchi", "Sednal na edno druvo", controller);
            comment.Save();

            //Get all the entities from analysis information
            var allInformation = controller.GetAllEntities<AnalysisInformation>();

            //To retrieve comments just ask for them
            Console.WriteLine(allInformation.First().Comments.First().Content);

            //Getting an item from the database.
            //We can get an item by its ID
            var firstEntityId = controller.GetAllEntities<AnalysisInformation>().First().Id; //As generic parameters we pass the Model Adapter class and the Model class itself
            var itemById = controller.GetEntityById<AnalysisInformation>(firstEntityId);

            //Each item keeps relation with its analysis data
            var itemByIdData = itemById.Data;

            DisplayData(itemById, itemByIdData);

            //We can also use a function to find an item
            var anInfWhere = controller.FindEntity<AnalysisInformation>(ai => ((Model.AnalysisInformation)ai).Title == "Analysis2");
            DisplayData(anInfWhere, anInfWhere.Data);


            //And we can get all the items from the database
            var collection = controller.GetAllEntities<AnalysisData>();
            foreach (var entity in collection)
                DisplayData(entity.AnalysisInformation, entity as AnalysisData);

            //Finally but not least we can delete items like this
            controller.DeleteAllEntities(allInformation);

            //It is important to mention that AnalysisData CANNOT be directly deleted!
        }

        public static void DisplayData(AnalysisInformation ai, AnalysisData ad)
        {
            Console.WriteLine($"{ai.Id} {ai.CreationDate.ToString()} {ai.Title} {ai.Descrioption}\n" +
                $"{ad.Id} {ad.XMeaning} [{string.Join(", ", ad.XData)}] {ad.YMeaning} [{string.Join(", ", ad.YData)}]");
        }
    }
}
