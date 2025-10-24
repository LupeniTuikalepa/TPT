using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TPT.Gameplay.UI.Heroes
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class HeroTurnPhaseUI<T> : MonoBehaviour 
        where T : HeroTurnPhase
    {
        public bool IsOpen { get; private set; }

        private CanvasGroup canvasGroup;

        protected HeroTurnUIController controller;

        private void Awake()
        {
            controller = GetComponentInParent<HeroTurnUIController>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            controller.OnPhaseStarts += OnPhaseStarts;
            controller.OnPhaseEnds += OnPhaseEnds;
            controller.OnTurnBegins += OnTurnBegins;
        }


        private void OnDisable()
        {
            controller.OnTurnBegins -= OnTurnBegins;
            controller.OnPhaseStarts -= OnPhaseStarts;
            controller.OnPhaseEnds -= OnPhaseEnds;
        }

        private void OnPhaseStarts(HeroTurnPhase phase)
        {
            if (!IsOpen && phase is T t)
                Open(t);
        }

        private void OnPhaseEnds(HeroTurnPhase phase)
        {
            if (IsOpen && phase is T t)
                Close(t);
        }


        private void OnTurnBegins()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        public void Open(T phase)
        {
            IsOpen = true;
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, .5f);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;

            ISelectHandler selectHandler = GetComponentInChildren<ISelectHandler>();

            if (selectHandler is Component component)
                EventSystem.current.SetSelectedGameObject(component.gameObject);

            OnOpen(phase);
        }

        protected abstract void OnOpen(T phase);

        public void Close(T phase)
        {
            IsOpen = false;
            canvasGroup.DOKill();
            canvasGroup.DOFade(1, 0);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;

            OnClose(phase);
        }

        protected abstract void OnClose(T phase);
    }
}