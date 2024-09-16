using System;
using System.Collections.Generic;
using System.Text;

namespace RPN_Recreate.WPF.Models
{
    class ModelResult
    {
        public ModelResult(string range, string results)
        {
            this.Range = range;
            this.Results = results;
        }
        public string Range { get; set; }
        public string Results { get; set; }
    }
}
