using System.Collections.Generic;
using Samples.Wpf.Model;

namespace Samples.Wpf.Services
{
    public class SampleService : ISampleService
    {
        public IEnumerable<Person> GetPeople()
        {
            return new List<Person>
            {
                new Person
                {
                    Name = "Ian",
                    Age = 29,
                    OwnedHouses = new List<House>
                    {
                        new House
                        {
                            Surface = 300,
                            Location = "Denver"
                        }
                    }
                },
                new Person
                {
                    Name = "JMN",
                    Age = 33,
                    OwnedHouses = new List<House>
                    {
                        new House
                        {
                            Surface = 80,
                            Location = "Madrid"
                        },
                          new House
                        {
                            Surface = 130,
                            Location = "Ciudad Real"
                        }
                    }
                },
            };
        }
    }
}