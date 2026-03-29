using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reign.Events
{
    public interface IEvent { }
    public interface IQuery<TResponse> { }
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> Dictionary_Subscribers = new();

        public static void Subscribe<T>(Action<T> CALLBACK) where T : IEvent
        {
            var var_Type = typeof(T);

            if (!Dictionary_Subscribers.ContainsKey(var_Type))
            {
                Dictionary_Subscribers[var_Type] = new List<Delegate>();
            }

            Dictionary_Subscribers[var_Type].Add(CALLBACK);
        }

        public static void Unsubscribe<T>(Action<T> CALLBACK) where T : IEvent
        {
            var var_Type = typeof(T);

            if (Dictionary_Subscribers.ContainsKey(var_Type))
            {
                Dictionary_Subscribers[var_Type].Remove(CALLBACK);
            }
        }

        public static void Publish<T>(T EVENT) where T : IEvent
        {
            var var_Type = typeof(T);

            if (!Dictionary_Subscribers.ContainsKey(var_Type)) return;

            foreach (var SUBSCRIBER in Dictionary_Subscribers[var_Type])
            {
                ((Action<T>)SUBSCRIBER)?.Invoke(EVENT);
            }
        }
    }

    public static class QueryBus
    {
        private static readonly Dictionary<Type, Delegate> Dictionary_Handlers = new();

        public static void Register<TQuery, TResponse>(Func<TQuery, TResponse> HANDLER) where TQuery : IQuery<TResponse>
        {
            Dictionary_Handlers[typeof(TQuery)] = HANDLER;
        }

        public static TResponse Query<TQuery, TResponse>(TQuery QUERY)
        {
            if (Dictionary_Handlers.TryGetValue(typeof(TQuery), out var HANDLER))
            {
                return ((Func<TQuery, TResponse>)HANDLER)(QUERY);
            }

            throw new Exception($"No handler present for query: {typeof(TQuery)}");
        }
    }
}