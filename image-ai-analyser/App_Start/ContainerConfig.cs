﻿using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using image_ai_analyser.Interfaces;
using image_ai_analyser.ModelBuilder;
using image_ai_analyser.Models;
using image_ai_analyser.Services;

namespace image_ai_analyser.App_Start
{
    public class ContainerConfig
    {
        public static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ImageViewModelBuilder>()
                .As<IImageViewModelBuilder>()
                .SingleInstance();

            builder.RegisterType<ImageServices>()
                .As<ImageServices>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}