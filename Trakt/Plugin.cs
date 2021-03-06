﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using Trakt.Configuration;

namespace Trakt
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public SemaphoreSlim TraktResourcePool = new SemaphoreSlim(1, 1);

        public Plugin(IApplicationPaths appPaths, IXmlSerializer xmlSerializer)
            : base(appPaths, xmlSerializer)
        {
            Instance = this;
            PollingTasks = new Dictionary<string, Task<bool>>();
        }

        public override string Name => "Trakt";

        private Guid _id = new Guid("4fe3201e-d6ae-4f2e-8917-e12bda571281");
        public override Guid Id
        {
            get { return _id; }
        }

        public override string Description
            => "Watch, rate and discover media using Trakt. The htpc just got more social";

        public static Plugin Instance { get; private set; }

        public PluginConfiguration PluginConfiguration => Configuration;

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "Trakt",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.configPage.html"
                }
            };
        }
        public Dictionary<string, Task<bool>> PollingTasks { get; set; }
    }
}
