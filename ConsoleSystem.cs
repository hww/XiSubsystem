using UnityEngine;

namespace VARP.Subsystems
{
    /// <summary>
    /// Just display all messages in system
    /// </summary>
    public class ConsoleSystem : BaseSystem
    {
        public ConsoleSystem(BaseSystem parent)
        {
            parent.AddChild(this);
        }

        public override void MessageRouter(BaseSystem src, ESustemMessage msg, object arg1, object arg2)
        {
            Debug.LogFormat("[{0:0.00}] {1} arg1: {2} arg2: {3}", Time.time, msg, arg1, arg2);
        }
    }
}