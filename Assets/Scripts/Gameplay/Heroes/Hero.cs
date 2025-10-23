using System;
using System.Collections.Generic;
using DG.Tweening;
using TPT.Core.Data.Heroes;
using TPT.Core.Data.Skills;
using TPT.Gameplay.Heroes.Animations;
using TPT.Gameplay.Heroes.Skills;
using TPT.Gameplay.Level;
using TPT.Gameplay.Player;
using UnityEngine;

namespace TPT.Gameplay.Heroes
{
    public partial class Hero : MonoBehaviour
    {
        #region Events
        public event Action OnTurnStarted;
        public event Action OnTurnEnded;
        public event Action OnHeroDied;
        public event Action<Hero> OnHeroSpawn;
        public event Action<Hero> OnHeroDespawn;
        public event Action<ISkill> OnSkillAdded;

        public event Action<ISkill> OnSkillRemoved;
        public event Action<int, Hero> OnHealthChanged;

        #endregion

        public int MaxHealth => Data.MaxHealth;
        public int MaxMana => Data.MaxMana;
        public HeroData Data { get; private set; }
        public int CurrentHealth { get; private set; }
        public int CurrentMana { get; private set; }
        public int CurrentSpeed { get; private set; }

        public int CurrentStrength { get; private set; }
        public int CurrentTurnPoints => LevelManager.Instance.GetPointsFor(this);

        public bool IsPlaying => LevelManager.Instance.CurrentHero == this;
        public bool IsAlive => CurrentHealth != 0;

        public Transform SpawnPoint { get; private set; }
        public PlayerController Player { get; private set; }
        public IEnumerable<ISkill> Skills => skills;

        private List<ISkill> skills = new();

        [field: SerializeField]
        public HeroAnimatorController HeroAnimator { get; private set; }


        public void ProcessSpawn(HeroData data, PlayerController player, Transform spawn)
        {
            Data = data;
            Player = player;
            SpawnPoint = spawn;

            CurrentHealth = data.MaxHealth;
            CurrentMana = data.MaxMana;
            CurrentSpeed = data.Speed;

            transform.SetParent(spawn);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            skills ??= new List<ISkill>(data.SkillsData.Length);
            skills.Clear();

            for (int i = 0; i < data.SkillsData.Length; i++)
            {
                SkillData skillData = data.SkillsData[i];
                if (skillData.TryCreateGetSkillForData(out ISkill skill))
                    AddSkill(skill);
            }

            OnHeroSpawn?.Invoke(this);
        }

        private void AddSkill(ISkill skill)
        {
            if(skills.Contains(skill))
                return;

            skills.Add(skill);
            OnSkillAdded?.Invoke(skill);
        }

        private bool RemoveSkill(ISkill skill)
        {
            if (skills.Remove(skill))
            {
                OnSkillRemoved?.Invoke(skill);
                return true;
            }

            return false;
        }

        public void ProcessDespawn()
        {
            Player = null;
            Data = null;

            OnHeroDespawn?.Invoke(this);
        }

        public void BeginTurn()
        {
            OnTurnStarted?.Invoke();
        }

        public void EndTurn()
        {
            OnTurnEnded?.Invoke();
        }

        public void AddOrRemoveHealth(int health)
        {
            //La vie avant de prendre le coup
            int lastHealth = CurrentHealth;

            CurrentHealth += health;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            if (CurrentHealth < 0)
                CurrentHealth = 0;

            OnHealthChanged?.Invoke(lastHealth, this);

            if (CurrentHealth == 0 && lastHealth != 0)
            {
                Die();
            }
        }

        public void Die()
        {
            CurrentHealth = 0;

            OnHeroDied?.Invoke();
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