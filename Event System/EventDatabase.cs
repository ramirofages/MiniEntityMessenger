using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventDatabase : ScriptableObject,ISerializationCallbackReceiver
{
	Dictionary<int,EventInfo> events;
	[SerializeField] List<EventInfo> sortedEvents;

	public void AddEvent (string eventName)
	{
		EventInfo newEvent=new EventInfo(eventName,"");
		events.Add(GetEventID(eventName),newEvent);
		sortedEvents.Add(newEvent);
		sortedEvents.Sort(new ordenar());

	}

	public List<string> GetSortedEventNames ()
	{
		List<string> names=new List<string>();

		foreach(EventInfo eventInfo in sortedEvents)
		{
			names.Add(eventInfo.Name);
		}
		return names;
	}
	public List<EventInfo> GetSortedEvents()
	{
		return sortedEvents;
	}

	public string GetEventName (int eventID)
	{
		if(eventID!=0)
		{
			EventInfo eventInfo;
			if(events.TryGetValue(eventID,out eventInfo))
		    {
				return eventInfo.Name;
			}
			Debug.Log(eventID.ToString()+" NOT FUCKING FOUND");
		}
		return "NOT FUCKING FOUND";
	}

	public int GetEventID (string eventName)
	{
		return Animator.StringToHash(eventName);
	}

 
	public void OnAfterDeserialize ()
	{

		events = new Dictionary<int,EventInfo>();
		if(sortedEvents==null)
		{
			sortedEvents=new List<EventInfo>();
			Debug.Log("creado sorted events");
		}
		foreach(EventInfo name in sortedEvents)
		{
			events.Add(GetEventID(name.Name),name);
		}
	}



	// basura

	class ordenar:IComparer<EventInfo>
	{
		public int Compare( EventInfo x, EventInfo y )
		{
			return string.Compare( x.Name,y.Name);
		}	
	}
	
	public void OnBeforeSerialize()
	{

	}
}


