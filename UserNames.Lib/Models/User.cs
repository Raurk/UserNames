using UserNames.Lib.Extensions;

namespace UserNames.Lib.Models
{
    public class User
    {
        [IntValidator]
        public int id { get; set; }
        public string first { get; set; }
        public string last { get; set; }

        [IntValidator]
        public int age { get; set; }
        public string gender { get; set; }
    }
}
