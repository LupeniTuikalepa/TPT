using TPT.Gameplay.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace TPT.Gameplay.UI.Heroes
{
    public class HeroHealthUI : MonoBehaviour
    {
        [SerializeField]
        private Image fill;
        
        public void Bind(Hero hero)
        {
            hero.OnHealthChanged += OnHeroHealthChanged;
        }
        
        public void Unbind(Hero hero)
        {
            hero.OnHealthChanged -= OnHeroHealthChanged;
        }
        
        private void OnHeroHealthChanged(int lastHealth, Hero hero)
        {
            float t = hero.CurrentHealth / (float)hero.MaxHealth;
            Debug.Log($"{hero.CurrentHealth} : {hero.MaxHealth}");
            fill.fillAmount = t;
        }


    }
}
