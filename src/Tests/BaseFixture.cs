﻿using System;
using Nancy.Testing;
using Isop.Server;
using Nancy.ViewEngines;
using Nancy.ViewEngines.Veil;

namespace Isop.Tests.Server
{
    public class BaseFixture
    {
        public BaseFixture ()
        {
        }

        public static Browser GetBrowser(Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> configuration=null, Action<BrowserContext> defaults=null)
        {
            return GetBrowser<FakeIsopServer>(configuration, defaults);
        }
        public static Browser GetBrowser<TISopServer>(Action<ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> configuration=null, Action<BrowserContext> defaults=null) where TISopServer : class, IIsopServer
        {
            var bootstrapper = new TestBootstrapperWithIsopServer<TISopServer>(with=>{
                with.DisableAutoRegistrations();
                with.Module<ControllerModule>();
                with.Module<IndexModule>();
                if (null!=configuration)
                {
                    configuration(with);
                }
                with.ViewLocationProvider<ResourceViewLocationProvider>();
                with.ViewEngine<VeilViewEngine>();
            });

            var browser = new Browser(bootstrapper, defaults);
            return browser;
        }
    }
}

