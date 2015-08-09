using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class EventManager
{
	List<int> eventIDs;
	List<Action> eventHandlers;

	public EventManager()
	{
		eventIDs=new List<int>();
		eventHandlers=new List<Action>();
	}

	public void AddListener(int eventID,Action eventHandler)
	{

		Action handler=getEventHandler(eventID);
		if(handler==null)
		{
			eventIDs.Add(eventID);
			eventHandlers.Add(eventHandler);
		}
		else
		{
			handler+=eventHandler;
		}
	}


	public void RemoveListener(int eventID,Action eventHandler)
	{
		Action handler=getEventHandler(eventID);
		if(handler!=null)
		{
			handler-=eventHandler;
		}

	}

	public void Broadcast(int eventID)
	{
		Action handler=getEventHandler(eventID);
		if(handler!=null)
		{
			handler();
		} 
	}



	Action getEventHandler(int eventID)
	{
		int eventsCount=eventIDs.Count;
		for(int i=0;i<eventsCount;i++)
		{
			if(eventIDs[i]==eventID)
			{
				return eventHandlers[i];
			}
		}
		return null;
	
	}
}

