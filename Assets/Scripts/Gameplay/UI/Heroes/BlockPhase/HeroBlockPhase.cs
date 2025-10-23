using TPT.Gameplay.Heroes;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes.BlockPhase
{
    public class HeroBlockPhase : HeroTurnPhase
    {
        public HeroBlockPhase(Hero hero) : base(hero)
        {

        }

        protected override void ProcessStart()
        {
            Debug.Log("Blocking");
            Finish(true);
        }

        protected override void ProcessSuccess()
        {
        }

        protected override void ProcessFailure()
        {
        }
    }
}