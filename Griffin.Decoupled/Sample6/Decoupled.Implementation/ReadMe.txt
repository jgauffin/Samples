This namespace represents the Decoupled.Implementation project. 

All command handlers are located in here. If they use your OR/M directly or not is up to you. Just make sure that the commands
are testable.

The reason to why I split up the commands and their handlers in different projects is to allow me to move the handlers to a seperate
process or server later. (the GUI just got a reference to the Decoupled and not to Decoupled.Implementation).

