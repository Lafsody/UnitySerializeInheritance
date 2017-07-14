using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(RoutineCollection))]
public class RoutineCollectionEditor : Editor
{
	RoutineCollection rc;
	List<Routine> routines;

	void OnEnable()
	{
		rc = (RoutineCollection)target;
		routines = rc.routines;
		Debug.Log(target.GetType());
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawList();

		serializedObject.ApplyModifiedProperties();
	}

	void DrawList()
	{
		EditorGUILayout.LabelField("Routines");

		EditorGUI.indentLevel++;

		foreach(var routine in rc.routines)
		{
			EditorGUILayout.LabelField(routine.GetName());
		}

		if (GUILayout.Button("Add Wait Routine"))
		{
			var routine = ScriptableObject.CreateInstance("WaitRoutine") as WaitRoutine;
			routines.Add(routine);
		}

		if (GUILayout.Button("Add Move Routine"))
		{
			var routine = ScriptableObject.CreateInstance("MoveRoutine") as MoveRoutine;
			routines.Add(routine);
		}

		if (GUILayout.Button("Clear"))
		{
			foreach (var routine in routines)
			{
				DestroyImmediate(routine);
			}
			routines.Clear();
		}

		EditorGUI.indentLevel--;
	}
}
