using UnityEngine;
using UnityEditor;

namespace Collection
{
    [CustomPropertyDrawer(typeof(DropFactor))]
    public class DropFactorDrawer : PropertyDrawer
    {
        private const string Drop = "drop";
        private const string Factor = "factor";
        private const float SpaceWidth = 2;
        private const float FactorWidth = 80;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float droppWidth = position.width - FactorWidth - SpaceWidth;
            float dropPosition = position.x;
            float factorPosition = dropPosition + droppWidth + SpaceWidth;
            var dropRect = new Rect(dropPosition, position.y, droppWidth, position.height);
            var factorRect = new Rect(factorPosition, position.y, FactorWidth, position.height);

            EditorGUI.PropertyField(dropRect, property.FindPropertyRelative(Drop), new GUIContent(""));
            EditorGUI.PropertyField(factorRect, property.FindPropertyRelative(Factor), new GUIContent(""));

            EditorGUI.EndProperty();
        }
    }
}