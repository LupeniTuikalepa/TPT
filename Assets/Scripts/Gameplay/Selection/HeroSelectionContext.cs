using TPT.Core.Data.Skills;
using TPT.Gameplay.Heroes;

namespace TPT.Gameplay.Selection
{
    public struct HeroSelectionContext
    {
        public Hero hero;
        public TargetTeam targetTeam;
        public TargetType targetType;
    }
}