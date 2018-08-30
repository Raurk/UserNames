using System;
using UserNames.Lib;
using UserNames.Lib.Models;

namespace UserNames
{
    class Program
    {
        static void Main(string[] args)
        {
            DataImporter<User> importer = new JsonImporter<User>();
            var userList = importer.Read();

            UserManager manager = new UserManager(userList);

            // The users full name for id=42
            var user1 = manager.GetUser(42);
            if (user1 != null)
            {
                Console.WriteLine("{0} {1}", user1.first, user1.last);
            }

            Console.WriteLine(Environment.NewLine);

            // All the users first names (comma separated) who are 23
            var usersByAge = manager.GetUsersByAge(23);
            int cnt = 0;
            foreach (var user in usersByAge)
            {
                if (cnt++ > 0)
                {
                    Console.Write(", ");
                }
                Console.Write(user.first);
            }

            Console.WriteLine(Environment.NewLine);

            // The number of genders per Age, displayed from youngest to oldest
            var gendersByAge = manager.GetGendersByAge();
            foreach (var result in gendersByAge)
            {
                Console.WriteLine("Age : {0} Female: {1} Male:{2}", result.age, result.female, result.male);
            }

            Console.ReadLine();
        }
    }
}

