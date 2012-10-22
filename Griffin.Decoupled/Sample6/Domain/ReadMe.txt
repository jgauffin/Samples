This namespace represents the Domain project/assembly in which the Domain Model 
(i.e. the business classes which are POCOs without dependencies to anything else such OR/Ms etc)

I try to follow DDD within this project (in my own applications).

Note that the domain events are declared in this project while the commands are not. 
That's because the events are part of the domain model (generated from it) while the commands
are just acting on the domain model.