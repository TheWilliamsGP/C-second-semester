using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
   //Class created to to store the different variebles 
    public class Module
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public double SelfStudyHoursPerWeek { get; set; }
        public Dictionary<DateTime, double> StudyHoursRecord { get; set; } = new Dictionary<DateTime, double>();
        public Module() 
        { 

        }
    }
}
