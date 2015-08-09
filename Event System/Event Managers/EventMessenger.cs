using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class EventMessenger 
{
	EventManager eventManager;
	CompositeEventManager compositeEventManager;

	SendEvent signalEvent;
	GameObject gameObject;

	public bool replicateEventOnChildren;
	public bool overrideEventForSignal;


	//################################## Constructor #############################################

	
	public void Initialize(GameObject gameObject)
	{
		this.gameObject=gameObject;
		eventManager=new EventManager();
		compositeEventManager=new CompositeEventManager();

		signalEvent=new SendEvent();
		signalEvent.senderEvent=Animator.StringToHash("Signal");

	}

	
	//###########################################################################################


	public void AddListener(ReceiveEvent eventID,Action eventHandler)
	{

		foreach(int id in eventID.receivingEvents)
		{
			eventManager.AddListener(id,eventHandler);
		}
	}
	public void AddListener<T>(ReceiveEvent eventID,Action<T> eventHandler)
	{
		foreach(int id in eventID.receivingEvents)
		{
			compositeEventManager.AddListener<T>(id,eventHandler);
		}
	}
	
	public void Broadcast(SendEvent eventID)
	{

		if(overrideEventForSignal)
		{
			BroadcastSignalEvent();
		}
		else
		{
			// Flujo normal
			eventManager.Broadcast(eventID.senderEvent);

			if(replicateEventOnChildren)
			{
				Transform transform=gameObject.transform;;
				int childCount=transform.childCount;

				for(int i=0;i<childCount;i++)
				{
					transform.GetChild(i).gameObject.Broadcast(eventID);
				}

			}
		}




	}
	public void Broadcast<T>(SendEvent eventID,T messageData)
	{

		if(overrideEventForSignal)
		{
			BroadcastSignalEvent();
		}
		else
		{
			// Flujo normal

			compositeEventManager.Broadcast<T>(eventID.senderEvent,messageData);
			Broadcast(eventID);  // le damos compatibilidad a los broadcast compuestos
			if(replicateEventOnChildren)
			{
				Transform transform=gameObject.transform;;
				int childCount=transform.childCount;
				for(int i=0;i<childCount;i++)
				{
					transform.GetChild(i).gameObject.Broadcast<T>(eventID,messageData);
				}
			}
		}
	}

	void BroadcastSignalEvent()
	{
		eventManager.Broadcast (signalEvent.senderEvent);
		if(replicateEventOnChildren)
		{
			Transform transform=gameObject.transform;;
			int childCount=transform.childCount;
			for(int i=0;i<childCount;i++)
			{
				transform.GetChild(i).gameObject.Broadcast (signalEvent);
			}
		}
	}
	
	public void RemoveListener(ReceiveEvent eventID,Action eventHandler)
	{
		foreach(int id in eventID.receivingEvents)
		{
			eventManager.RemoveListener(id,eventHandler);
		}
	}
	public void RemoveListener<T>(ReceiveEvent eventID,Action<T> eventHandler)
	{
		foreach(int id in eventID.receivingEvents)
		{
			compositeEventManager.RemoveListener<T>(id,eventHandler);
		}
	}


}
