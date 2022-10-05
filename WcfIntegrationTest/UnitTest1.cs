using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ServiceModel;

namespace WcfIntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string uristring = "net.pipe://localhost";
            var client = new ChannelFactory<ITestService>(
                    new NetNamedPipeBinding(), 
                    new EndpointAddress(uristring))
                .CreateChannel();
            
            using (ServiceHost host = new ServiceHost(typeof(TestService), new Uri(uristring)))
            {
                host.Open();
                var result = client.GetMessage();
                host.Close();

                Assert.AreEqual("Hello", result);
            }
        }
    }
   
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetMessage();
    }
    
    public class TestService : ITestService
    {
        public string GetMessage() => "Hello";
    }
}
