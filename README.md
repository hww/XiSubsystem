# XiSubsystem

![](https://img.shields.io/badge/unity-2018.3%20or%20later-green.svg)
[![⚙ Build and Release](https://github.com/hww/XiSubsystem/actions/workflows/ci.yml/badge.svg)](https://github.com/hww/XiSubsystem/actions/workflows/ci.yml)
[![openupm](https://img.shields.io/npm/v/com.hww.xisubsystem?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.hww.xisubsystem/)
[![](https://img.shields.io/github/license/hww/XiSubsystem.svg)](https://github.com/hww/XiSubsystem/blob/master/LICENSE)
[![semantic-release: angular](https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release)](https://github.com/semantic-release/semantic-release)

Base subsystem for any Unity 3D projects created by [hww](https://github.com/hww)

## Introduction

In my projects, the Subsystem is a replacement for the Singletone class. It's a better solution and allows you to make more complex projects without any headaches. Imagine Singletones that can be organized as a tree, but can also be initialized in a few passes -- that's what the Subsystem is for. On top of everything else, each Subsystem can receive and broadcast messages.

This solution was successfully used in several game projects, which made the Singletone class and all its problems unnecessary.
The system organized by tree of subsystems. 

## Install

The package is available on the openupm registry. You can install it via openupm-cli.

```bash
openupm add com.hww.xisubsystem
```
You can also install via git url by adding this entry in your manifest.json

```bash
"com.hww.xisubsystem": "https://github.com/hww/XiSubsystem.git#upm"
```

## Usage 

Each subsystem has fields to buid the tree.

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

A bit more practical example 

```C#
public partial class SoundSystem : BaseSystem
{
    /// <summary>SoundSystem constructor</summary>
    public SoundSystem(BaseSystem parent)
    {
        parent.AddChild(this);
    }

    // Singleton

    /// <summary>Initialize system</summary>
    public static void PreInitialize()
    {
        // Initialization code;
    }

    /// <summary>De initialize system</summary>
    public static void DeInitialize()
    {
        // Deinitialization
    }   
}   
```        
## Initialization

The two passes initialization process calls the method _preInitialize_ for all subsystems at the phase 1. Then calls the method _initialize_ at the phase 2. 

```C#
private static readonly SystemConfig[] Subsystems = {
    // the example of system abowe
    new SystemConfig
    {
        name = "GameSoundSystem",
        preInitialize = GameSoundSystem.PreInitialize,
        deInitialize = GameSoundSystem.DeInitialize
    },
    // as example here is more complex system
    new SystemConfig
    {
        name = "SpawnerSystem",
        preInitialize = SpawnerSystem.PreInitialize,
        initialize = SpawnerSystem.Initialize,
        deInitialize = SpawnerSystem.DeInitialize,
        initializeObject = SpawnerSystem.InitializeObject,
        entryType = typeof(Spawner)
    },
    ...
}
```
The full code of the initialization structure Subsystem below

```C#
/// <summary>
/// Structure for initialization of a subsystem.
/// </summary>
public struct SystemConfig
{
    /// <summary> Subsystem name </summary>
    public string name;        
    /// <summary> Called for each subsystem to initialize </summary>
    public Action preInitialize;
    /// <summary> Called after all subsystem preInitialize was called </summary>
    public Action initialize;
    /// <summary> Called for each entry </summary>
    public Action<object> initializeObject;
    /// <summary> Called for each subsystem to de-initialize </summary>
    public Action deInitialize;
    /// <summary> Object type for this Subsystem </summary>
    public Type entryType;
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

### Garbage 

To prevent a garbage should be used a message container for arg1 and arg2.

### Extend the message ID

Because the SystemEvent enum defined in this class, there should be a way to extend enum for the game. Therefore there is pseudo event ids.

```C#
public enum SystemEvent
{
    Null = 0,
        
    UnitySceneOnLoaded,
    UnitySceneOnUnloaded,
    UnitySceneOnChanged,
        
    ...
    // Pseudo events
    SoundEvent = 0x1000,
    EntityEvent = 0x2000,
    GameEvent = 0x4000
}
```

For example sound events of the game can be specified as:

```C#
public enum SoundEvent
{
    Null = SystemEvent::SoundEvent,
    PlayMusic,
    PlaySound,
    ....
}
```

The same way can be added EntityEvents or GameEvents. 
