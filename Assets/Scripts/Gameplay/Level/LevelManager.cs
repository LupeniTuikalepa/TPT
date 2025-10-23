using System.Collections.Generic;
using TPT.Gameplay.Heroes;
using TPT.Gameplay.Player;
using UnityEngine;

namespace TPT.Gameplay.Level
{
    public partial class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        [field: SerializeField]
        public PlayerController Player1 { get; private set; }

        [field: SerializeField]
        public PlayerController Player2 { get; private set; }

        [SerializeField, Range(1, 10)]
        private int turnsPreviewCount = 6;

        [SerializeField, Range(0, 3000)]
        private int turnCost = 1000;

        public Hero CurrentHero { get; private set; }

        private List<Hero> nextHeroes = new List<Hero>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            Player1.OnHeroSpawn += OnHeroSpawn;
            Player1.OnHeroDespawn += OnHeroDespawn;
            Player2.OnHeroSpawn += OnHeroSpawn;
            Player2.OnHeroDespawn += OnHeroDespawn;
        }

        private void OnDisable()
        {
            Player1.OnHeroSpawn -= OnHeroSpawn;
            Player1.OnHeroDespawn -= OnHeroDespawn;
            Player2.OnHeroSpawn -= OnHeroSpawn;
            Player2.OnHeroDespawn -= OnHeroDespawn;
        }

        private void Start()
        {
            if(Instance != this)
                return;
        }

        public void StartGame(LevelInfos levelInfos)
        {
            foreach (var hero in levelInfos.firstPlayerHeroes)
                Player1.SpawnHero(hero);
            foreach (var hero in levelInfos.secondPlayerHeroes)
                Player2.SpawnHero(hero);

            MoveToNextTurn();
        }

        public PlayerController GetOtherPlayer(PlayerController player) => player == Player1 ? Player2 : Player1;

        private void OnHeroSpawn(Hero hero)
        {
            heroTurnPoints.Add(hero, turnCost);
        }
        private void OnHeroDespawn(Hero hero)
        {
            heroTurnPoints.Remove(hero);
        }
    }
}