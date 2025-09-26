using Application.Interfaces;
using Application.Services.Implamentations;
using Autofac;
using Infrastructure.Context;
using Infrastructure.Repository.Implementations;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitofWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DependencyInjection
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region helper
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            #endregion

            #region managers&Repositories
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AccountService>().As<IAccountService>(); 
            builder.RegisterType<InvoiceRepository>().As<IInvoiceRepository>();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>();
            #endregion

            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var opts = (configuration["DbConnection:ConnectionString"]);
                return new SqlConnectionFactory(opts);
            })
            .AsSelf()
            .InstancePerLifetimeScope();


            #region validators
            //builder.RegisterType<ClaimValidator>().As<IValidator<ClaimDto>>().InstancePerLifetimeScope();
            //builder.RegisterType<RegisterUserValidator>().As<IValidator<UserForRegisterDto>>().InstancePerLifetimeScope();
            //builder.RegisterType<TemplateAddValidator>().As<IValidator<TemplateAddDto>>().InstancePerLifetimeScope();
            //builder.RegisterType<TemplateUpdateValidator>().As<IValidator<TemplateUpdateDto>>().InstancePerLifetimeScope();
            #endregion

        }
    }

}
