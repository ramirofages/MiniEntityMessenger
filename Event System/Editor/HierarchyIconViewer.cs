using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
[InitializeOnLoad]
public class HierarchyIconViewer
{
	static Texture2D replicateIcon;
	static Texture2D overrideIcon;
	static List<int> replicateOnChildren;
	static List<int> overrideEvent;

	static HierarchyIconViewer ()
	{
		replicateIcon= Resources.Load<Texture2D>("replicate");
		overrideIcon= Resources.Load<Texture2D>("override");

		EditorApplication.hierarchyWindowChanged += UpdateHierarchyState;
		EditorApplication.hierarchyWindowItemOnGUI += ShowHierarchyIcons;

		EditorApplication.delayCall+=asd;

	}

	static void asd()
	{
		UpdateHierarchyState();

	}
	public static void UpdateGameObjectView(int instanceID,GameObjectState goState)
	{
		if(replicateOnChildren ==null)
		{
			replicateOnChildren=new List<int>();
			overrideEvent=new List<int>();
			EditorApplication.hierarchyWindowChanged();
		}
		CheckForReplicate(instanceID,goState);
		CheckForOverride(instanceID,goState);
	}
	static void CheckForReplicate(int instanceID,GameObjectState goState)
	{
		if(goState.Messenger.replicateEventOnChildren)
		{
			if(!replicateOnChildren.Contains(instanceID))
			{
				replicateOnChildren.Add(instanceID);
			}
		}
		else
		{
			if(replicateOnChildren.Contains(instanceID))
			{
				replicateOnChildren.Remove(instanceID);
			}
		}
		EditorApplication.RepaintHierarchyWindow();
	}
	static void CheckForOverride(int instanceID,GameObjectState goState)
	{
		if(goState.Messenger.overrideEventForSignal)
		{
			if(!overrideEvent.Contains(instanceID))
			{
				overrideEvent.Add(instanceID);
			}
		}
		else
		{
			if(overrideEvent.Contains(instanceID))
			{
				overrideEvent.Remove(instanceID);
			}
		}
		EditorApplication.RepaintHierarchyWindow();
	}


	static void UpdateHierarchyState ()
	{

		GameObject[] go = GameObject.FindObjectsOfType<GameObject>();
		replicateOnChildren = new List<int> ();
		overrideEvent=new List<int>();
		foreach (GameObject g in go)
		{
			GameObjectState state=g.GetComponent<GameObjectState> ();
			if (state != null)
			{
				if(state.Messenger.replicateEventOnChildren)
				{
					replicateOnChildren.Add (g.GetInstanceID ());
				}
				if(state.Messenger.overrideEventForSignal)
				{
					overrideEvent.Add(g.GetInstanceID());
				}
			}
		}
	}
	static void ShowHierarchyIcons (int instanceID, Rect selectionRect)
	{
		// place the icoon to the right of the list:
		Rect r = new Rect (selectionRect);

		//r.width = 20;
		if (replicateOnChildren != null && overrideEvent != null )
		{
			if(replicateOnChildren.Contains (instanceID))
			{
				r.x = r.width - 23;
				GUI.Label (r, replicateIcon);
			}
			if(overrideEvent.Contains (instanceID))
			{
				r.x = r.width - 5;
				r.width = 20;
				GUI.Label (r, overrideIcon);
			}
		}
	}
}