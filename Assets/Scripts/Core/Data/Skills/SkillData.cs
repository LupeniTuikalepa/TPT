using UnityEngine;

namespace TPT.Core.Data.Skills
{
    public abstract class SkillData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set;}

        [field: SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]
        public int ManaCost { get; private set; }

        [field: SerializeField]
        public TargetTeam TargetTeam { get; private set; }
        [field: SerializeField]
        public TargetType TargetType { get; private set; }

    }
}