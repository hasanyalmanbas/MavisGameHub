using System;
using System.Collections.Generic;

namespace _Project.Core
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type)) _subscribers[type] = new List<Delegate>();
            _subscribers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_subscribers.TryGetValue(type, out var list)) return;
            list.Remove(handler);
        }

        public void Publish<T>(T evt)
        {
            var type = typeof(T);
            if (!_subscribers.TryGetValue(type, out var list)) return;
            foreach (var @delegate in list) ((Action<T>)@delegate)(evt);
        }
    }
}