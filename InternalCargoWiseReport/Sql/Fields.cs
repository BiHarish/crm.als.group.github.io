using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class LongReturn
    {
        public Exception ex { get; internal set; }
        public long value { get; internal set; }
    }

    public class BoolReturn
    {
        public Exception ex { get; internal set; }
        public bool value { get; internal set; }
    }

    public class DataSetReturn
    {
        public Exception ex { get; internal set; }
        public DataSet ds { get; internal set; }
    }
}
