using TPT.Gameplay.Heroes;
using TPT.Gameplay.UI.Heroes.Panels;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes.Skills
{
    public class HeroSkillPhase : HeroTurnPhase
    {
        private static readonly int IsPreparingAttack = Animator.StringToHash("IsPreparingAttack");

        public HeroSkillPhase(Hero hero) : base(hero)
        {

        }

        protected override void ProcessStart()
        {
            hero.HeroAnimator.Animator.SetBool(IsPreparingAttack, true);
        }

        protected override void ProcessSuccess()
        {
            hero.HeroAnimator.Animator.SetBool(IsPreparingAttack, false);
        }

        protected override void ProcessFailure()
        {
            hero.HeroAnimator.Animator.SetBool(IsPreparingAttack, false);
        }
    }
}