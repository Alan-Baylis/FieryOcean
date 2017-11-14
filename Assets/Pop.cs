﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SomeClass))]
public class SomeEditor : Editor
{
    string[] _choices = new[] { "forward", "middle","backward" };
    int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        var someClass = target as SomeClass;
        // Update the selected choice in the underlying object
        someClass.choice = _choices[_choiceIndex];
        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}
