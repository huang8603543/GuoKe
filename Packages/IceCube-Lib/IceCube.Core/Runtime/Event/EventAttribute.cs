using System;

namespace IceCube.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAttribute : Attribute
    {
        public ushort MsgCode;

        public EventAttribute(ushort nMsgCode)
        {
            MsgCode = nMsgCode;
        }
    }
}
