using System;
using System.Collections.Generic;
using TPT.Core.Data.Heroes;
using UnityEngine;

namespace TPT.Gameplay
{
    public class LevelDebug : MonoBehaviour
    {
        [SerializeField]
        private List<HeroData> player1Heroes;
        [SerializeField]
        private List<HeroData> player2Heroes;

        private void Start()
        {
            LevelInfos infos = new LevelInfos()
            {
                firstPlayerHeroes = player1Heroes,
                secondPlayerHeroes = player2Heroes
            };

            LevelManager.Instance.StartGame(infos);
        }
    }
}