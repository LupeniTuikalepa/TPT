using System;
using TMPro;
using TPT.Core.Data.Skills;
using TPT.Gameplay.Heroes.Skills;
using TPT.Gameplay.Selection;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes.Skills
{
    public class HeroSkillUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private TextMeshProUGUI description;
        [SerializeField]
        private TextMeshProUGUI manaCost;
        [SerializeField]
        private CanvasGroup canvasGroup;

        private HeroTurnSkillsPhaseUI uiController;


        public void Sync(ISkill skill, HeroTurnSkillsPhaseUI controller)
        {
            uiController = controller;
            bool canBeUsed = skill.CanBeUsed();

            canvasGroup.interactable = canBeUsed;
            canvasGroup.alpha = canBeUsed ? 1f : 0;

            title.text = skill.Title;
            description.text = skill.Description;
            manaCost.text = skill.ManaCost.ToString();
        }

        public void Select()
        {
            uiController.Select(this);
        }
    }
}