namespace SampleApps.Wpf.Services
{
    using System.Collections.Generic;

    using SampleApps.Wpf.Model;

    public interface ISampleService
    {
        IEnumerable<Person> GetPeople();
    }
}