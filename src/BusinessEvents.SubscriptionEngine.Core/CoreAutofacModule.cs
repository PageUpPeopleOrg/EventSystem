﻿using Autofac;
using BusinessEvents.SubscriptionEngine.Core.DataStore;
using BusinessEvents.SubscriptionEngine.Core.DeadLetterManagement;
using BusinessEvents.SubscriptionEngine.Core.FeedManagement;
using BusinessEvents.SubscriptionEngine.Core.Notifiers;

namespace BusinessEvents.SubscriptionEngine.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<BusinessEventStore>().As<IBusinessEventStore>().InstancePerDependency();
            builder.RegisterType<AtomFeedService>().As<IFeedService>().InstancePerDependency();

            builder.RegisterType<ServiceProcess>().As<IServiceProcess>().InstancePerDependency();
            builder.RegisterType<SubscriptionsManager>().As<ISubscriptionsManager>().SingleInstance();
            builder.RegisterType<AuthenticationModule>().As<IAuthenticationModule>().InstancePerDependency();
            builder.RegisterType<DeadLetterService>().As<IDeadLetterService>().InstancePerDependency();
            builder.RegisterType<SubscriberErrorService>().As<ISubscriberErrorService>().InstancePerDependency();

            builder.RegisterType<TelemetryNotifier>().Keyed<INotifier>(SubscriptionType.Telemetry);
            builder.RegisterType<SlackNotifier>().Keyed<INotifier>(SubscriptionType.Slack);
            builder.RegisterType<WebhookNotifier>().Keyed<INotifier>(SubscriptionType.Webhook);
            builder.RegisterType<AuthenticatedWebhookNotifier>().Keyed<INotifier>(SubscriptionType.AuthenticatedWebhook);
            builder.RegisterType<LambdaNotifier>().Keyed<INotifier>(SubscriptionType.Lambda);
        }
    }
}