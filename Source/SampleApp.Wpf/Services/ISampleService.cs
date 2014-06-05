using System.Collections.Generic;
using StyleMVVM.DotNet.TestApp.Model;

namespace StyleMVVM.DotNet.TestApp.Services
{
    public interface ISampleService
    {
        IEnumerable<Person> GetPeople();
    }
}