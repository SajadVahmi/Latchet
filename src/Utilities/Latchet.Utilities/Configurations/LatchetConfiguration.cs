using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latchet.Utilities.Configurations
{
    public class LatchetConfiguration
    {
        public string ServiceId { get; set; } = "DefaultServiceName";
        public LatchetPullingPublisherConfiguration PullingPublisher { get; set; }
        public LatchetMessagingConfiguration Messageconsumer { get; set; }
        public EntityChangeInterception EntityChangeInterception { get; set; }
        public ApplicationEvents ApplicationEvents { get; set; }
        public LatchetTranslatorConfiguration Translator { get; set; }
    }
}
