namespace SampleApps.Wpf.Services
{
    using System.Collections.Generic;

    using SampleApps.Wpf.Model;

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
                        },
                          new House
                        {
                            Surface = 130,
                        }
                    }
                },
            };
        }
    }
}