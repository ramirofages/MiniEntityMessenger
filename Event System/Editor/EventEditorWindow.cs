using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
public class EventEditorWindow : EditorWindow
{

	List<EventInfo> eventList;
	EventDatabase eventDatabase;
	int selectedEvent;
	Vector2 scroll;
	string newEventName;

	[MenuItem("Database/Events editor")]
	static void showWindow()
	{
		EditorWindow.GetWindow(typeof(EventEditorWindow));
	}

	void OnEnable()
	{
		eventDatabase=Resources.Load<EventDatabase>("EventDatabase");
		eventList=eventDatabase.GetSortedEvents();
		selectedEvent = -1;
		newEventName="";
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(10,10,165,position.height));
		ShowEventList();
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(200,10,400,position.height));
		ShowSelectedEventInfo();
		GUILayout.EndArea();

		GUILayout.BeginArea(new Rect(470,10,400,position.height));
		ShowNewEventScreen();
		GUILayout.EndArea();

	}
	void ShowEventList()
	{
		GUILayout.Label("Event list: ",EditorStyles.boldLabel);
		GUILayout.Space(5);
		scroll=GUI.BeginScrollView(new Rect(0,20,165,position.height-30),scroll,new Rect(0,20,140,eventList.Count*19)); // 19 numero magico, equivale a masomenos el alto de cada item de texto

		for(int i=0;i<eventList.Count;i++)
		{
			
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			if(GUILayout.Button(eventList[i].Name))
			{
				selectedEvent=i;
			}
			GUILayout.EndHorizontal();
		}
		GUI.EndScrollView();
	}

	void ShowSelectedEventInfo()
	{
		GUILayout.Label("Event information:",EditorStyles.boldLabel);
		GUILayout.Space(10f);
		GUILayout.BeginHorizontal();
			GUILayout.Space(10f);
			GUILayout.BeginVertical();
				if(selectedEvent != -1 )
				{
					GUILayout.Label("Name: "+eventList[selectedEvent].Name);
					GUILayout.Space(5f);
					GUILayout.Label("Description: ");
					eventList[selectedEvent].Description=GUILayout.TextArea(eventList[selectedEvent].Description,GUILayout.Height(150),GUILayout.Width(250));
				}
			GUILayout.EndVertical();
		GUILayout.EndHorizontal();
	}

	void ShowNewEventScreen()
	{
		GUILayout.Label("Create event:",EditorStyles.boldLabel);
		GUILayout.Space(10f);
		GUILayout.BeginHorizontal();
			GUILayout.Label("New event name:",GUILayout.Width(100));
			newEventName=GUILayout.TextField(newEventName,GUILayout.Width(150));
		GUILayout.EndHorizontal();
		GUILayout.Space(10);
		if(GUILayout.Button("Create",GUILayout.Width(100)))
		{
			if(newEventName=="")
			{
				Debug.LogWarning("Must put a name");
				return;
			}
				
			foreach(EventInfo eventInfo in eventList)
			{
				if(eventInfo.Name==newEventName)
				{
					Debug.LogError("Name already exists");
					return;
				}
					
			}
			eventDatabase.AddEvent(newEventName);
			eventList=eventDatabase.GetSortedEvents();
			selectedEvent = -1;
			EditorUtility.SetDirty(eventDatabase);
			AssetDatabase.SaveAssets();

		}
	}
}
