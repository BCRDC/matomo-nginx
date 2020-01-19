using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Host.Models
{

    [DataContract]
    public class EWei
    {
        [DataMember(Name = "paClientId")]
        public string PaClientId { get; set; }

        [DataMember(Name = "paResourceUri")]
        public string PaResourceUri { get; set; }

        [DataMember(Name = "paKey")]
        public string PaKey { get; set; }

        // PaAuthority
        [DataMember(Name = "paAuthority")]
        public string PaAuthority { get; set; }

        [DataMember(Name = "platformUrl")]
        public string PlatformUrl { get; set; }
    }


    [DataContract]
    public class EWeiFront
    {
        [DataMember(Name = "accessToken")]
        public string AccessToken { get; set; }

        [DataMember(Name = "platformUrl")]
        public string PlatformUrl { get; set; }
    }
}
