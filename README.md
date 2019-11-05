# Eflatun.EventBus
Generic EventBus for Unity with Extenject.

## Purpose
Extenject's signals are great, but at the end you end up converting your installers into god classes that wire up emitters and listeners together. I did not like this. Besides, I really don't want to write code just to wire together emitters and listeners.

This library offers a more flexible solution. It implements an event bus, that allows for emitting and listening to events, without any wire-up code.

## Installation

This repository is a UPM package.

1. Get this: https://github.com/mob-sakai/UpmGitExtension

2. Install this repo from Unity Package Manager window, inside Unity *(follow the instructions on the link above if this is your first time using UPM for git repositories)*.

## Usage

### Preparation 

#### With Extenject/Zenject

 I already provided you with a premade installer that installs the EventBus as a singleton, so you can just register it on your context. You can find it at the root of the package folder. 

	* Find it here in Unity Editor: (Projet Panel → Packages → Eflatun.EventBus → `EflatunEventBusInstaller.cs`)
	
	* It is a `MonoInstaller`.

If the provided installer doesn't satisfy your needs, you can always install it on your own, wherever you want. Take a look at the provided installer to get an idea how to do it.

#### Without Extenject/Zenject

Even though I ***STRONGLY*** recommend you use Extenject, you can still use this library without it.

You somehow need to make the EventBus class available to the rest of your code. 

*If I were you, I would wrap it around with a singleton generic class and use the singleton everywhere for convinience.*

### Declaring Events

```cs
using Eflatun.EventBus;

// This class will be our event itself.
// Events must derive from Event<> class. Generic argument is the class itself.
public class EventA : Event<EventA>
{
	// First parameter will accept a IEventEmitter<>. This is the *thing* that raised the event, whatever it is.
	// Second parameter accepts an EventArguments<>, or in this case, a class that derives from it.
	// Then we simply pass these to the base class construcor with base(sender, args). Base class handles everything for us.
	public EventA(IEventEmitter<EventA> sender, Args args) : base(sender, args)
	{
	}

	// This class is where we will pass data along with our event.
	// This class derives from EventArguments<>. This is a must in order to use it in the Event's constructor.
	public class Args : EventArguments<EventA>
	{
		public string Message { get; }

		public Args(string message)
		{
			Message = message;
		}
	}
}
```

**Note:** You don't have to have an Arguments implementation. If you omit it, that means your event won't take any arguments. You EventA class constructor will look like this then:

```cs
public EventA(IEventEmitter<EventA> sender, EventArgs<EventA> args) : base(sender, args)
{
}
```

Since you EventArgs<> is abstract, it is not constructable *(well, technically it is, but that is not within the scope of this document)*.
Therefore every emitter must pass in 'null' instead of the arguments class.
	
### Emitting Events

```cs
using UnityEngine;
using Zenject;
using Eflatun.EventBus;

// Emitter classes must implement IEventEmitter<> interface.
public class EmitterA : MonoBehaviour, IEventEmitter<EventA>
{
	// Get our event bus  from Extenject.
	private EventBus<EventA> _eventBus;

	[Inject]
	public void Init(EventBus<EventA> eventBus)
	{
		_eventBus = eventBus;
	}
	
	// Wrapping event emitting inside a method is an arbitrary decision, you don't have to do it.
	public void EmitEventA(string message)
	{		
		// Prepare the event arguments.
		// You don't have to have arguments for your event. Then you would skip 
		// creating an args class and pass in 'null' for the second argument 
		// while constructing your event.
		var args = new EventA.Args(message);
		
		// Create our event.
		var evnt = new EventA(this, args);
		
		// Emit the event we just created.
		_eventBus.Emit(evnt);
	}
}
```

### Listening for Events

```cs
using UnityEngine;
using Zenject;
using Eflatun.EventBus;

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

	public void Start()
	{
		// Let event bus know we want to listen for the event. 
		// Pass in the listener class as an argument.
		// (You will probably want to pass in 'this' 99% of the time.)
		_eventBus.Listen(this);
	}

	// This will be our event handler. It is required by IEventListener<>. 
	// We get the event object as our parameter.
	public void OnEvent(EventA evnt)
	{
		// The object that emitted the event.
		IEventEmitter<EventA> sender = evnt.Sender;
		
		// Arguments of the event.
		EventA.Args args = evnt.Arguments;
		
		// A property in the event arguments.
		var message = args.Message;
	}
}
```

## Caveats

* If you emit events in Awake or Start, Listeners might not be able to catch them. They also might. So I suggest registering listeners in Awake and emitting in Start, to make sure all listeners receive the event correctly.

* GC allocations. This is fixable by using structs instead of classes for Events. I will work on it.


## License

MIT License. Refer to the [LICENSE](/LICENSE) file.

Copyright (c) 2019 S. Tarık Çetin.

