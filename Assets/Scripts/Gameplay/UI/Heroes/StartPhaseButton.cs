using System;
using TPT.Gameplay.Heroes;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes
{
    public abstract class StartPhaseButton<T> : MonoBehaviour where T : HeroTurnPhase
    {
        protected HeroTurnUIController Controller { get; private set; }
        protected CanvasGroup CanvasGroup { get; private set; }

        private void Awake()
        {
            Controller = GetComponentInParent<HeroTurnUIController>();
            CanvasGroup = GetComponent<CanvasGroup>();

            Controller.OnTurnBegins += () => Init(Controller.Hero);
        }


        public void StartPhase()
        {
            T phase = CreatePhase();
            Controller.StartPhase(phase);
        }

        protected virtual void Init(Hero hero)
        {

        }
        protected abstract T CreatePhase();
    }
}