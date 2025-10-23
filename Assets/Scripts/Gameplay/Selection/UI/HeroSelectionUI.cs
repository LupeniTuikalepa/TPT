using TMPro;
using TPT.Gameplay.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace TPT.Gameplay.Selection.UI
{
    public class HeroSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI heroName;
        [SerializeField]
        private GameObject selectedOutline;

        [Space]
        [SerializeField]
        private Image healthBar;
        [SerializeField]
        private TextMeshProUGUI healthText;
        [Space]
        [SerializeField]
        private Image manaBar;
        [SerializeField]
        private TextMeshProUGUI manaText;

        private HeroSelectionUIController controller;

        public void Sync(Hero hero, HeroSelectionUIController heroSelectionUIController)
        {
            controller = heroSelectionUIController;

            heroName.text = hero.Data.Name;
            healthBar.fillAmount = (float)hero.CurrentHealth / hero.MaxHealth;
            healthText.text = $"{hero.CurrentHealth}/{hero.MaxHealth}";
            manaBar.fillAmount = (float)hero.CurrentMana / hero.MaxMana;
            manaText.text = $"{hero.CurrentMana}/{hero.MaxMana}";
        }

        public void Select()
        {
            controller.Select(this);
        }

        public void OnSelected()
        {
            selectedOutline.SetActive(true);
        }
        public void OnDeselected()
        {
            selectedOutline.SetActive(false);
        }
    }
}