using BrazilianHedgeFoundsRetriever.BusinessRules.Interfaces;
using BrazilianHedgeFoundsRetriever.Entities;
using BrazilianHedgeFoundsRetriever.Entities.Requests;
using BrazilianHedgeFoundsRetriever.Entities.Responses;
using BrazilianHedgeFoundsRetriever.Repositories.Base;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BrazilianHedgeFoundsRetriever.BusinessRules.Implementors
{
    public class DataImplementor : IDataInterface
    {
        private readonly IConfiguration _configuration;
        private readonly MongoDBRepositoryBase<HedgeFoundData> _mongoDBRepositoryBase;
        public DataImplementor(IConfiguration configuration, MongoDBRepositoryBase<HedgeFoundData> mongoDBRepositoryBase)
        {
            _configuration = configuration;
            _mongoDBRepositoryBase = mongoDBRepositoryBase;
        }


        public RetrieveHedgeFoundsDataByFilterResponse RetrieveHedgeFoundsDataByFilter(RetrieveHedgeFoundsDataByFilterRequest request)
        {
            return new RetrieveHedgeFoundsDataByFilterResponse
            {
                HedgeFoundDatas = _mongoDBRepositoryBase.GetByFilter(BuildMongoFilter(request))
            };
        }
        

        public void LoadData()
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int initialYear = _configuration.GetValue<int>(
                "DataImplementorConfigs:DataQueryInitialYear");
            int initialMonth = _configuration.GetValue<int>(
                "DataImplementorConfigs:DataQueryInitialMonth");

            for (int year = initialYear; year <= currentYear; year++)
            {
                for (int month = initialMonth; month <= (year == currentYear ? currentMonth : 12); month++)
                    {
                    ReadCVSandInsertDataIntoDB(DownloadCSVFile(year, month)); 
                }
            
            }

            DeleteCreatedLocalFolder();
        }


        private string DownloadCSVFile(int year, int month)
        {
            string urlBase = $@"{_configuration.GetValue<string>(
                "ConnectionStrings:DataSourceUrlBase")}";
            DirectoryInfo newLocalFolderPath = Directory.CreateDirectory($@"{_configuration.GetValue<string>(
                "DataImplementorConfigs:NewLocalFolderPathToSaveCSVs")}");
            string monthString = month.ToString().Length > 1 ? month.ToString() : $"0{month}";


            System.Net.WebClient client = new WebClient();
            string archiveName = $@"{_configuration.GetValue<string>("DataImplementorConfigs:CSVFileNameBase")}{year}{monthString}.csv";
            string urlWithArchiveName = urlBase + archiveName;
            var pathWithArchiveName = $@"{newLocalFolderPath.FullName}\{archiveName}";
            client.DownloadFile(urlWithArchiveName, pathWithArchiveName);

            return pathWithArchiveName;
        }

        private void ReadCVSandInsertDataIntoDB(string pathWithArchiveName)
        {
            using (StreamReader reader = new StreamReader(pathWithArchiveName))
            {
                CsvConfiguration config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
                using (CsvReader csv = new CsvReader(reader, config))
                {
                    List<HedgeFoundData> records = csv.GetRecords<HedgeFoundData>().ToList();

                    _mongoDBRepositoryBase.InsertMany(records);

                    csv.Dispose();
                }
                reader.Dispose();
                reader.Close();
            }

            DeleteUsedFile(pathWithArchiveName);
        }

        private void DeleteUsedFile(string pathWithArchiveName)
        {
            if (File.Exists(pathWithArchiveName))
            {
                File.Delete(pathWithArchiveName);
            }
        }

        private void DeleteCreatedLocalFolder()
        {
            DirectoryInfo newLocalFolderPath = Directory.CreateDirectory($@"{_configuration.GetValue<string>(
                "DataImplementorConfigs:NewLocalFolderPathToSaveCSVs")}");

            if (newLocalFolderPath.Exists)
            {
                newLocalFolderPath.Delete(true);
            }
        }

        private FilterDefinition<HedgeFoundData> BuildMongoFilter(RetrieveHedgeFoundsDataByFilterRequest request)
        {
            var builder = Builders<HedgeFoundData>.Filter;
            List<FilterDefinition<HedgeFoundData>> filterDefinitions = new List<FilterDefinition<HedgeFoundData>>();

            filterDefinitions.Add(builder.Eq(x => x.CNPJ_FUNDO, request.CNPJ));
            if (request.StartDate.HasValue && request.EndDate.HasValue)
                filterDefinitions.Add(builder.Gte(x => x.DT_COMPTC, request.StartDate.Value.Date) & (builder.Lt(x => x.DT_COMPTC, request.EndDate.Value.Date.AddDays(1))));
            else if (request.StartDate.HasValue)
                filterDefinitions.Add(builder.Gte(x => x.DT_COMPTC, request.StartDate.Value.Date));
            else if (request.EndDate.HasValue)
                filterDefinitions.Add(builder.Lt(x => x.DT_COMPTC, request.EndDate.Value.Date.AddDays(1)));

            var filterConcat = builder.And(filterDefinitions);
            return filterConcat;
        }


    }
}
