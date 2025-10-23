

namespace TPT.Gameplay.UI.Heroes.BlockPhase
{
    public class StartBlockPhaseButton : StartPhaseButton<HeroBlockPhase>
    {
        protected override HeroBlockPhase CreatePhase() =>
            new HeroBlockPhase(Controller.Hero);
    }
}