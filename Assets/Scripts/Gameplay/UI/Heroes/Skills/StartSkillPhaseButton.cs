using System.Linq;
using TPT.Gameplay.Heroes;

namespace TPT.Gameplay.UI.Heroes.Skills
{
    public class StartSkillPhaseButton : StartPhaseButton<HeroSkillPhase>
    {
        protected override void Init(Hero hero)
        {
            CanvasGroup.interactable = Controller.Hero.Skills.Any();
        }

        protected override HeroSkillPhase CreatePhase() =>
            new HeroSkillPhase(Controller.Hero);
    }
}