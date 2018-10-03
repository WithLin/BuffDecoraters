using System;
using Serilog;

namespace Decorate.Sample
{
    public interface ITestService
    {
        //[ATest]
        String Test(String name);

        [BTest("ITestService.Test")]
        String PipeTest(String name);
    }


    public class TestService : ITestService
    {
        private readonly ILogger _logger;

        public TestService(ILogger logger)
        {
            _logger = logger;
        }


        public String Test(String name)
        {
            _logger.Information($"TestService_Test_Name:{name}");
            return name;
        }

        [ATest]
        [BTest("Inherited and Change BTestAttribute vaule")]
        public string PipeTest(string name)
        {
            _logger.Information($"TestService_PipeTest_Name:{name}");
            return name;
        }
    }
}