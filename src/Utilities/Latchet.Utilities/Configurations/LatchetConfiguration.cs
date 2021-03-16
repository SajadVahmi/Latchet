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
        public string JsonSerializerTypeName { get; set; }
        public string ExcelSerializerTypeName { get; set; }
        public string UserInfoServiceTypeName { get; set; }
        public bool RegisterAutomapperProfiles { get; set; } = true;
        public string AssmblyNameForLoad { get; set; }
        public LatchetMessageBusConfiguration MessageBus { get; set; }
        public LatchetPullingPublisherConfiguration PullingPublisher { get; set; }
        public LatchetMessagingConfiguration Messageconsumer { get; set; }
        public EntityChangeInterception EntityChangeInterception { get; set; }
        public ApplicationEvents ApplicationEvents { get; set; }
        public LatchetTranslatorConfiguration Translator { get; set; }
        public LatchetSwaggerConfiguration Swagger { get; set; }
        public LatchetCachingConfiguration Caching { get; set; }
    }
}
