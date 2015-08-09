using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public static class GameObjectExtension
{


	public static void Broadcast(this GameObject go,SendEvent eventID) 
	{
		GameObjectState msg=go.GetComponent<GameObjectState>();
		if(msg!=null)
		{
			msg.Messenger.Broadcast(eventID);
		}


	}
	public static void Broadcast<T>(this GameObject go,SendEvent eventID,T messageData) 
	{

		GameObjectState msg=go.GetComponent<GameObjectState>();
		if(msg!=null)
		{
			msg.Messenger.Broadcast<T>(eventID,messageData);
		}
	}
	public static EventMessenger GetMessenger(this GameObject go)
	{
		return go.GetComponent<GameObjectState>().Messenger;
	}


}
