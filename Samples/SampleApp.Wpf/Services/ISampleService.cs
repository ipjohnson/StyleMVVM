using System.Collections.Generic;
using Samples.Wpf.Model;

namespace Samples.Wpf.Services
{
    public interface ISampleService
    {
        IEnumerable<Person> GetPeople();
    }
}