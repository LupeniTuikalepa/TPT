using System;
using System.Collections.Generic;
using DG.Tweening;
using TPT.Core.Data.Heroes;
using TPT.Gameplay.Heroes;
using Unity.Cinemachine;
using UnityEngine;

namespace TPT.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action<Hero> OnHeroSpawn;
        public event Action<Hero> OnHeroDespawn;

        [SerializeField]
        private Transform[] spawnPoints;

        [field: SerializeField]
        public Color PlayerColor { get; private set; }
        [field: SerializeField]
        public CinemachineTargetGroup TargetGroup { get; private set; }

        private Dictionary<HeroData, Hero>  heroes = new Dictionary<HeroData, Hero>();
        public IEnumerable<Hero> Heroes => heroes.Values;

        private Queue<Transform> availableSpawnPoints = new Queue<Transform>();


        private void Awake()
        {
            availableSpawnPoints = new Queue<Transform>(spawnPoints);
        }

        public Hero SpawnHero(HeroData data)
        {
            if (!availableSpawnPoints.TryDequeue(out Transform spawn))
                return null;

            GameObject heroGameObject = Instantiate(data.Prefab);
            Hero hero = heroGameObject.GetComponent<Hero>();

            if (heroes.Remove(data, out Hero existingHero))
            {
                existingHero.DOKill();
                Destroy(existingHero.gameObject);
            }

            hero.name = $"{name}_{data.Name}";
            hero.ProcessSpawn(data, this, spawn);
            heroes.Add(data, hero);
            TargetGroup.Targets.Add(new CinemachineTargetGroup.Target()
            {
                Object = hero.transform,
                Radius = 1,
                Weight = 1,
            });

            OnHeroSpawn?.Invoke(hero);
            return hero;
        }

        public void DespawnHero(Hero hero)
        {
            if (hero.Player != this)
            {
                Debug.LogError("Cannot despawn hero because it belongs to another player");
                return;
            }

            hero.ProcessDespawn();
            TargetGroup.Targets.RemoveAll(ctx => ctx.Object == hero.transform);

            OnHeroDespawn?.Invoke(hero);
            Destroy(hero.gameObject);
        }
    }
}