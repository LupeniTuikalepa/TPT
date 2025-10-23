using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TPT.Gameplay.UI.Heroes
{
    public class ReturnToMainPanelButton : MonoBehaviour
    {
        private HeroTurnUIController controller;

        private void Awake()
        {
            controller = GetComponentInParent<HeroTurnUIController>();

        }

        public void OnButtonClick()
        {
            controller.StopCurrentPhase(false);
        }
    }
}