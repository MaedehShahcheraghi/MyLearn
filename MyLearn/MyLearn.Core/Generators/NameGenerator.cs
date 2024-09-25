using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Generators
{
    public class NameGenerator
    {
        public static string GenerateUnicCode()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
