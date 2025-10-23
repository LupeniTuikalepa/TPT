using System;
using TPT.Core.Data.Skills;

namespace TPT.Gameplay.Heroes.Skills
{
    public abstract class Skill<T> : ISkill where T : SkillData
    {
        public event Action<Hero> OnUsed;

        public string Title => Data.Title;
        public string Description => Data.Description;

        public TargetTeam TargetTeam => Data.TargetTeam;

        public TargetType TargetType => Data.TargetType;

        public int ManaCost => Data.ManaCost;

        public T Data { get; private set; }

        public void Initialize(T data)
        {
            Data = data;
            Initialize();
        }

        public abstract bool CanBeUsed();
        protected abstract void Initialize();

        public void Use(SkillUsageContext context)
        {
            context.from.AddOrRemoveMana(Data.ManaCost);
            OnUse(context);

            OnUsed?.Invoke(context.from);
        }
        protected abstract void OnUse(SkillUsageContext context);
    }
}