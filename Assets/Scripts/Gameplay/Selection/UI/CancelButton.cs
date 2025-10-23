using System;
using TPT.Gameplay.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace TPT.Gameplay.Selection.UI
{
    public class CancelButton : MonoBehaviour
    {
        private HeroSelectionUIController controller;

        private void Awake()
        {
            controller = GetComponentInParent<HeroSelectionUIController>();
        }


        public void Cancel()
        {
            controller.CancelSelection();
        }
    }
}