using System;
using TPT.Gameplay.Heroes;
using UnityEngine;

namespace TPT.Gameplay.UI.Heroes
{
    public class HeroTurnUI : MonoBehaviour
    {
        [SerializeField] 
        private Hero hero;

        [SerializeField] 
        private Canvas canvas;
        
        [SerializeField] 
        private HeroHealthUI healthUI;

        private void Awake()
        {
            canvas.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            hero.OnTurnStarted += OnHeroTurnStarted;
            hero.OnTurnEnded += OnHeroTurnEnded;
            
            healthUI.Bind(hero);
        }

        private void OnDisable()
        {
            hero.OnTurnStarted -= OnHeroTurnStarted;
            hero.OnTurnEnded -= OnHeroTurnEnded;
            
            healthUI.Unbind(hero);
        }

        private void OnHeroTurnStarted()
        {
            canvas.gameObject.SetActive(true);
        }

        private void OnHeroTurnEnded()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
