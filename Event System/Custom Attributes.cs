using UnityEngine;
using System.Collections;
using System;


[AttributeUsage(AttributeTargets.Class)]
public class ExtraInfoAttribute : System.Attribute
{
	string info;
	public ExtraInfoAttribute(string info)
	{
		this.info=info;		
	}
	public string Info
	{
		get
		{
			return info;
		}
	}
	
}


public class SendAttribute : PropertyAttribute
{
	
}


public class ReceiveAttribute : PropertyAttribute
{
	
}

public class EventAttribute : PropertyAttribute
{
	
}
