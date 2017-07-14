using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System;

[CustomEditor(typeof(RoutineCollection))]
public class RoutineCollectionEditor : Editor
{
	RoutineCollection rc;
	List<Routine> routines;

	ReorderableList list;

	void OnEnable()
	{
		rc = (RoutineCollection)target;
		routines = rc.routines;

		CreateReorderableList();
	}

	void CreateReorderableList()
	{
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("routines"),
		 true, true, true, true);

		list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				if (element == null)
					return;
				var editor = CreateEditor(element.objectReferenceValue) as RoutineEditor;
				if (editor == null)
					return;
				editor.DrawPropertyDrawer(rect);
		};

		list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {  
			var menu = new GenericMenu();
			var routineTypes = AssetDatabase.FindAssets("", new[]{"Assets/Scripts/Routines"});
			foreach (var routine in routineTypes) {
				var path = AssetDatabase.GUIDToAssetPath(routine);
				menu.AddItem(new GUIContent(Path.GetFileNameWithoutExtension(path)), 
				false, clickHandler, new routineDataForCreate(){name = Path.GetFileNameWithoutExtension(path)});
			}
			menu.ShowAsContext();
		};

		list.onRemoveCallback = (ReorderableList l) =>
		{
			var element = l.serializedProperty.GetArrayElementAtIndex(l.index).objectReferenceValue as Routine;
			Undo.RecordObject(rc, "Remove");
			routines.RemoveAt(l.index);
			// Undo.DestroyObjectImmediate(element);
			Undo.IncrementCurrentGroup();
		};

		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Routines");
		};
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		DrawList();
		serializedObject.ApplyModifiedProperties();
	}

	void DrawList()
	{
		// foreach(var routine in rc.routines)
		// {
		// 	var editor = CreateEditor(routine);
		// 	editor.OnInspectorGUI();
		// }

		list.DoLayoutList();

		// if (GUILayout.Button("Clear"))
		// {
		// 	foreach (var routine in routines)
		// 	{
		// 		DestroyImmediate(routine);
		// 	}
		// 	routines.Clear();
		// }
	}

	void clickHandler(object obj)
	{
		var routineData = (routineDataForCreate)obj;
		var routine = ScriptableObject.CreateInstance(routineData.name) as Routine;
		Undo.RecordObject(rc, "Add");
		routines.Add(routine);
		Undo.IncrementCurrentGroup();
	}

	private struct routineDataForCreate
	{
		public string name;
	}
}
