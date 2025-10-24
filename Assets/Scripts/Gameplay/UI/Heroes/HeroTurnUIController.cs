using System;
using DG.Tweening;
using TPT.Gameplay.Heroes;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes
{
    public class HeroTurnUIController : MonoBehaviour
    {
        public event Action<HeroTurnPhase> OnPhaseStarts;
        public event Action<HeroTurnPhase> OnPhaseEnds;
        public event Action OnTurnBegins;
        public event Action OnTurnEnds;

        [SerializeField]
        private Hero hero;

        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private CanvasGroup mainPanelGroup;

        public HeroTurnPhase CurrentPhase { get; private set; }

        public Hero Hero => hero;

        private void Awake()
        {
            canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            hero.OnTurnStarted += Activate;
            hero.OnTurnEnded += Deactivate;
        }

        private void OnDisable()
        {
            hero.OnTurnStarted -= Activate;
            hero.OnTurnEnded -= Deactivate;
        }

        private void Activate()
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, .3f);

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            OnTurnBegins?.Invoke();
            mainPanelGroup.DOFade(1, .3f);
            mainPanelGroup.interactable = true;
            mainPanelGroup.blocksRaycasts = true;
        }

        private void Deactivate()
        {
            canvasGroup.DOKill();
            canvasGroup.DOFade(0, .3f)
                .OnComplete(() => canvasGroup.gameObject.SetActive(true));
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            OnTurnEnds?.Invoke();
        }

        public void StartPhase<T>(T phase) where T : HeroTurnPhase
        {
            if(CurrentPhase != null)
                StopCurrentPhase(false);

            mainPanelGroup.DOKill();
            mainPanelGroup.DOFade(0, .3f);
            mainPanelGroup.interactable = false;
            mainPanelGroup.blocksRaycasts = false;

            CurrentPhase = phase;
            phase.Start();
            OnPhaseStarts?.Invoke(phase);
        }

        public void StopCurrentPhase(bool success)
        {
            if(CurrentPhase == null)
                return;

            CurrentPhase.Finish(success);
            mainPanelGroup.DOKill();

            mainPanelGroup.alpha = 1;
            mainPanelGroup.interactable = true;
            mainPanelGroup.blocksRaycasts = true;

            OnPhaseEnds?.Invoke(CurrentPhase);
        }
    }
}