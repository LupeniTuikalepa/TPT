using UnityEditor;

namespace Helteix.ChanneledProperties.Editor.Formulas
{
    public class FormulaPropertyElement<T> : ChanneledPropertyElement<T> where T : unmanaged
    {

        public FormulaPropertyElement(IChanneledPropertyEditor<T> channeledProperty, SerializedProperty property) : base(channeledProperty, property)
        {
        }


        protected override ChannelElement<T>  CreateChannelElement() => new FormulaChannelElement<T>();
    }
}