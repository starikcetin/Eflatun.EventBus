[![openupm](https://img.shields.io/npm/v/com.eflatun.eventbus?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.eflatun.eventbus/)

# Eflatun.EventBus

Generic event bus for Unity with Extenject.

## Purpose

Extenject's signals are great, but at the end you end up converting your installers into god classes that wire up emitters and listeners together. I did not like this. Besides, I really don't want to write code just to wire together emitters and listeners.

This library offers a more flexible solution. It implements an event bus, that allows for emitting and listening to events, without any wire-up code.

## Installation

```
npm install -g openupm-cli
openupm add com.eflatun.eventbus
```

## Usage

### Preparation

#### With Extenject/Zenject

I already provided you with a premade installer that installs the `EventBus<>` as a singleton, so you can just register it on your context. You can find it at the root of the package folder.

- Find it here in Unity Editor: `Project Panel → Packages → Eflatun.EventBus → EflatunEventBusInstaller.cs`

- It is a `MonoInstaller`.

If the provided installer doesn't satisfy your needs, you can always install it on your own, wherever you want. Take a look at the provided installer to get an idea on how to do it.

#### Without Extenject/Zenject

**NOTE**: Currently this library lists Extenject as a dependency. So even if you don't use it, you are getting it. Because I am too lazy to maintain two separate branches of the same library.

Even though I **_STRONGLY_** recommend you use Extenject, you can still use this library without it.

You somehow need to make the EventBus class available to the rest of your code.

_If I were you, I would wrap it around with a singleton generic class and use the singleton everywhere for convenience._

### Declaring Events with Arguments

```cs
using Eflatun.EventBus.interfaces;

// This struct will be our event itself.
// Events must derive from IEvent<> interface. Generic argument is the Arguments struct this event takes.
public readonly struct EventA : IEvent<EventA.Args>
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
    public readonly struct Args : IEventArguments
    {
        public string Message { get; }

        public Args(string message)
        {
            Message = message;
        }
    }
}
```

You don't _have to_ make the structs readonly, but it is a **_really_** good idea to do so.

### Declaring Events without Arguments

```cs
using Eflatun.EventBus.interfaces;

public readonly struct EventE : IEvent
{
}
```

Yep. It is really that simple.

You don't _have to_ make the structs readonly, but it is a **_really_** good idea to do so.

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
    private void Start()
    {
        // Prepare the arguments.
        var newArgs = new EventA.Args($"This event is sent from EmitterA.");

        // Create the event.
        var newEvent = new EventA(newArgs);

        // Emit the event we just created.
        // The first parameter is the sender, the second parameter is the event instance.
        _eventBus.Emit(this, newEvent);
    }
}
```

Emitters can be anything you want, they don't have to be a `MonoBehaviour`.

### Listening for Events

```cs
using UnityEngine;
using Zenject;

public class ListenerA : MonoBehaviour, IEventListener<EventA>
{
    // Get our event bus from Extenject.
    private EventBus<EventA> _eventBus;

    [Inject]
    public void Init(EventBus<EventA> eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        // Let event bus know we want to listen for the event.
        // Pass in a listener function.
        _eventBus.AddListener(OnEventA);
    }

    // This will be our event handler.
    // We get the sender and the event as our parameters.
    public void OnEventA(object sender, EventA @event)
    {
        // Get the arguments of the event.
        var args = @event.Arguments;
        var message = args.Message;

        Debug.Log($"ListenerA received EventA from {sender} with the message: {message}");
    }

    private void OnDestroy()
    {
        // Call RemoveListener for cleanup.
        _eventBus.RemoveListener(OnEventA);
    }
}
```

Listeners don't have to reside within a `MonoBehaviour`, they can reside anywhere you want.

Listeners can be anonymous funcitons as well, but making them named functions allows you to remove the listener later on.

### `After` and `Before` Events

Most of the time you don't (and shouldn't) care about the order that listeners are invoked. But if it happens that you need to run a listener before or after all other listeners, you can do that as well.

You have three _groups_ of listeners, that are invoked in this order:

1. Listeners registered with `ListenBefore`
1. Listeners registered with `Listen`
1. Listeners registered with `ListenAfter`

Listeners that are in the same _group_ are invoked in the order of registration.

You also need to use the corresponding cleanup methods, namely:

1. `ListenBefore` -> `RemoveBefore`
1. `Listen` -> `Remove`
1. `ListenAfter` -> `RemoveAfter`

## Caveats

- If you emit events in `Awake` or `Start`, Listeners might not be able to catch them. They also might. So if you have to emit events at the beginning of the component lifecycle, I suggest registering listeners in `Awake` and emitting in `Start`, to make sure all the listeners receive the event.

- Try not to use `ListenBefore` or `ListenAfter` too much and if possible never at all. Because listeners getting invoked in a coordinated order introduces a reliance/coupling to the order of invocation. Most of the time this destroys the purpose of using an event bus in the first place, which is to decouple your code. If you really need to control the order of listeners, maybe using an event-based structure isn't the best solution in your case and you should opt for simple method calls where you control the exact order of invocation.

## License

MIT License. Refer to the [LICENSE](/LICENSE) file.

Copyright (c) 2019 S. Tarık Çetin.
