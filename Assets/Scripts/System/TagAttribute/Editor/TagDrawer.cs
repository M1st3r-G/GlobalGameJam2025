using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;
            int selectedIndex = Mathf.Max(0, System.Array.IndexOf(tags, property.stringValue));

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, tags);
            property.stringValue = tags[selectedIndex];

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}