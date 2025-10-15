using UnityEditor;

namespace Helteix.ChanneledProperties.Editor.Priorities
{
    public class PriorityPropertyElement<T> : ChanneledPropertyElement<T>
    {
        public PriorityPropertyElement(IChanneledPropertyEditor<T> channeledProperty, SerializedProperty property)
            : base(channeledProperty, property)
        {

        }

        protected override ChannelElement<T>  CreateChannelElement() => new PriorityChannelElement<T>();
    }
}