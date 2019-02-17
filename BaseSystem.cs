// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

namespace VARP.Subsystems
{
    /// <summary>
    /// Allow to build tree of systems and deliver messages to all family
    /// </summary>
    public partial class BaseSystem
    {
        private BaseSystem parent;
        private BaseSystem sibling;
        private BaseSystem children;

        /// <summary>
        /// Add child to this system
        /// </summary>
        public void AddChild(BaseSystem child)
        {
            if (children == null)
                children = child;
            else
            {
                var curChild = children;
  
                while (true)
                {
                    if (curChild.sibling == null)
                    {
                        curChild.sibling = child;
                        break;
                    }
      
                    curChild = curChild.sibling;
                }
            }
  
            child.parent = this;
        }

        /// <summary>
        /// Remove child from this system
        /// </summary>
        public void RemoveChild(BaseSystem child)
        {
            if (children != null)
            {
                BaseSystem prevChild = null;
                BaseSystem curChild  = children;

                while (curChild != child)
                {
                    prevChild = curChild;
      
                    if ((curChild = curChild.sibling) == null)
                        return;
                }
    
                if (prevChild != null)
                    prevChild.sibling = curChild.sibling;   
            }
        }

        /// <summary>
        /// Get system's child by index
        /// </summary>
        public BaseSystem GetChild(int index)
        {
            if (children == null)
                return null;

            var curChild = children;
            int curIndex = 0;
  
            while (index != curIndex)
            {
                if ((curChild = curChild.sibling) == null)
                    return null;
  
                curIndex++;
            }
  
            return curChild; 
        }
        
        /// <summary>
        /// Deliver message to all children children of this system. Do not deliver to self.
        /// </summary>
        public virtual void OnMessage(BaseSystem sender, int msg, object arg1, object arg2)
        {
            if (sender == this)
                return;
    
            if (children != null)
            {
                var cur = children;
                do 
                {
                    cur.OnMessage(sender, msg, arg1, arg2);
                } while ((cur = cur.sibling) != null);
            }
        }
        
        /// <summary>
        /// Deliver message to all family from top system. 
        /// </summary>
        public virtual void PostMessage(BaseSystem sender, int msg, object arg1 = null, object arg2 = null)
        {
            var cur = this;
  
            while (cur.parent != null)
                cur = cur.parent;
    
            cur.OnMessage(sender, msg, arg1, arg2);
        }
    }
}