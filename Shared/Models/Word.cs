using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Word
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Count { get; set; }
    }
}
