using System.Collections.Generic;
using DG.Tweening;
using TPT.Data.Heroes;
using UnityEngine;

namespace TPT.Gameplay.Heroes
{
    public class Hero : MonoBehaviour
    {
        public int MaxHealth => HeroData.MaxHealth;
        public int MaxMana => HeroData.MaxMana;

        public HeroData HeroData { get; private set; }
        public int CurrentHealth { get; private set; }
        public int CurrentMana { get; private set; }
        public int CurrentSpeed { get; private set; }

        public int CurrentStrength { get; private set; }
        public int CurrentTurnPoints => LevelManager.Instance.GetPointsFor(this);

        public bool IsPlaying => LevelManager.Instance.CurrentHero == this;

        public void Initialize(HeroData data)
        {
            HeroData = data;
            CurrentHealth = data.MaxHealth;
            CurrentMana = data.MaxMana;
            CurrentSpeed = data.Speed;
        }

        public void BeginTurn()
        {
            transform.DOPunchScale(Vector3.one * .15f, .3f);
            transform.DOPunchPosition(Vector3.up * .2f, .5f);
        }

        public void EndTurn()
        {

        }

        public void AddOrRemoveHealth(int health)
        {
            CurrentHealth += health;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void AddOrRemoveMana(int mana)
        {
            CurrentMana += mana;
            if (CurrentMana > MaxMana)
                CurrentMana = MaxMana;
            if (CurrentMana < 0)
                CurrentMana = 0;
        }


        public int GetSpeedForTurn(uint turn) => CurrentSpeed;
    }
}