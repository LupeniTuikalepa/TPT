using TPT.Core.Data.Skills;

namespace TPT.Gameplay.Heroes.Skills
{
    public interface ISkill
    {
        string Title { get; }

        string Description { get; }

        int ManaCost { get; }

        TargetTeam TargetTeam { get; }

        TargetType TargetType { get; }

        bool CanBeUsed();
        void Use(SkillUsageContext context);
    }
}