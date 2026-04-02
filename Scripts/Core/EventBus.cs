using System;
using System.Collections.Generic;
namespace SL.Scripts.Core;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> _events = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);

        if (!_events.ContainsKey(type))
            _events[type] = new List<Delegate>();

        _events[type].Add(callback);
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);

        if (!_events.ContainsKey(type))
            return;

        _events[type].Remove(callback);
    }

    public static void Publish<T>(T evt)
    {
        var type = typeof(T);

        if (_events.ContainsKey(type))
        {
            var listeners = _events[type];
            for (int i = 0; i < listeners.Count; i++)
            {
                ((Action<T>)listeners[i])?.Invoke(evt);
            }
        }
        // 🔥 gọi object listeners
        if (_events.ContainsKey(typeof(object)))
        {
            foreach (var cb in _events[typeof(object)])
                ((Action<object>)cb)?.Invoke(evt);
        }
    }

    public static void Clear()
    {
        _events.Clear();
    }
}