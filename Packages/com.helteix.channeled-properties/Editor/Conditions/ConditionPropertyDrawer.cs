using Helteix.ChanneledProperties.Conditions;
using UnityEditor;

namespace Helteix.ChanneledProperties.Editor.Conditions
{
    [CustomPropertyDrawer(typeof(Condition))]
    public class ConditionPropertyDrawer : ChanneledPropertyDrawer
    {
        protected override ChanneledPropertyElement<TValue> GetElement<TValue>(SerializedProperty property, IChanneledPropertyEditor<TValue> channeledProperty)
        {
            return  new ConditionPropertyElement((IChanneledPropertyEditor<bool>)channeledProperty, property) as ChanneledPropertyElement<TValue>;
        }
    }
}