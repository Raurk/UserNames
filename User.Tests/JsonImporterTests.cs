using System.Collections.Generic;
using UserNames.Lib;
using Xunit;

namespace User.Tests
{
    public class JsonImporterTests
    {
        [Fact(Skip = "Integration")]
        public void ReadDataTest()
        {
            JsonImporter<UserNames.Lib.Models.User> importer = new JsonImporter<UserNames.Lib.Models.User>();

            List<UserNames.Lib.Models.User> users = importer.Read();

            Assert.NotNull(users);
        }

    }
}
