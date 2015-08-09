using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CompositeEventManager
{
	List<int> eventIDs;
	List<List<Delegate>> eventHandlers;
	
	public CompositeEventManager()
	{
		eventIDs=new List<int>();
		eventHandlers=new List<List<Delegate>>();
	}
	
	public void AddListener<T>(int eventID,Action<T> eventHandler)
	{
		Action<T> handler=getEventHandler<T>(eventID);
		if(handler==null)
		{
			handler=eventHandler;
			eventIDs.Add(eventID);
			eventHandlers.Add(new List<Delegate>());
			eventHandlers[eventHandlers.Count-1].Add(handler);
			
		}
		else
		{
			handler+=eventHandler;
		}
	}
	public void RemoveListener<T>(int eventID,Action<T> eventHandler)
	{
		Action<T> handler=getEventHandler<T>(eventID);
		if(handler!=null)
		{
			handler-=eventHandler;
		}
	}
	public void Broadcast<T>(int eventID,T data)
	{
		
		Action<T> handler=getEventHandler<T>(eventID);
		if(handler!=null)
		{
			handler(data);
		} 
	}
	Action<T> getEventHandler<T>(int eventID)
	{
		int eventsCount=eventIDs.Count;
		for(int i=0;i<eventsCount;i++)
		{
			if(eventIDs[i]==eventID)
			{
				int eventHandlersCount=eventHandlers[i].Count;
				for(int j=0;j<eventHandlersCount;j++)
				{
					if(eventHandlers[i][j].GetType()==typeof(Action<T>))
					{
						return (Action<T>)eventHandlers[i][j];
					}
				}
			}
		}
		return null;
		
	}
}