using NaughtyAttributes.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomPropertyDrawer(typeof(DynamicDropdownAttribute))]
public class DynamicDropdownPropertyDrawer : UnityEditor.PropertyDrawer
{

    public override void OnGUI(Rect position,
                      SerializedProperty property, GUIContent label)
    {
        //Debug.Log($"{fieldInfo} :: {property.propertyType}, {property.stringValue} : {attribute.TypeId}");

        var attr = PropertyUtility.GetAttribute<DynamicDropdownAttribute>(property);
        var target = PropertyUtility.GetTargetObject(property);
        var method = ReflectionUtility.GetMethod(target, attr.enumMethodName);
        var values = (IEnumerable<object>)method.Invoke(target, null);

        if (values == null) return;

        // Selected value
        object selectedValue = fieldInfo.GetValue(target);

        // Selected value index
        int selectedValueIndex = 
            Mathf.Max(0, Array.IndexOf(values.ToArray(), selectedValue));

        this.DrawDropdown(target, 
            fieldInfo, 
            property.displayName, 
            selectedValueIndex, 
            values.ToArray(),
            values.Select(i => $"{i}").ToArray());
    }

    public override float GetPropertyHeight(SerializedProperty property,
                                                            GUIContent label)
    {
        var height = base.GetPropertyHeight(property, label);
        //Debug.Log($"{property.ToString()} {height} {EditorGUIUtility.singleLineHeight}");
        return height - 18;
    }

    private void DrawDropdown(UnityEngine.Object target, FieldInfo fieldInfo, string label, int selectedValueIndex, object[] values, string[] displayOptions)
    {
        EditorGUI.BeginChangeCheck();

        int newIndex = EditorGUILayout.Popup(label, selectedValueIndex, displayOptions);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Dropdown");
            fieldInfo.SetValue(target, values[newIndex]);
        }
    }
}
