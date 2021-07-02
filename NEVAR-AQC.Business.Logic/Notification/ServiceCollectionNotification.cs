using Microsoft.Extensions.DependencyInjection;
using NEVAR_AQC.Business.Notification;
using NEVAR_AQC.Data;
using NEVAR_AQC.Data.EF.Repositories;

namespace NEVAR_AQC.Business.Logic.Notification
{
    public static class ServiceCollectionNotification
    {
        public static IServiceCollection AddNotificationService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<,>));
            services.AddTransient(typeof(INotificationBase<>), typeof(NotificationBase<>));
            services.AddTransient<ICustomerNotification, CustomerNotification>();
            services.AddTransient<IRequirementInvoiceNotification, RequirementInvoiceNotification>();
            services.AddTransient<IImplementerNotification, ImplementerNotification>();
            return services;
        }
    }
}