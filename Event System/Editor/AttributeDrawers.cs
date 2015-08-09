using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(SendAttribute))]
public class SendAttributeDrawer : PropertyDrawer
{


	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return 0;
	}
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
	{

	}
}

[CustomPropertyDrawer(typeof(ReceiveAttribute),false)]
public class ReceiveAttributeDrawer : PropertyDrawer
{
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return 0;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
	{
		position.y=20;
	}
}


[CustomPropertyDrawer(typeof(EventAttribute),false)]
public class EventAttributeDrawer : PropertyDrawer
{
	
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return 0;
	}
	
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
	{
		position.y=20;
	}
}
