using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetAtmo.Entities
{
    public class GetUser
    {
        public class Administrative
        {
            public string country { get; set; }
            public string reg_locale { get; set; }
            public string lang { get; set; }
            public int unit { get; set; }
            public int windunit { get; set; }
            public int pressureunit { get; set; }
            public int feel_like_algo { get; set; }
        }

        public class DateCreation
        {
            public int sec { get; set; }
            public int usec { get; set; }
        }

        public class Body
        {
            public string _id { get; set; }
            public Administrative administrative { get; set; }
            public DateCreation date_creation { get; set; }
            public List<string> devices { get; set; }
            public string mail { get; set; }
            public int timeline_not_read { get; set; }
        }


        public string status { get; set; }
        public Body body { get; set; }
        public double time_exec { get; set; }
        public int time_server { get; set; }
    }
}
