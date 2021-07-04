using Popupnik.Server.Components.Model.Entities;
using Popupnik.Server.UnitTests.DataAccessTestsBase;
using NUnit.Framework;
using System.Linq;

namespace Popupnik.Server.UnitTests.DataAccessTests
{
    [TestFixture]
#if DEBUG
    public class EntityTests : DataAccessTestBase
#else
    public class EntityTests : DataAccessMockTestBase
#endif
    {
        [Test]
        public void SaveUserTest()
        {
            const string computerName = "COMPUTER NAME";
            const string displayName = "DISPLAY NAME";
            using (var firstClient = DbContextProvider.CreateContext())
            {
                using (var secondClient = DbContextProvider.CreateContext())
                {
                    var userQueryForFirstClient = firstClient.Entities<User>();
                    Assert.AreEqual(0, userQueryForFirstClient.Count());

                    var user = EntityFactory.CreateUser();
                    user.ComputerName = computerName;
                    user.DisplayName = displayName;
                    secondClient.Entities<User>().InsertOnSubmit(user);
                    Assert.AreEqual(0, userQueryForFirstClient.Count());

                    secondClient.SubmitChanges();
                    Assert.AreEqual(1, userQueryForFirstClient.Count());
                    Assert.AreEqual(1, (from User entry in userQueryForFirstClient
                                        where true
                                              && entry.ComputerName == computerName
                                              && entry.DisplayName == displayName
                                        select entry).Count()
                        );
                }
            }
        } 
        
        [Test]
        public void LoadTwoUsersFromSecondTest()
        {
            using (var client = DbContextProvider.CreateContext())
            {
                var userQuery = client.Entities<User>();
                Assert.AreEqual(0, userQuery.Count());

                var user1 = EntityFactory.CreateUser();
                user1.ComputerName = "1";
                user1.DisplayName = "1";
                var user2 = EntityFactory.CreateUser();
                user2.ComputerName = "2";
                user2.DisplayName = "2";
                var user3 = EntityFactory.CreateUser();
                user3.ComputerName = "3";
                user3.DisplayName = "3";
                var user4 = EntityFactory.CreateUser();
                user4.ComputerName = "4";
                user4.DisplayName = "4";
                client.Entities<User>().InsertAllOnSubmit(new[] {user1, user2, user3, user4});
                client.SubmitChanges();

                Assert.AreEqual(4, userQuery.Count());
                var array = userQuery.OrderBy(user => user.CreatedAt).Skip(1).Take(2).ToArray();

                Assert.AreEqual(2, array.Length);
                Assert.AreEqual("2", array[0].ComputerName);
                Assert.AreEqual("3", array[1].ComputerName);
            }
        }
    }
}