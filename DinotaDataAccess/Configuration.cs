﻿using Dinota.Core;

namespace Dinota.DataAccces
{
    public class Configuration : ConfigurationBase
    {
        private static Configuration _configuration;

        private Configuration()
        {
        }
        
        public static Configuration Instance
        {
            get { return _configuration ?? (_configuration = new Configuration()); }
        }
    }
}
