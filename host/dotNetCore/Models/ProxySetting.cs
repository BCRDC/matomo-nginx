using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Host.Models
{
    public class ProxySetting
    { 
        public string Name { get; set; }


        public string Url { get; set; }
    }

    public class ProxySettings
    {
        public ProxySetting[] Values { get; set; }

    }

}
