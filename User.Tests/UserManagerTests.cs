using System;
using System.Collections.Generic;
using System.Linq;
using UserNames.Lib;
using Xunit;

namespace User.Tests
{
    public class UserNameTest
    {
        public static IEnumerable<object[]> GetNullData()
        {
            List<object[]> users = new List<object[]>
            {
                new object[] { new List<UserNames.Lib.Models.User> { new UserNames.Lib.Models.User()}.ToList()}
            };

            return users;
        }

        public static IEnumerable<object[]> GetData(int numParams)
        {
            List<object[]> users = new List<object[]>
            {
                new object[] { new List<UserNames.Lib.Models.User> {
                    new UserNames.Lib.Models.User() {id = 53, first = "Bill", last = "Bryson", age = 23, gender = "M"},
                    new UserNames.Lib.Models.User() {id = 62, first = "John", last = "Travolta", age = 54, gender = "M"},
                    new UserNames.Lib.Models.User() {id = 41, first = "Frank", last = "Zappa", age = 23, gender = "M"},
                    new UserNames.Lib.Models.User() {id = 31, first = "Jill", last = "Scott", age = 66, gender = "M"},
                    new UserNames.Lib.Models.User() {id = 31, first = "Anna", last = "Meredith", age = 66, gender = "F"},
                    new UserNames.Lib.Models.User() {id = 31, first = "Janet", last = "Jackson", age = 66, gender = "F"}
                }.Take(numParams).ToList()}};
            return users;
        }

        [Theory]
        [MemberData(nameof(GetNullData))]
        public void GetNullUserThrowsException(List<UserNames.Lib.Models.User> users)
        {
            Action testCode = () => { new UserManager(users); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<ApplicationException>(ex);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetUserDoesNotThrowsException(List<UserNames.Lib.Models.User> users)
        {
            Action testCode = () => { new UserManager(users); };

            var ex = Record.Exception(testCode);

            Assert.Null(ex);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 40)]
        public void GetUserThrowsException(List<UserNames.Lib.Models.User> users)
        {
            Action testCode = () => { new UserManager(users); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetUserEdgeMinusTest(List<UserNames.Lib.Models.User> users)
        {
            UserManager manager = new UserManager(users);

            var user = manager.GetUser(-1);

            Assert.Null(user);
        }


        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetUserEdgeZeroTest(List<UserNames.Lib.Models.User> users)
        {
            UserManager manager = new UserManager(users);

            var user = manager.GetUser(0);

            Assert.Null(user);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetUserEdgeMaxTest(List<UserNames.Lib.Models.User> users)
        {
            UserManager manager = new UserManager(users);

            var user = manager.GetUser(int.MaxValue);

            Assert.Null(user);
        }


        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetUserExistsTest(List<UserNames.Lib.Models.User> users)
        {
            UserManager manager = new UserManager(users);

            var user = manager.GetUser(53);

            Assert.NotNull(user);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetUserByAgeExistsTest(List<UserNames.Lib.Models.User> users)
        {
            int age = 54;
            UserManager manager = new UserManager(users);

            var user = manager.GetUsersByAge(age);

            Assert.NotNull(user);
            Assert.True(user.First().age == age);
        }

        [Theory]
        [MemberData(nameof(GetData), parameters: 4)]
        public void GetGendersByAgeExistsTest(List<UserNames.Lib.Models.User> users)
        {
            UserManager manager = new UserManager(users);

            var userAge = manager.GetGendersByAge();

            Assert.NotNull(userAge);
            Assert.True(userAge.Count == 3);
        }

    }
}
