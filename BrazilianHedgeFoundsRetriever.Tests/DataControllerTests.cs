using BrazilianHedgeFoundsRetriever.BusinessRules.Implementors;
using BrazilianHedgeFoundsRetriever.BusinessRules.Interfaces;
using BrazilianHedgeFoundsRetriever.Entities;
using BrazilianHedgeFoundsRetriever.Entities.Requests;
using BrazilianHedgeFoundsRetriever.Entities.Responses;
using BrazilianHedgeFoundsRetriever.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace BrazilianHedgeFoundsRetriever.Tests
{
    public class DataControllerTests
    {

        [Fact]
        public void RetrieveHedgeFoundsDataByFilterTest()
        {
            //Arrange
            RetrieveHedgeFoundsDataByFilterRequest onlyCNPJFilter = new RetrieveHedgeFoundsDataByFilterRequest()
            {
                CNPJ = "00.017.024/0001-53",
                StartDate = null,
                EndDate = null
            };

            RetrieveHedgeFoundsDataByFilterRequest onlyCNPJandStardDateFilter = new RetrieveHedgeFoundsDataByFilterRequest()
            {
                CNPJ = "00.017.024/0001-53",
                StartDate = new DateTime(2017, 1, 2),
                EndDate = null

            };

            RetrieveHedgeFoundsDataByFilterRequest onlyCNPJandEndDateFilter = new RetrieveHedgeFoundsDataByFilterRequest()
            {
                CNPJ = "00.017.024/0001-53",
                StartDate = null,
                EndDate = new DateTime(2017, 1, 4)

            };

            RetrieveHedgeFoundsDataByFilterRequest fullFilter = new RetrieveHedgeFoundsDataByFilterRequest()
            {
                CNPJ = "00.017.024/0001-53",
                StartDate = new DateTime(2017, 1, 2),
                EndDate = new DateTime(2017, 1, 4)

            };

            Moq.Mock<IDataInterface> onlyCNPJFilterMock = new Moq.Mock<IDataInterface>();
            onlyCNPJFilterMock.Setup(x => x.RetrieveHedgeFoundsDataByFilter(onlyCNPJFilter).HedgeFoundDatas.Count).Returns(1298);

            Moq.Mock<IDataInterface> onlyCNPJandStardDateFilterMock = new Moq.Mock<IDataInterface>();
            onlyCNPJandStardDateFilterMock.Setup(x => x.RetrieveHedgeFoundsDataByFilter(onlyCNPJandStardDateFilter).HedgeFoundDatas.Count).Returns(1297);

            Moq.Mock<IDataInterface> onlyCNPJandEndDateFilterMock = new Moq.Mock<IDataInterface>();
            onlyCNPJandEndDateFilterMock.Setup(x => x.RetrieveHedgeFoundsDataByFilter(onlyCNPJandEndDateFilter).HedgeFoundDatas.Count).Returns(3);

            Moq.Mock<IDataInterface> fullFilterMock = new Moq.Mock<IDataInterface>();
            fullFilterMock.Setup(x => x.RetrieveHedgeFoundsDataByFilter(fullFilter).HedgeFoundDatas.Count).Returns(3);

            Moq.Mock<IConfiguration> configurationMock = new Moq.Mock<IConfiguration>();
            Moq.Mock<MongoDBRepositoryBase<HedgeFoundData>> MongoDBRepositoryBaseMock = new Moq.Mock<MongoDBRepositoryBase<HedgeFoundData>>();

            DataImplementor dataImplementor = new DataImplementor(configurationMock.Object, MongoDBRepositoryBaseMock.Object);

            //Act
            int onlyCNPJFilterExpectedResult = onlyCNPJFilterMock.Object.RetrieveHedgeFoundsDataByFilter(onlyCNPJFilter).HedgeFoundDatas.Count;
            int onlyCNPJFilterResult = dataImplementor.RetrieveHedgeFoundsDataByFilter(onlyCNPJFilter).HedgeFoundDatas.Count;

            int onlyCNPJandStardDateFilterExpectedResult = onlyCNPJandStardDateFilterMock.Object.RetrieveHedgeFoundsDataByFilter(onlyCNPJandStardDateFilter).HedgeFoundDatas.Count;
            int onlyCNPJandStardDateFilterResult = dataImplementor.RetrieveHedgeFoundsDataByFilter(onlyCNPJandStardDateFilter).HedgeFoundDatas.Count;

            int onlyCNPJandEndDateFilterExpectedResult = onlyCNPJandEndDateFilterMock.Object.RetrieveHedgeFoundsDataByFilter(onlyCNPJandEndDateFilter).HedgeFoundDatas.Count;
            int onlyCNPJandEndDateFilterResult = dataImplementor.RetrieveHedgeFoundsDataByFilter(onlyCNPJandEndDateFilter).HedgeFoundDatas.Count;

            int fullFilterExpectedResult = fullFilterMock.Object.RetrieveHedgeFoundsDataByFilter(fullFilter).HedgeFoundDatas.Count;
            int fullFilterResult = dataImplementor.RetrieveHedgeFoundsDataByFilter(fullFilter).HedgeFoundDatas.Count;

            //Assert
            Assert.Equal(onlyCNPJFilterExpectedResult, onlyCNPJFilterResult);
            Assert.Equal(onlyCNPJandStardDateFilterExpectedResult, onlyCNPJandStardDateFilterResult);
            Assert.Equal(onlyCNPJandEndDateFilterExpectedResult, onlyCNPJandEndDateFilterResult);
            Assert.Equal(fullFilterExpectedResult, fullFilterResult);

        }
    }
}
