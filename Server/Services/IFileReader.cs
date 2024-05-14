using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string path);
    }
}
