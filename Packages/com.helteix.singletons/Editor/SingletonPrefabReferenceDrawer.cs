using Helteix.Singletons.PrefabMapping;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helteix.Singletons.Editor
{
    [CustomPropertyDrawer(typeof(PrefabReference))]
    public class SingletonPrefabReferenceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("prefab"))
            {
                label = "",
                style =
                {
                    flexGrow = 1,
                }
            };
        }
    }
    [CustomPropertyDrawer(typeof(ResourceReference))]
    public class SingletonResourceReferenceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new PropertyField(property.FindPropertyRelative("path"))
            {
                label = "",
                style =
                {
                    flexGrow = 1,
                }
            };
        }
    }
}