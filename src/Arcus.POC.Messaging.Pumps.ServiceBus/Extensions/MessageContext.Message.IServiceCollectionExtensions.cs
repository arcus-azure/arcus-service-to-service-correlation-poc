﻿using System;
using Arcus.POC.Messaging.Pumps.ServiceBus;
using GuardNet;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions on the <see cref="IServiceCollection"/> to add a <see cref="AzureServiceBusMessagePump"/> and its <see cref="IAzureServiceBusMessageHandler{TMessage}"/>'s implementations.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a <see cref="IAzureServiceBusMessageHandler{TMessage}" /> implementation to process the messages from an <see cref="AzureServiceBusMessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="messageContextFilter">The function that determines if the message handler should handle the message based on the context.</param>
        /// <param name="messageBodyFilter">The filter to restrict the message processing based on the incoming message body.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="services"/>, <paramref name="messageContextFilter"/>, or <paramref name="messageBodyFilter"/> is <c>null</c>.</exception>
        public static IServiceCollection WithServiceBusMessageHandler<TMessageHandler, TMessage>(
            this IServiceCollection services,
            Func<AzureServiceBusMessageContext, bool> messageContextFilter,
            Func<TMessage, bool> messageBodyFilter)
            where TMessageHandler : class, IAzureServiceBusMessageHandler<TMessage>
            where TMessage : class
        {
            Guard.NotNull(services, nameof(services), "Requires a set of services to add the message handler");
            Guard.NotNull(messageContextFilter,  nameof(messageContextFilter), "Requires a filter to restrict the message processing within a certain message context");

            return services.WithMessageHandler<TMessageHandler, TMessage, AzureServiceBusMessageContext>(messageContextFilter, messageBodyFilter);
        }

        /// <summary>
        /// Adds a <see cref="IAzureServiceBusMessageHandler{TMessage}" /> implementation to process the messages from an <see cref="AzureServiceBusMessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="messageContextFilter">The function that determines if the message handler should handle the message based on the context.</param>
        /// <param name="messageBodyFilter">The filter to restrict the message processing based on the incoming message body.</param>
        /// <param name="implementationFactory">The function that creates the message handler.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="services"/>, <paramref name="messageContextFilter"/>, <paramref name="messageBodyFilter"/>, or <paramref name="implementationFactory"/> is <c>null</c>.</exception>
        public static IServiceCollection WithServiceBusMessageHandler<TMessageHandler, TMessage>(
            this IServiceCollection services,
            Func<AzureServiceBusMessageContext, bool> messageContextFilter,
            Func<TMessage, bool> messageBodyFilter,
            Func<IServiceProvider, TMessageHandler> implementationFactory)
            where TMessageHandler : class, IAzureServiceBusMessageHandler<TMessage>
            where TMessage : class
        {
            Guard.NotNull(services, nameof(services), "Requires a set of services to add the message handler");
            Guard.NotNull(messageContextFilter,  nameof(messageContextFilter), "Requires a filter to restrict the message processing within a certain message context");
            Guard.NotNull(messageBodyFilter, nameof(messageBodyFilter), "Requires a filter to restrict the message processing based on the incoming message body");
            Guard.NotNull(implementationFactory, nameof(implementationFactory), "Requires a function to create the message handler with dependent services");

            return services.WithMessageHandler<TMessageHandler, TMessage, AzureServiceBusMessageContext>(messageContextFilter, messageBodyFilter, implementationFactory);
        }
    }
}
