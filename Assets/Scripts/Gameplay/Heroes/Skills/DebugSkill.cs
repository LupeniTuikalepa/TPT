using TPT.Core.Data.Skills;
using UnityEngine;

namespace TPT.Gameplay.Heroes.Skills
{
    [CreateSkillFor(typeof(DebugSkillData))]
    public sealed class DebugSkill : Skill<DebugSkillData>
    {
        public override bool CanBeUsed() => true;

        protected override void Initialize() { }

        protected override void OnUse(SkillUsageContext context)
        {
            Debug.Log(Data.LogMessage);
        }
    }
}