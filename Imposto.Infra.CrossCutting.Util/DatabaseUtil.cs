using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Infra.CrossCutting.Util
{
    public static class DatabaseUtil
    {
        public static object GetValueOrDBNull(this object value)
        {
            return value == null ? DBNull.Value : (object)value;
        }
    }
}
