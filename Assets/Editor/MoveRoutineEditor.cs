using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoveRoutine))]
public class MoveRoutineEditor : RoutineEditor
{
	SerializedProperty grid;

	void OnEnable()
	{
		grid = serializedObject.FindProperty("targetGrid");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUI.indentLevel++;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Move to ");
		EditorGUILayout.PropertyField(grid, GUIContent.none);
		EditorGUILayout.EndHorizontal();
		EditorGUI.indentLevel--;

		serializedObject.ApplyModifiedProperties();
	}

	public override void DrawPropertyDrawer(Rect rect)
	{
		var width1 = 60;

		serializedObject.Update();
		EditorGUI.LabelField(new Rect(rect.x, rect.y, width1, rect.height), "Move to ");
		EditorGUI.PropertyField(new Rect(rect.x + width1, rect.y, rect.width - width1, rect.height), grid, GUIContent.none);
		serializedObject.ApplyModifiedProperties();
	}
}
