using ProjectEssential.EnumLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    public class BMIViewModel
    {
        public string Name { get; set; }
        
        public int Height { get; set; }
        
        public int Weight { get; set; }
        
        public string Unit { get; set; }
        
        public double BMIValue { get; set; }
        
    }
}
