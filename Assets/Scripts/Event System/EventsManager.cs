using System;
using System.Collections.Generic;
public static class EventsManager
{
    static readonly Dictionary<Type, Action<NotificationEvent>> _events = new Dictionary<Type, Action<NotificationEvent>>();

    static readonly Dictionary<Delegate, Action<NotificationEvent>> _eventLookups = new Dictionary<Delegate, Action<NotificationEvent>>();

    //Registers a listener for a specific type of notification
    public static void AddListener<T>(Action<T> evt) where T : NotificationEvent
    {
        if (!_eventLookups.ContainsKey(evt))
        {
            Action<NotificationEvent> newAction = (e) => evt((T)e);
            _eventLookups[evt] = newAction;

            if (_events.TryGetValue(typeof(T), out Action<NotificationEvent> internalAction))
                _events[typeof(T)] = internalAction += newAction;
            else
                _events[typeof(T)] = newAction;
        }
    }
    //Removes a previous added listener
    public static void RemoveListener<T>(Action<T> evt) where T : NotificationEvent
    {
        if (_eventLookups.TryGetValue(evt, out var action))
        {
            if (_events.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    _events.Remove(typeof(T));
                else
                    _events[typeof(T)] = tempAction;
            }
            _eventLookups.Remove(evt);
        }
    }
    //Broadcast the event for all the listeners registered for a specific type
    public static void Broadcast(NotificationEvent evt)
    {
        if (_events.TryGetValue(evt.GetType(), out var action))
            action.Invoke(evt);
    }
    // Removes all registered listeners.
    public static void Clear()
    {
        _events.Clear();
        _eventLookups.Clear();
    }
}
//class for the notification events
public class NotificationEvent{}
