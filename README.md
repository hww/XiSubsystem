# extSubsystem

Base subsystem for my Unity projects

![](https://img.shields.io/badge/unity-2021.1%20or%20later-green.svg)
[![⚙ Build and Release](https://github.com/hww/XiSusystem/actions/workflows/ci.yml/badge.svg)](https://github.com/hww/XiSusystem/actions/workflows/ci.yml)
[![openupm](https://img.shields.io/npm/v/com.hww.XiSusystem?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.hww.xisubsystem/)
[![](https://img.shields.io/github/license/hww/XiSusystem.svg)](https://github.com/hww/XiSusystem/blob/master/LICENSE)

## Introduction

The system organized by tree of subsystems. Each subsystem has fields to buid the tree.

```C#
public partial class BaseSystem
    {
        private BaseSystem parent;
        private BaseSystem sibling;
        private BaseSystem children;
}
```

The list below contains methods for making and ispecting the tree.

```C#
public void AddChild(BaseSystem child)
public void RemoveChild(BaseSystem child)
public BaseSystem GetChild(int index)
```

The example of subsystem below.

```C#
    /// <summary>
    /// Just display all messages in system
    /// </summary>
    public class ConsoleSystem : BaseSystem
    {
        public ConsoleSystem(BaseSystem parent)
        {
            parent.AddChild(this);
        }

        public override void OnMessage(BaseSystem sender, ESustemMessage msg, object arg1, object arg2)
        {
            Debug.LogFormat("[{0:0.00}] {1} arg1: {2} arg2: {3}", Time.time, msg, arg1, arg2);
        }
    }
```

## Messages

Each subsystem may have a method for receiving messages.

```C#
public virtual void OnMessage(BaseSystem sender, ESustemMessage msg, object arg1, object arg2)        
```

To send message there are next method.

```C#
// Deliver the message to all family from top system. 
public virtual void PostMessage(BaseSystem sender, ESustemMessage msg, object arg1 = null, object arg2 = null)
```

Figure below illustrate subsystems tree and message routing.

![Subsystems Image](Documentation/subsystems.png)
