using System;

namespace TPT.Gameplay.Heroes.Skills
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CreateSkillForAttribute : Attribute
    {
        public readonly Type dataType;

        public CreateSkillForAttribute(Type dataType)
        {
            this.dataType = dataType;
        }
    }
}