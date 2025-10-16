using System;
using System.Collections.Generic;
using TPT.Gameplay.Heroes;
using TPT.Gameplay.Player;
using UnityEngine;
using UnityEngine.InputSystem;


namespace TPT.Gameplay
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


        private void Start()
        {
            if(Instance != this)
                return;

            Player1.Initialize();
            Player2.Initialize();

            //TODO Add automatic registration and removal
            foreach (Hero hero in Player1.Heroes)
                heroTurnPoints.Add(hero, turnCost);

            foreach (Hero hero in Player2.Heroes)
                heroTurnPoints.Add(hero, turnCost);
            
            MoveToNextTurn();
        }

        private void Update()
        {
            
            if (Keyboard.current[Key.Space].wasPressedThisFrame)
            {
                MoveToNextTurn();
                //Debug.Log($"Current Hero: {CurrentHero}");
            }
            
            if (Keyboard.current[Key.UpArrow].wasPressedThisFrame)
            {
                CurrentHero.AddOrRemoveHealth(10);
            }
            
            if (Keyboard.current[Key.DownArrow].wasPressedThisFrame)
            {
                CurrentHero.AddOrRemoveHealth(-10);
            }

        }
    }
}