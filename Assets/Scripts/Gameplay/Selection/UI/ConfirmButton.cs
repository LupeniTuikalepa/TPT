using System;
using TPT.Gameplay.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace TPT.Gameplay.Selection.UI
{
    public class ConfirmButton : MonoBehaviour
    {
        private HeroSelectionUIController controller;

        [SerializeField]
        private Button button;

        private void Awake()
        {
            controller = GetComponentInParent<HeroSelectionUIController>();
        }

        private void OnEnable()
        {
            HeroSelection.OnSelectionBegin += Bind;
            HeroSelection.OnSelectionEnds += Unbind;
        }


        private void OnDisable()
        {
            HeroSelection.OnSelectionBegin -= Bind;
            HeroSelection.OnSelectionEnds -= Unbind;

        }

        private void Bind(HeroSelection selection)
        {
            selection.OnHeroSelected += CheckValidity;
        }


        private void Unbind(HeroSelection selection)
        {
            selection.OnHeroSelected -= CheckValidity;
        }

        private void CheckValidity(Hero obj)
        {
            button.interactable = obj != null;
        }

        public void Confirm()
        {
            controller.ValidateSelection();
        }
    }
}