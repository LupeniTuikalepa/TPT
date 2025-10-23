using System.Collections.Generic;
using TPT.Core.Data.Skills;
using TPT.Gameplay.Heroes;
using TPT.Gameplay.Level;
using TPT.Gameplay.Selection;

namespace TPT.Gameplay.UI.Heroes.Attack
{
    public class HeroAttackPhase : HeroTurnPhase, IHeroSelectionCallbackProvider
    {
        private Hero heroToAttack;

        public HeroAttackPhase(Hero hero) : base(hero)
        {

        }

        void IHeroSelectionCallbackProvider.OnComplete(List<Hero> heroes)
        {
            Finish(heroes.Count != 0);
        }

        void IHeroSelectionCallbackProvider.OnFail()
        {
            Finish(false);
        }

        protected override void ProcessStart()
        {
            HeroSelection.Begin(new HeroSelectionContext()
            {
                hero = hero,
                targetType = TargetType.One,
                targetTeam = TargetTeam.Enemies,
            }, this);
        }


        protected override void ProcessSuccess()
        {
            if(heroToAttack)
                heroToAttack.AddOrRemoveHealth(-hero.CurrentStrength);

            LevelManager.Instance.MoveToNextTurn();
        }

        protected override void ProcessFailure()
        {

        }
    }
}