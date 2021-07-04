using System.Configuration;
using NUnit.Framework;

namespace Popupnik.Server.UnitTests.DataAccessTests
{
    /// <summary>
    /// Checks whether the app.config file is successfully loaded for separated test library
    /// </summary>
    [TestFixture]
    public class AppConfigTests
    {
        [Test]
        public void CheckConfigFile()
        {
            Assert.AreEqual("passed", ConfigurationManager.AppSettings["test"]);
        }
    }
}