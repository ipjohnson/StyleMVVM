using System.Collections.Generic;
using System.Windows.Media;

namespace Samples.Wpf.Model
{
    public class Person
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public IEnumerable<House> OwnedHouses { get; set; }

        public ImageSource Picture { get; set; }
    }
}