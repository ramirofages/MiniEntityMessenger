# Mini Entity Messenger 
This is a simple messenger that I wrote for Unity a few months ago when I was doing research on Entity Component Architectures.

During my research I noticed that there were a lot of different implementations on the subject, and most of them didn't put 
enough emphasis in the communication part of the architecture ( which I considered to be critical ).  So after reading as much as I could
get my hands on, I decided to make my own communication system for unity. 

I chose unity because of it's flexibility and also already had an implementation of the Entity Architecture, so a lot of the work was already done.
The only thing that I felt could  be improved was inter-entity communication ( all you have is SendMessage or BroadcastMessage that uses reflexion
and calls the methods by name on any of it's components).

The objective of this little experiment was to learn a bit more about the whole subject while doing emphasis in the communication part. I can't say for sure
that this is a good or bad system but I had fun doing it and learnt a lot. As a proof of concept I used an early version of this in a game called Sweet Revenge
( which you can get here https://play.google.com/store/apps/details?id=com.Skweith.SweetRevenge ). There I put to test the robustness and viability
of the system and was pretty satisfied with the results. It was "good enough" for something that I did myself but it isn't something that I would use in a bigger game.

After I got everything done, I never intended to put this on github or share it since I don't think it would draw someone's attention, being that there are
alot of already good frameworks around  ( Artemis Entity Framework for example, and recently I discovered a german company which did another implementation called 
Entitas and you definitely should check that out ). But I wanted to put the code somewhere else than dropbox, and github seemed like a nice alternative, so here it is.
And also since I never intended to show this to someone else, there are some parts of the code that are ugly and never had the chance or time to refactor it, so sorry for that.

I put an example project with a few sample scenes and a really easy setup so it's easy to understand. It is also worth noting that the core system ( that is the internal messenger
that makes this possible ) is based on gilley033's implementation that he kindly provided on the unity forums ( http://forum.unity3d.com/threads/advanced-c-messenger.112449/#post-1342413 )
and I just extended it to integrate it with what I had in mind for the system ( and also added a custom inspector so it looks cooler ).
