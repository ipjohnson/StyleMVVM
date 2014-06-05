namespace SampleApps.Wpf.Model
{
    using System.Collections.Generic;

    public class Person
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public IEnumerable<House> OwnedHouses { get; set; }
    }
}