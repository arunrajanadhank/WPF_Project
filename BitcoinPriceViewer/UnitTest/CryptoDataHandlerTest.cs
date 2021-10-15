using CryptoCurrencyDataCollector;
using CryptoCurrencyDataCollector.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CryptoCurrencyDataCollectorTests
{
    [TestClass]
    public class CryptoDataHandlerTest
    {
        private Mock<IDataSource> dataSourceMock;
        private Mock<IDataParser> parserMock;

        private readonly string jsonData = "{\"bpi\":{\"2021-09-14\":47112.1767,\"2021-09-15\":48139.485,\"2021-09-16\":47770.4467,}," +
                    "\"disclaimer\":\"This data was produced from the CoinDesk Bitcoin Price Index. BPI value data returned as USD.\"" +
                    ",\"time\":{\"updated\":\"Oct 15, 2021 00:03:00 UTC\",\"updatedISO\":\"2021-10-15T00:03:00+00:00\"}}";

        [TestInitialize]
        public void TestInitialize()
        {
            dataSourceMock = new Mock<IDataSource>();
            parserMock = new Mock<IDataParser>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            dataSourceMock = null;
            parserMock = null;
        }

        [TestMethod]
        public void CryptoDataHandler_GetClosingPricesTest_Success()
        {
            //Arrange
            JsonParser parser = new JsonParser();
            var resultData = parser.ParseData(jsonData);

            dataSourceMock.Setup(p => p.ReadDataFromSource(It.IsAny<string>())).Returns(jsonData);

            //Act
            CryptoDataHandler dataHandler = new CryptoDataHandler(dataSourceMock.Object, parser);
            var closingPrices = dataHandler.GetClosingPrices();

            var actualResult = JsonConvert.SerializeObject(closingPrices);
            var expectedResult = JsonConvert.SerializeObject(resultData);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void CryptoDataHandler_GetClosingPricesTest_DataModelNull()
        {
            //Arrange
            JsonParser parser = new JsonParser();
            dataSourceMock.Setup(p => p.ReadDataFromSource(It.IsAny<string>())).Returns(string.Empty);

            //Act
            CryptoDataHandler dataHandler = new CryptoDataHandler(dataSourceMock.Object, parser);
            var closingPrices = dataHandler.GetClosingPrices();

            //Assert
            Assert.IsNull(closingPrices, null);
        }

        [TestMethod]
        public void JsonParser_ParseDataTest_Success()
        {
            //Arrange
            JsonParser jsonParser = new JsonParser();
            CryptoDataModel expectedDataModel = new CryptoDataModel();
            expectedDataModel = JsonConvert.DeserializeObject<CryptoDataModel>(jsonData);

            parserMock.Setup(p => p.ParseData(It.IsAny<string>())).Returns(expectedDataModel);

            //Act
            var actualDataModel = jsonParser.ParseData(jsonData);

            var actualResult = JsonConvert.SerializeObject(actualDataModel);
            var expectedResult = JsonConvert.SerializeObject(expectedDataModel);

            //Assert
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void JsonParser_ParseDataTest_Failed()
        {
            //Arrange
            JsonParser jsonParser = new JsonParser();
            CryptoDataModel expectedDataModel = new CryptoDataModel();
            expectedDataModel = JsonConvert.DeserializeObject<CryptoDataModel>("");

            parserMock.Setup(p => p.ParseData(It.IsAny<string>())).Returns(expectedDataModel);

            //Act
            var actualDataModel = jsonParser.ParseData("InvalidJson");

            //Assert
            Assert.AreEqual(actualDataModel, expectedDataModel);
        }
    }
}
