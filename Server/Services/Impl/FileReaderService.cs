using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Services;

namespace Server.Services.Impl
{
    public class FileReaderService : IFileReader
    {
        public async Task<string> ReadFileAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }

}
