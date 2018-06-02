using LinearRegression.App.Contracts;
using LinearRegression.App.Contracts.Services;
using LinearRegression.App.Models;
using LinearRegression.Database;
using LinearRegression.Database.ModelAdapters;
using LinearRegression.Database.ModelContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearRegression.App.ServiceAdapters
{
    public class DatabaseService : Service, IDatabaseService
    {
        private IModelController<LinearRegressionDbContext> controller;

        public DatabaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.controller = serviceProvider.GetService(typeof(IModelController<LinearRegressionDbContext>)) as IModelController<LinearRegressionDbContext>;
        }

        public long SaveEntity(IFullAnalysis<IAnalysisDataRow> analysisModel)
        {
            var analysisInformation = SaveAnalysisInformation(analysisModel);
            analysisModel.DatabaseId = analysisInformation.Id;

            SaveAnalysisData(analysisModel, analysisInformation);

            return analysisInformation.Id;
        }

        public IList<IAnalysisMetadata> GetAllEntities()
        {
            var allEntities = this.controller.GetAllEntities<AnalysisInformation>();

            var collection = new List<IAnalysisMetadata>();
            foreach (var entity in allEntities)
                collection.Add(new AnalysisMetadata(entity.Id, entity.Title, entity.Descrioption, entity.CreationDate));


            return collection;
        }

        private AnalysisInformation SaveAnalysisInformation(IAnalysisMetadata amd)
        {
            var ad = new AnalysisInformation(amd.CreationDate, amd.Title, amd.Description, this.controller);
            ad.Save();

            return ad;
        }

        private void SaveAnalysisData(IAnalysisData<IAnalysisDataRow> ad, AnalysisInformation ai)
        {
            var xArray = ad.Data.Select(d => d.X).ToArray();
            var yArray = ad.Data.Select(d => d.Y).ToArray();

            var adEntity = new AnalysisData(ad.XMeaning, xArray, ad.YMeaning, yArray, ai, this.controller);
            adEntity.Save();
        }
    }
}
