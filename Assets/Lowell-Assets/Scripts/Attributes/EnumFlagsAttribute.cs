using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() {}

    public static List<T> GetSelected<T>(T property) where T : System.Enum {
        List<T> selected = new List<T>();
        for (int i = 0; i < System.Enum.GetValues(typeof(T)).Length; i++) {
            int layer = 1 << i;
            if ((Convert.ToInt32(property) & layer) != 0) {
                T value = (T)Enum.ToObject(typeof(T), i);
                selected.Add(value);
            }
        }

        return selected;
    }
}

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.intValue = EditorGUI.MaskField(position, label, property.intValue, property.enumNames);
    }
}