using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return ShouldShow(property) ? EditorGUI.GetPropertyHeight(property, label, true) : 0f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (ShouldShow(property))
            EditorGUI.PropertyField(position, property, label, true);
    }

    private bool ShouldShow(SerializedProperty property)
    {
        var attr = (ShowIfAttribute)attribute;

        SerializedProperty conditionProp =
            property.serializedObject.FindProperty(attr.ConditionFieldName);

        if (conditionProp == null)
        {
            Debug.LogWarning(
                $"ShowIf: Field '{attr.ConditionFieldName}' not found on {property.serializedObject.targetObject.GetType().Name}"
            );
            return true;
        }

        if (conditionProp.propertyType != SerializedPropertyType.Boolean)
        {
            Debug.LogWarning(
                $"ShowIf: Field '{attr.ConditionFieldName}' is not a bool."
            );
            return true;
        }

        bool value = conditionProp.boolValue;
        return attr.Invert ? !value : value;
    }
}
