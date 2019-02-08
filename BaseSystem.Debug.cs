using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VARP.DataStructures.Interfaces;

namespace VARP.Subsystems
{
    public partial class BaseSystem 
    {
        public static string Inspect(BaseSystem system)
        {
            var sb = new StringBuilder();
            Inspect(system, sb);
            return sb.ToString();
        }
        
        public static void Inspect(BaseSystem system, StringBuilder sb, string prefix = "")
        {
            Debug.Assert(system != null);
            sb.Append(prefix);
            sb.AppendLine(system.GetType().Name);
            var current = system.children;
            while (current != null)
            {
                Inspect(current, sb, prefix + "  ");
                current = current.sibling;
            }
        }
    }
}
