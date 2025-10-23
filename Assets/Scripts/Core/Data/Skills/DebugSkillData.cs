using UnityEngine;

namespace TPT.Core.Data.Skills
{
    [CreateAssetMenu(menuName = "TPT/Skills/Debug")]
    public class DebugSkillData : SkillData
    {
        [field: SerializeField, TextArea]
        public string LogMessage { get; private set; }
    }
}