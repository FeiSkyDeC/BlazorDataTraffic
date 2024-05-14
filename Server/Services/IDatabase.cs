using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IDatabaseService
    {
        Task SaveWordCountsAsync(IDictionary<string, int> wordCounts);
    }
}
