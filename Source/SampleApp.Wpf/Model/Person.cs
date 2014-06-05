using System.Collections.Generic;

namespace Samples.Wpf.Model
{
    public class Person
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public IEnumerable<House> OwnedHouses { get; set; }
    }
}