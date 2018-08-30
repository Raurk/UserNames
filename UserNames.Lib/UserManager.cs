using System;
using System.Collections.Generic;
using System.Linq;
using UserNames.Lib.Extensions;
using UserNames.Lib.Models;

namespace UserNames.Lib
{
    public class UserManager
    {
        readonly SortedDictionary<int, User> _users;

        public UserManager(List<User> userList)
        {
            foreach (var user in userList)
            {
                if (!user.AllPropertiesValid())
                {
                    throw new ApplicationException("Invalid User Data");
                }
            }            

            var dict = userList.ToDictionary(key => key.id, user => user);
            _users = new SortedDictionary<int, User>(dict);
        }

        public User GetUser(int id)
        {
            if (_users.TryGetValue(id, out var user))
            {
                return user;
            }

            Console.WriteLine("Invalid User ID");

            return null;
        }

        public IList<User> GetUsersByAge(int age)
        {
            return _users.Where(user => user.Value.age == age).Select(user => user.Value).OrderBy(name => name.last).ThenBy(name => name.first).ToList();
        }

        public IList<AgeCount> GetGendersByAge()
        {
            var results = from user in _users.Values
                group user.gender by user.age into g
                select new AgeCount(g.Key, g.Count(m => m == "M"), g.Count(m => m == "F"));

            return results.OrderBy(age => age.age).ToList();
        }

        public class AgeCount
        {
            public int age { get; }
            public int male { get; }
            public int female { get; }

            public AgeCount(int age, int male, int female)
            {
                this.age = age;
                this.male = male;
                this.female = female;
            }
        }
    }
}
