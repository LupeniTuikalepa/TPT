namespace TPT.Gameplay.UI.Heroes.Attack
{
    public class StartAttackPhaseButton : StartPhaseButton<HeroAttackPhase>
    {
        protected override HeroAttackPhase CreatePhase() =>
            new HeroAttackPhase(Controller.Hero);
    }
}