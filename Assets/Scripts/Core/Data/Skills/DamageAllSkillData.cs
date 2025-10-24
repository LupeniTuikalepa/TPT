using UnityEngine;

namespace TPT.Core.Data.Skills
{
    [CreateAssetMenu(menuName = "TPT/Skills/DamageAll")]
    public class DamageAllSkillData : SkillData
    {
        [field: SerializeField]
        public int Damage { get; private set; }
    }
}