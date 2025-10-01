using System;
using UnityEngine;
using UnityEngine.Events;

namespace XaviEssencials.Runtime
{
    [CreateAssetMenu(fileName = "EventChannel", menuName = "Xavi Essencials/EventChannel")]
    public class EventChannel : ScriptableObject
    {
        public UnityAction OnEventRaised;
        public UnityAction<object> OnEventRaisedWithContext;

        public void Raise() => OnEventRaised?.Invoke();

        public void Raise(object context) => OnEventRaisedWithContext?.Invoke(context);

        public void Raise(Action callback)
        {
            OnEventRaised?.Invoke();
            callback?.Invoke();
        }

        public void Raise(object context, Action callback)
        {
            OnEventRaisedWithContext?.Invoke(context);
            callback?.Invoke();
        }
    }
}
