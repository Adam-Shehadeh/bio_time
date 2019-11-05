using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bio_time.Models {
    public class ConfigModel {
        public string ApplicationName { get; set; }
        public string Developer { get; set; }
        public double Rate { get; set; }
        public List<ContractModel> Contracts { get; set; }
        public int LastSelectedContractIndex { get; set; }
    }
    public class ContractModel {
        public int Index { get; set; }
        public string ClientName { get; set; }
        public string ContractTitle { get; set; }
    }
}
