using UnityEditor;

namespace Helteix.ChanneledProperties.Editor.Conditions
{
    public class ConditionPropertyElement : ChanneledPropertyElement<bool>
    {

        public ConditionPropertyElement(IChanneledPropertyEditor<bool> channeledProperty, SerializedProperty property) : base(channeledProperty, property)
        {

        }

        protected override ChannelElement<bool>  CreateChannelElement() => new ConditionChannelElement();
    }
}