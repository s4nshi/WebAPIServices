using WebAPI.Services;
using System.Collections.Generic;

namespace AdPlatformServiceTests
{
    public class AdPlatformServiceTests
    {
        private readonly IAdPlatformService _service;

        public AdPlatformServiceTests()
        {
            _service = new AdPlatformService();
        }

        [Fact]
        public void TestLoadAndSearch()
        {
            var content = @"Яндекс.Директ:/ru
            Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
            Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
            Крутая реклама:/ru/svrd";

            _service.LoadFromFile(content).GetAwaiter().GetResult();

            // Тест для /ru
            var resultRu = _service.SearchByLocation("/ru");
            Assert.Single(resultRu);
            Assert.Contains("Яндекс.Директ", resultRu);

            // Тест для /ru/svrd
            var resultSvrd = _service.SearchByLocation("/ru/svrd");
            Assert.Equal(2, resultSvrd.Count());
            Assert.Contains("Яндекс.Директ", resultSvrd);
            Assert.Contains("Крутая реклама", resultSvrd);

            // Тест для /ru/svrd/revda
            var resultRevda = _service.SearchByLocation("/ru/svrd/revda");
            Assert.Equal(3, resultRevda.Count());
            Assert.Contains("Яндекс.Директ", resultRevda);
            Assert.Contains("Крутая реклама", resultRevda);
            Assert.Contains("Ревдинский рабочий", resultRevda);
        }

        [Fact]
        public void TestEmptyLocation()
        {
            var result = _service.SearchByLocation("");
            Assert.Empty(result);
        }

        [Fact]
        public void TestNonExistingLocation()
        {
            var result = _service.SearchByLocation("/non/existing");
            Assert.Empty(result);
        }
    }
}
