using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICWR.QueryInternal
{
    public class InnerOutResult
    {
        public Exception ex { get; set; }
        public DataTable dt { get; set; }

        public DataSet ds { get; set; }
    }
}
