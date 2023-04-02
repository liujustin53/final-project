using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIf))]
public class DrawIfDrawer : PropertyDrawer
{
    DrawIf drawIf;
    SerializedProperty comparedField;

    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DrawIf drawIf = attribute as DrawIf;
        var toggle = property.serializedObject.FindProperty(drawIf.toggleName);

        propertyHeight = base.GetPropertyHeight(property, label);

        if (toggle.boolValue != drawIf.invert)
        {
            EditorGUI.PropertyField(position, property);
        }
        else
        {
            propertyHeight = 0;
        }
    }
}
