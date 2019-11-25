using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bio_time.Models
{
    public class TimeLogFile
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string FileContent { get; set; }
        public DateTime LastWrite { get; set; }
    }
}
