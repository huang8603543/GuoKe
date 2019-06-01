using System;
using System.Collections.Generic;

namespace IceCube.Core
{
    public class EventArg
    {
        public List<object> Args;
        
        public EventArg(params object[] rArgs)
        {
            Args = new List<object>(rArgs);
        }

        public T Get<T>(int nIndex)
        {
            if (Args == null)
                return default;
            if (nIndex < 0 || nIndex >= this.Args.Count)
                return default;
            return (T)Args[nIndex];
        }
    }

    public class EventManager : TSingleton<EventManager>
    {
        public class Event
        {
            public int MsgCode;
            public List<Action<EventArg>> Callbacks;
        }

        Dictionary<int, Event> mEvents;

        protected EventManager()
        {

        }

        public void Initialize()
        {
            mEvents = new Dictionary<int, Event>();
        }

        public bool Contains(int nMsgCode)
        {
            return mEvents.ContainsKey(nMsgCode);
        }

        public void Binding(int nMsgCode, Action<EventArg> rEventCallback)
        {
            Event rEvent = null;
            if (mEvents.TryGetValue(nMsgCode, out rEvent))
            {
                if (rEvent.Callbacks == null)
                {
                    rEvent.Callbacks = new List<Action<EventArg>>();
                }
                else
                {
                    if (!rEvent.Callbacks.Contains(rEventCallback))
                    {
                        rEvent.Callbacks.Add(rEventCallback);
                    }
                }
            }
            else
            {
                rEvent = new Event()
                {
                    MsgCode = nMsgCode,
                    Callbacks = new List<Action<EventArg>>() { rEventCallback },
                };
                mEvents.Add(nMsgCode, rEvent);
            }
        }

        public void Unbinding(int nMsgCode, Action<EventArg> rEventCallback)
        {
            Event rEvent = null;
            if (mEvents.TryGetValue(nMsgCode, out rEvent))
            {
                if (rEvent.Callbacks != null)
                {
                    rEvent.Callbacks.Remove(rEventCallback);
                }
            }
        }

        public void Distribute(int nMsgCode, params object[] rEventArgs)
        {
            EventArg rEventArg = new EventArg()
            {
                Args = new List<object>(rEventArgs),
            };
            Distribute(nMsgCode, rEventArg);
        }

        public void Distribute(int nMsgCode, EventArg rEventArg)
        {
            Event rEvent = null;
            if (mEvents.TryGetValue(nMsgCode, out rEvent))
            {
                if (rEvent.Callbacks != null)
                {
                    for (int i = 0, len = rEvent.Callbacks.Count; i < len; i++)
                    {
                        rEvent.Callbacks[i]?.Invoke(rEventArg);
                    }
                }
            }
        }
    }
}
