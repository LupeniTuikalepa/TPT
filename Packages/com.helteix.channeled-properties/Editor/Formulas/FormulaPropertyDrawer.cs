using System;
using Helteix.ChanneledProperties.Formulas;
using UnityEditor;

namespace Helteix.ChanneledProperties.Editor.Formulas
{
    [CustomPropertyDrawer(typeof(Formula<>))]
    public class FormulaPropertyDrawer : ChanneledPropertyDrawer
    {
        protected override ChanneledPropertyElement<TValue> GetElement<TValue>(SerializedProperty property, IChanneledPropertyEditor<TValue> channeledProperty)
        {
            Type t = typeof(FormulaPropertyElement<>).MakeGenericType(typeof(TValue));
            object a = Activator.CreateInstance(t, channeledProperty, property);
            
            return  a as ChanneledPropertyElement<TValue>;
        }
    }
}