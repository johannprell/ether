# Ether

Modular real-time variable / signal flow system.

Inspired by guitar stomp-boxes and modular synthesizers,
the goal is to create a modular system where simple components
can create complex results.

Semantics are loosely based on radio communication:

### Carrier
A float value with range 0 to 1. Transmits itself to the "Ether"
via the global **Carrier Stream**, or locally to a **Receiver** on
the same gameObject.
Carrier output is always clamped between 0 and 1.

### Signature
Assets called **Signature Tokens** can be created and used to identify streamed
carriers, both for transmitting and reading. The main purpose of this is the
ability to read the same carrier value from many different locations, similar to
how pub-sub events work.

### Receiver
Reads a Carrier signal from stream or locally. Can also be set
to read itself, essentially creating a feedback loop.
Receivers both read the carrier value and run it through **modulators**
and **effectors**.

### Modulator
A component that implements the **IModulator** interface.
Takes the carrier input, modulates the value in some fashion
and outputs the result.
If several modulators are attached to a Receivers gameObject,
they will be chained, like stomp effects on a pedal board.

Combined output of all attached modulators is clamped between -10 and 10. This leaves
some headroom to mess with the original carrier value, much like
an analog audio signal can be over-driven, but still keeps things within
a uniform set of bounds that **Effectors** can expect to work within.

### Effector
A component that implements the **IEffector** interface.
Takes the modulated value and performs an effect, or action,
based on the value. Many effectors can be attached to the same
Receiver - they will take the same input signal but otherwise not
influence each other (unless they are programmed to).
Effector output or result can be absolutely anything.

This is where you turn modulated values into animations, colors, sounds,
and fun effects!

### Automation
An additional Automation Receiver can be attached. Much like
a Carrier Receiver, this reads the value of either a streamed carrier,
a local carrier, or from itself.

All modulators and effectors that are attached and have implemented
the interface for automation will take automation input with one float value.
The automation input can be used for anything.
 
 ### Played Components
 Effectors and Modulators can implement the **IPlayed** interface, and add
 behavior that can be played (triggered) such as envelopes.

These are the building blocks included in Ether - combine, extend, and experiment!
