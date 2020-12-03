using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EchoCardiologyDS.Models
{

    public class ComboBoxConfiguration
    {
        public IEnumerable<string> DegreeRegurgitation { get => GetDegree(); }
        private IEnumerable<string> GetDegree()
        {
            string result = "0,нез,неб,1,2,3,4,5,6";
            return result.Split(',');
        }
    }
 
}
