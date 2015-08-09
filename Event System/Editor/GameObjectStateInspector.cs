using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameObjectState))]
public class GameObjectStateInspector : Editor
{

	public override void OnInspectorGUI ()
	{
		base.DrawDefaultInspector();
		if(GUI.changed)
		{
			GameObjectState state=(GameObjectState)target;
			HierarchyIconViewer.UpdateGameObjectView(state.gameObject.GetInstanceID(),state);
		}
	}
}
