using System.Collections.Generic;
using System.Linq;

namespace UserNames.Lib
{
    public abstract class DataImporter<T> where T : class
    {
        protected List<T> Items { get; set; } = new List<T>();

        protected abstract void ReadData();
        protected abstract void FormatData();
        protected abstract IEnumerable<T> ValidateData();

        public List<T> Read()
        {
            this.ReadData();
            this.FormatData();
            return this.ValidateData().ToList();
        }
    }
}
