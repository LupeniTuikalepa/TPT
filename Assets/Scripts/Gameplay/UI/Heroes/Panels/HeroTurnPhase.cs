using System;
using TPT.Gameplay.Heroes;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes
{
    public abstract class HeroTurnPhase
    {
        public event Action<HeroTurnPhase> OnPhaseStarted;

        public event Action<HeroTurnPhase> OnPhaseSuccess;

        public event Action<HeroTurnPhase> OnPhaseFailure;

        public readonly Hero hero;

        protected HeroTurnPhase(Hero hero)
        {
            this.hero = hero;
        }

        public void Start()
        {
            ProcessStart();
            OnPhaseStarted?.Invoke(this);
        }


        public void Finish(bool success)
        {
            if (success)
            {
                ProcessSuccess();
                OnPhaseSuccess?.Invoke(this);
            }
            else
            {
                ProcessFailure();
                OnPhaseFailure?.Invoke(this);
            }
        }

        protected abstract void ProcessStart();
        protected abstract void ProcessSuccess();
        protected abstract void ProcessFailure();
    }
}