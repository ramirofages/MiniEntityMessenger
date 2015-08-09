using UnityEngine;
using System.Collections;
using System;
[DisallowMultipleComponent]
public class GameObjectState : MonoBehaviour 
{

	
	[SerializeField] EventMessenger messenger;
	BaseMonoBehaviour[] behaviours;

	bool messengerInitialized;

	void Awake()
	{
		behaviours=GetComponents<BaseMonoBehaviour>();
	}

	public EventMessenger Messenger
	{
		get
		{
			if(!messengerInitialized)
			{
				messenger.Initialize(gameObject);
				messengerInitialized=true;
			}
			return messenger;
		}
	}
	
	public BaseMonoBehaviour[] Behaviours
	{
		get
		{
			if(behaviours==null)
			{
				behaviours=GetComponents<BaseMonoBehaviour>();
			}
			return behaviours;
		}
	}
}
