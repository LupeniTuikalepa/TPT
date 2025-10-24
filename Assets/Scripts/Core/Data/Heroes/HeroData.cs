using TPT.Core.Data.Skills;
using UnityEngine;

namespace TPT.Core.Data.Heroes
{
    [CreateAssetMenu(fileName = "New HeroData", menuName = "TPT/Hero", order = 0)]
    public class HeroData : ScriptableObject
    {
        [field : SerializeField]
        public string Name { get; private set; }
        

        [field : SerializeField, TextArea]
        public string Description { get; private set; }

        [field : SerializeField]
        public int MaxHealth { get; private set; }

        [field: SerializeField]
        public int MaxMana { get; private set; }

        [field: SerializeField]
        public int Speed { get; private set; }

        [field: SerializeField]
        public int Strength { get; private set; }

        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public SkillData[] SkillsData { get; private set; }
    }
}