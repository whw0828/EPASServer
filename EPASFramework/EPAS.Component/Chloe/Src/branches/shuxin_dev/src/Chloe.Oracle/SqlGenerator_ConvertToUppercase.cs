using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Oracle
{
    class SqlGenerator_ConvertToUppercase : SqlGenerator
    {
        public override string SqlName(string name)
        {
            if (name == null)
                return name;

            return name.ToUpper();
        }
    }
}
