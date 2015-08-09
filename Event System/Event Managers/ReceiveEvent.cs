using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReceiveEvent 
{
	[HideInInspector] public int[] receivingEvents;

	public ReceiveEvent()
	{
		receivingEvents=new int[]{0};
	}


}
