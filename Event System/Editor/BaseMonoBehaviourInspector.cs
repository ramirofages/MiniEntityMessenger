using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
[CustomEditor(typeof(BaseMonoBehaviour),true)]
public class BaseMonoBehaviourInspector : Editor
{

	List<SenderEventWrapper> senders;
	List<ReceiverEventWrapper> receivers;
	EventDatabase eventDatabase;
	SerializedObject inspectedObject;
	string[] eventNames;
	void OnEnable()
	{
		eventDatabase=Resources.Load<EventDatabase>("EventDatabase");
		eventNames=eventDatabase.GetSortedEventNames().ToArray();
		showConfig=false;
		showAdditionalInfo=false;
		senders=new List<SenderEventWrapper>();
		receivers=new List<ReceiverEventWrapper>();
		inspectedObject=new SerializedObject(target);



		FieldInfo[] objectMembers=target.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);  

		foreach(FieldInfo member in objectMembers)
		{
			if(member.FieldType==typeof(ReceiveEvent))
			{
				// Conseguimos los eventos a los que se escucha 
				SerializedProperty receivingEvents=inspectedObject.FindProperty(member.Name).FindPropertyRelative("receivingEvents");

				List<int> receivingEventIndex=new List<int>();
				List<SerializedProperty> receivingEventHolder=new List<SerializedProperty>();


				for(int i=0;i<receivingEvents.arraySize;i++)
				{
					receivingEventIndex.Add(FindNameIndex(receivingEvents.GetArrayElementAtIndex(i).intValue));
					receivingEventHolder.Add(receivingEvents.GetArrayElementAtIndex(i));
				}

				
				receivers.Add(new ReceiverEventWrapper(member.Name,receivingEventIndex,receivingEventHolder));

			}
			else
			{
				if(member.FieldType==typeof(SendEvent))
				{
					SerializedProperty senderEvent=inspectedObject.FindProperty(member.Name).FindPropertyRelative("senderEvent");

					if(senderEvent.intValue==0)
					{
						showConfig=true;
					}
					SenderEventWrapper ep=new SenderEventWrapper(member.Name,FindNameIndex(senderEvent.intValue),senderEvent);
					senders.Add(ep);
				}
			}

		}

		BaseMonoBehaviour baseMono=(BaseMonoBehaviour)target;
		object[] attributes=baseMono.GetType().GetCustomAttributes(true);
		foreach(object att in attributes)
		{
			if(att is ExtraInfoAttribute)
			{
				ExtraInfoAttribute b=(ExtraInfoAttribute)att;
				additionalInfo=b.Info;
				showAdditionalInfo=true;
			}
		}




	}
	bool showConfig;
	bool showAdditionalInfo;
	string additionalInfo;
	
	public override void OnInspectorGUI()
	{
		if(senders.Count>0 || receivers.Count>0 || showAdditionalInfo)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("");
			if(GUILayout.Button("config",GUILayout.Width(45),GUILayout.Height(19)))
			{
				showConfig= !showConfig;
			}
			GUILayout.EndHorizontal();
		}
		
		if(showConfig)
		{

			ShowEventsConfiguration();
			if(showAdditionalInfo)
			{
				GUILayout.Label("--------------------------------------");
				ShowAdditionalInfo();
			}

		}
		else
		{
			DrawDefaultInspector();
		}
		
	}
	void ShowAdditionalInfo()
	{
		GUILayout.Label("Info:",EditorStyles.boldLabel);			
				
		GUILayout.BeginHorizontal();
		GUILayout.Space(10);
		GUILayout.Label(additionalInfo);			
		GUILayout.EndHorizontal();

	}

	void ShowEventsConfiguration()
	{
		inspectedObject.Update();
		if(senders.Count>0)
		{
			GUILayout.Label("Sends:",EditorStyles.boldLabel);
			GUILayout.Space(5);
			ShowSenders();
		}

		if(receivers.Count>0)
		{
			GUILayout.Label("Receives:",EditorStyles.boldLabel);
			GUILayout.Space(5);
			ShowReceivers();
		}

		if(GUI.changed)
		{
			inspectedObject.ApplyModifiedProperties();
		}
	}
	void ShowSenders()
	{
		int sendersCount = senders.Count;
		for(int i=0;i<sendersCount;i++)
		{

			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			GUILayout.Label(senders[i].name,GUILayout.Width(150));
			senders[i].index=EditorGUILayout.Popup(senders[i].index,eventNames);
			if(GUI.changed)
			{
				senders[i].eventID.intValue=eventDatabase.GetEventID(eventNames[senders[i].index]);
			}

			GUILayout.EndHorizontal();

		}
	}
	void ShowReceivers()
	{
		int receiversCount=receivers.Count;
		for(int i=0;i<receiversCount;i++)
		{

			GUILayout.BeginHorizontal();
				GUILayout.Space(10);
				GUILayout.Label(receivers[i].name,GUILayout.Width(150));
				if(GUILayout.Button("+",GUILayout.Width(25)) || receivers[i].index.Count==0)
				{
					SerializedProperty eventReceivers=inspectedObject.FindProperty(receivers[i].name).FindPropertyRelative("receivingEvents");;

					eventReceivers.InsertArrayElementAtIndex(eventReceivers.arraySize);
					eventReceivers.GetArrayElementAtIndex(eventReceivers.arraySize-1).intValue=eventDatabase.GetEventID(eventNames[0]);

					receivers[i].index.Add(0);
					receivers[i].prop.Add(eventReceivers.GetArrayElementAtIndex(eventReceivers.arraySize-1));

					
					inspectedObject.ApplyModifiedProperties();
					return;
				}

			    if(GUILayout.Button("-",GUILayout.Width(25)))
				{
				if(receivers[i].index.Count>1)
					{
						receivers[i].index.RemoveAt(receivers[i].index.Count-1);
						receivers[i].prop.RemoveAt(receivers[i].prop.Count-1);
						SerializedProperty eventReceivers=inspectedObject.FindProperty(receivers[i].name).FindPropertyRelative("receivingEvents");;
						eventReceivers.DeleteArrayElementAtIndex(eventReceivers.arraySize-1);
						inspectedObject.ApplyModifiedProperties();
						return;
					}
					
				}
				GUILayout.Space(20);
				GUILayout.BeginVertical();
					for(int j=0;j<receivers[i].index.Count;j++)
					{
						receivers[i].index[j]=EditorGUILayout.Popup(receivers[i].index[j],eventNames);

						if(GUI.changed)
						{
							receivers[i].prop[j].intValue=eventDatabase.GetEventID(eventNames[receivers[i].index[j]]);
						}
					}
				GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.Space(10);
			
		}
	}

	int FindNameIndex(int eventID)
	{
		string name=eventDatabase.GetEventName(eventID);
		for(int i=0;i<eventNames.Length;i++)
		{
			if(name==eventNames[i])
			{
				return i;
			}
		}
		
		return 0;
	}

		                        
}

class SenderEventWrapper
{
	public string name;
	public int index;
	public SerializedProperty eventID;
	
	public SenderEventWrapper(string eventName,int i,SerializedProperty p)
	{
		name=eventName;
		index=i;
		eventID=p;
	}
	
}

class ReceiverEventWrapper
{
	public string name;
	public List<int> index;
	public List<SerializedProperty> prop;
	
	public ReceiverEventWrapper(string eventName,List<int> i,List<SerializedProperty> p)
	{
		name=eventName;
		index=i;
		prop=p;
		
	}
}
