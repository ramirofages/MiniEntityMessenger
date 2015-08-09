using UnityEngine;
using System.Collections;


[RequireComponent(typeof(GameObjectState))]
//[OtherInfo("None")]
public class BaseMonoBehaviour : MonoBehaviour 
{
	
	EventMessenger msg;

	protected EventMessenger Messenger
	{
		get
		{ 
			if(msg==null)
			{
				GameObjectState gameObjectState=gameObject.GetComponent<GameObjectState>();
				if(gameObjectState==null)
				{
					print(msg);
					msg=gameObject.AddComponent<GameObjectState>().Messenger;

				}
				else
				{
					msg=gameObjectState.Messenger;
				}

			}

			return msg;

		}
	}

}
