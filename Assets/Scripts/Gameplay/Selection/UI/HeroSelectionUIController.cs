using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Helteix.Tools;
using TPT.Gameplay.Heroes;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace TPT.Gameplay.Selection.UI
{
    public class HeroSelectionUIController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private CinemachineTargetGroup targetGroup;
        [SerializeField]
        private CinemachineCamera groupCamera;

        [SerializeField]
        private HeroSelectionUI prefab;
        [SerializeField]
        private Transform root;

        private Dictionary<HeroSelectionUI, Hero> createdHeroSelectionUIs;

        private HeroSelection current;

        private InputAction cancelAction;
        private InputAction validateAction;

        private void Awake()
        {
            createdHeroSelectionUIs = new Dictionary<HeroSelectionUI, Hero>();

            if (EventSystem.current.currentInputModule is InputSystemUIInputModule module)
            {
                cancelAction = module.cancel;
                validateAction = module.submit;
            }

            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void OnEnable()
        {
            HeroSelection.OnSelectionBegin += BeginSelection;
            HeroSelection.OnSelectionEnds += EndSelection;
        }


        private void OnDisable()
        {
            HeroSelection.OnSelectionBegin -= BeginSelection;
            HeroSelection.OnSelectionEnds -= EndSelection;
        }

        private void BeginSelection(HeroSelection selection)
        {
            current = selection;
            targetGroup.Targets.Clear();
            Hero contextHero = selection.Context.hero;
            groupCamera.Follow = contextHero.transform;

            root.ClearChildren();
            createdHeroSelectionUIs.Clear();

            foreach (Hero candidate in selection.Candidates)
            {;
                targetGroup.AddMember(candidate.transform, 1, candidate == contextHero ? 2 : 1);

                HeroSelectionUI instance = Instantiate(prefab, root);
                instance.Sync(candidate, this);
                createdHeroSelectionUIs.Add(instance, candidate);
            }

            if(targetGroup.Targets.All(ctx => ctx.Object != contextHero.transform))
                targetGroup.AddMember(contextHero.transform, 1, 2);

            groupCamera.gameObject.SetActive(true);
            canvasGroup.DOFade(1, .5f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            if(cancelAction != null)
                cancelAction.performed += CancelSelection;
            if (validateAction != null)
                validateAction.performed += ValidateSelection;

            current.Select(selection.Candidates[0]);
        }


        private void EndSelection(HeroSelection selection)
        {
            groupCamera.gameObject.SetActive(false);

            canvasGroup.DOFade(0, .5f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            if(cancelAction != null)
                cancelAction.performed -= CancelSelection;
            if (validateAction != null)
                validateAction.performed -= ValidateSelection;
        }

        private void ValidateSelection(InputAction.CallbackContext ctx)
        {
            if(current != null && current.Selection != null)
                ValidateSelection();
        }

        public void ValidateSelection() => current?.Validate();

        public void Select(HeroSelectionUI heroSelectionUI)
        {
            if (current == null)
                return;

            foreach ((HeroSelectionUI ui, Hero hero) in createdHeroSelectionUIs)
            {
                if (heroSelectionUI == ui)
                {
                    Debug.Log("LA");
                    current.Select(hero);
                    ui.OnSelected();
                }
                else
                    ui.OnDeselected();
            }
        }

        private void CancelSelection(InputAction.CallbackContext obj) => CancelSelection();
        public void CancelSelection() => current?.Cancel();
    }
}