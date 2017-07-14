using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaitRoutine))]
public class WaitRoutineEditor : RoutineEditor
{
	SerializedProperty waitTime;

	void OnEnable()
	{
		waitTime = serializedObject.FindProperty("waitTime");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUI.indentLevel++;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Wait ");
		EditorGUILayout.PropertyField(waitTime, GUIContent.none);
		EditorGUILayout.EndHorizontal();
		EditorGUI.indentLevel--;

		serializedObject.ApplyModifiedProperties();
	}

	public override void DrawPropertyDrawer(Rect rect)
	{
		var width1 = 40;
		var width2 = 30;

		serializedObject.Update();
		EditorGUI.LabelField(new Rect(rect.x, rect.y, width1, rect.height), "Wait ");
		EditorGUI.PropertyField(new Rect(rect.x + width1, rect.y, rect.width - width1 - width2, rect.height), waitTime, GUIContent.none);
		EditorGUI.LabelField(new Rect(rect.x + rect.width - width2, rect.y, width2, rect.height), " TU");
		serializedObject.ApplyModifiedProperties();
	}
}
