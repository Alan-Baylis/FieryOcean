using UnityEngine;
using UnityEditor;
using System.Collections;

// Tell the RangeDrawer that it is a drawer for properties with the RangeAttribute.

[CustomPropertyDrawer(typeof(InspectorNameAttribute))]
public class InspectorNameDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// First get the attribute since it contains the range for the slider
		InspectorNameAttribute propertyRename = attribute as InspectorNameAttribute;
		EditorGUI.PropertyField(position, property, new GUIContent(propertyRename.name));
	}
}
