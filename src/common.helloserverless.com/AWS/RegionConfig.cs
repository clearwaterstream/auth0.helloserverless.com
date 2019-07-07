using Amazon;
using common.helloserverless.com.IoC;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace common.helloserverless.com.AWS
{
    public class RegionConfig
    {
        static readonly RegionEndpoint _currentRegion = null;

        static readonly ILogger<RegionConfig> logger = ServiceRegistrar.GetLogger<RegionConfig>();

        static RegionConfig()
        {
            var ec2Region = Amazon.Util.EC2InstanceMetadata.Region;

            // will be null for non-ec2 code
            if (ec2Region != null)
            {
                _currentRegion = ec2Region;

                return;
            }

            var regionName = ServiceRegistrar.Configuration["AWS_REGION"];

            if (string.IsNullOrEmpty(regionName))
                regionName = "us-east-1";

            logger.LogInformation($"using {regionName} region");

            _currentRegion = RegionEndpoint.GetBySystemName(regionName);
        }

        public static RegionEndpoint CurrentRegion => _currentRegion;
    }
}
