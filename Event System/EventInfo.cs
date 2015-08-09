using UnityEngine;
using System.Collections;


[System.Serializable]
public class EventInfo
{
	[SerializeField] string name;
	[SerializeField] string description;
	
	public string Name
	{
		get{ return name;}
	}
	public string Description
	{
		get{ return description;}
		set{ description= value; }
	}
	public EventInfo(string name, string description)
	{
		this.name=name;
		this.description=description;
	}
}