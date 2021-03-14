using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Configurations
{
    public class LatchetConfigurations
    {
        public string ServiceId { get; set; } = "DefaultServiceName";
        public LatchetPullingPublisherConfigurations PullingPublisher { get; set; }
        public EntityChangeInterception EntityChangeInterception { get; set; }
        public ApplicationEvents ApplicationEvents { get; set; }
    }
}
