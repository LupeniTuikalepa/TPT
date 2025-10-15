using Helteix.Singletons.PrefabMapping;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helteix.Singletons.Editor
{
    [CustomPropertyDrawer(typeof(SingletonPrefabReference))]
    public class SingletonReferenceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                }
            };

            container.Add(new PropertyField(property.FindPropertyRelative("singletonType"))
            {
                style =
                {
                    width = 350,
                    marginRight = 10
                }
            });
            container.Add(new EnumField()
            {
                bindingPath = "referenceType",
                style =
                {
                    width = 150,
                    paddingLeft = 15,
                    paddingRight = 15,
                }
            });
            container.Add(new PropertyField(property.FindPropertyRelative("assetRef"))
            {
                style =
                {
                    flexGrow = 1,
                }
            });

            container.Bind(property.serializedObject);
            return container;
        }
    }
}