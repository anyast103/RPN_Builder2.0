using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate.WPF.Models
{
    class ModelResult
    {
        public ModelResult(string range, string results)
        {
            this.range = range;
            this.results = results;
        }
        public string range { get; set; }
        public string results { get; set; }
    }
}
