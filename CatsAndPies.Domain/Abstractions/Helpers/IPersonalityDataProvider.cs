using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Abstractions.Helpers
{
    public interface IPersonalityDataProvider
    {
        List<string> GetResponses(string personalityType);
        List<string> GetGreetings(string personalityType);
    }
}
