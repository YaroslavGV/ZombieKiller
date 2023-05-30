using UnityEngine;
using UnityEditor;

namespace StatSystem
{
    [CustomPropertyDrawer(typeof(StatValue))]
    public class StatValueDrawer : PropertyDrawer
    {
        private const string Type = "type";
        private const string Value = "value";
        private const float SpaceWidth = 2;
        private const float ValueWidth = 80;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            bool typeEditable = attribute is StatTypeNotEdititable == false;
            Draw(position, property, label, typeEditable);
        }

        public static void Draw (Rect position, SerializedProperty property, GUIContent label, bool typeEditable)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            float typeWidth = position.width - ValueWidth - SpaceWidth;
            float typePosition = position.x;
            float valuePosition = typePosition + SpaceWidth + typeWidth;
            var typeRect = new Rect(typePosition, position.y, typeWidth, position.height);
            var valueRect = new Rect(valuePosition, position.y, ValueWidth, position.height);

            property.DrawRelativeEnum(Type, typeRect, typeEditable);
            EditorGUI.PropertyField(valueRect, property.FindPropertyRelative(Value), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}