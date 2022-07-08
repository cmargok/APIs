using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GenericResponseModel
    {
        public bool Succcess { get; set; }
        public string ErrorNumber { get; set; }
        public string ErrorDetail { get; set; }
        public int NumberOfRecords { get; set; }
    }
}
