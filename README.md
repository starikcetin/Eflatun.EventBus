# Eflatun.EventBus
Generic event bus for Unity with Extenject.

## Purpose
Extenject's signals are great, but at the end you end up converting your installers into god classes that wire up emitters and listeners together. I did not like this. Besides, I really don't want to write code just to wire together emitters and listeners.

This library offers a more flexible solution. It implements an event bus, that allows for emitting and listening to events, without any wire-up code.

## Installation

This repository is a UPM package.

1. Get these: 

    1. https://github.com/mob-sakai/UpmGitExtension
    
    2. https://github.com/mob-sakai/GitDependencyResolverForUnity

2. Install this repo from Unity Package Manager window, inside Unity *(follow the instructions on the first link above if this is your first time using UPM for Git repositories)*.

## Usage

### Preparation 

#### With Extenject/Zenject

 I already provided you with a premade installer that installs the `EventBus<>` as a singleton, so you can just register it on your context. You can find it at the root of the package folder. 

* Find it here in Unity Editor: `Projet Panel → Packages → Eflatun.EventBus → EflatunEventBusInstaller.cs`

* It is a `MonoInstaller`.

If the provided installer doesn't satisfy your needs, you can always install it on your own, wherever you want. Take a look at the provided installer to get an idea on how to do it.

#### Without Extenject/Zenject

**NOTE**: Currently this library lists Extenject as a dependency. So even if you don't use it, you are getting it. Because I am too lazy to maintain two separate branches of the same library.

Even though I ***STRONGLY*** recommend you use Extenject, you can still use this library without it.

You somehow need to make the EventBus class available to the rest of your code. 

*If I were you, I would wrap it around with a singleton generic class and use the singleton everywhere for convinience.*

### Declaring Events with Arguments

```cs
using Eflatun.EventBus.interfaces;

// This struct will be our event itself.
// Events must derive from IEvent<> interface. Generic argument is the Arguments struct this event takes.
public struct EventA : IEvent<EventA.Args>
{
    // This is the arguments we pass along with our event instance.
    // This property is required by IEvent<> interface.
    public Args Arguments { get; }

    // This is our constructor. We want the emitter to give us an instance of EventA.Args while creating the event.
    public EventA(Args arguments)
    {
        Arguments = arguments;
    }

    // This struct will hold our arguments.
    public struct Args : IEventArguments
    {
        public string Message { get; }

        public Args(string message)
        {
            Message = message;
        }
    }
}
```

### Declaring Events without Arguments

```cs
using Eflatun.EventBus.interfaces;

public struct EventE : IEvent
{
}
```

Yep. It is really that simple.

    
### Emitting Events

```cs
using System.Collections;
using UnityEngine;
using Zenject;

public class EmitterA : MonoBehaviour
{
    // Get our event bus  from Extenject.
    private EventBus<EventA> _eventBus;

    [Inject]
    public void Init(EventBus<EventA> eventBus)
    {
        _eventBus = eventBus;
    }

    // Let's emit our event as soon as our game starts.
    public IEnumerator Start()
    {
        // Prepare the arguments.
        var newArgs = new EventA.Args($"sent from {nameof(EmitterA)}");
        
        // Create the event.
        var newEvent = new EventA(newArgs);
        
        // Emit the event we just created.
        _eventBus.Emit(this, newEvent);
    }
}
```

Emitters can be anything you want, they don't have to be a `MonoBehaviour`.

### Listening for Events

```cs
using Eflatun.EventBus.interfaces;
using UnityEngine;
using Zenject;

// Listener classes must implement IEventListener<> interface.
public class ListenerA : MonoBehaviour, IEventListener<EventA>
{
    // Get our event bus from Extenject.
    private EventBus<EventA> _eventBus;

    [Inject]
    public void Init(EventBus<EventA> eventBus)
    {
        _eventBus = eventBus;
    }

    public void Awake()
    {
        // Let event bus know we want to listen for the event. 
        // Pass in the listener class as an argument.
        // (You will probably want to pass in 'this' 99% of the time.)
        _eventBus.Listen(this);
    }

    // This will be our event handler. It is required by IEventListener<>. 
    // We get the sender and the event as our parameters.
    public void OnEvent(object sender, EventA @event)
    {
        // Arguments of the event.
        var args = @event.Arguments;
        var message = args.Message;
        
        Debug.Log($"{nameof(ListenerA)} on {gameObject.name} received {@event} from {sender}");
    }
}
```

Just like emitters, listeners can also be anything you want, they don't have to be a `MonoBehaviour`.

## Caveats

* If you emit events in Awake or Start, Listeners might not be able to catch them. They also might. So I suggest registering listeners in Awake and emitting in Start, to make sure all listeners receive the event correctly.

* ~GC allocations. This is fixable by using structs instead of classes for Events. I will work on it.~ 
    * This is fixed in `0.1.0`. Now you can use structs for your events and arguments, this means absolutely 0 GC allocations! Hooray!


## License

MIT License. Refer to the [LICENSE](/LICENSE) file.

Copyright (c) 2019 S. Tarık Çetin.

