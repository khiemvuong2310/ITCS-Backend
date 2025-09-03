using Finamon.Repo.UnitOfWork;
using Finamon.Repo.UnitOfWork.Repositories;
using Finamon.Service.Interfaces;
using Finamon.Service.Mapping;
using Finamon.Service.RequestModel;
using Finamon.Service.Services;
using Finamon_Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;

namespace FA25_CP.CryoFert_BE.AppStarts
{
    public static class DependencyInjection
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddSingleton<ITwilioRestClient>(new TwilioRestClient("ACCOUNT_SID", "AUTH_TOKEN"));

            //Add Scoped
        }
    }
}
