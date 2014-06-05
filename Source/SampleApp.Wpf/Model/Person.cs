using System.Collections.Generic;

namespace StyleMVVM.DotNet.TestApp.Model
{
    public class Person
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public IEnumerable<House> OwnedHouses { get; set; }
    }
}