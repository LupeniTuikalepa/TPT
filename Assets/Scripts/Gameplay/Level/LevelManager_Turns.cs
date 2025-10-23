using System.Collections.Generic;
using System.Linq;
using TPT.Gameplay.Heroes;
using UnityEngine;
using UnityEngine.Pool;

namespace TPT.Gameplay
{
    public partial class LevelManager
    {
        private readonly Dictionary<Hero, int> heroTurnPoints = new();

        private Hero GetNextHero()
        {
            using (ListPool<Hero>.Get(out var heroesToAdd))
            using (ListPool<Hero>.Get(out var keys))
            {
                keys.AddRange(heroTurnPoints.Keys);

                while (heroesToAdd.Count == 0 && nextHeroes.Count == 0)
                {
                    foreach (var hero in keys)
                    {
                        if (nextHeroes.Contains(hero))
                            continue;

                        heroTurnPoints[hero] -= hero.CurrentSpeed;
                        //Debug.Log($"{hero.name} is now at {heroTurnPoints[hero]} with {hero.CurrentSpeed} speed");
                        if (heroTurnPoints[hero] <= 0)
                            heroesToAdd.Add(hero);
                    }
                }

                heroesToAdd.Sort(CompareHeroPoints);
                nextHeroes.AddRange(heroesToAdd);
            }

            Hero next = nextHeroes[0];
            nextHeroes.RemoveAt(0);
            return next;
        }

        private int CompareHeroPoints(Hero a, Hero b)
        {
            return heroTurnPoints[a].CompareTo(heroTurnPoints[b]);
        }


        public void MoveToNextTurn()
        {
            if (CurrentHero != null)
            {
                CurrentHero.EndTurn();
                heroTurnPoints[CurrentHero] = turnCost;
                CurrentHero = GetNextHero();
            }
            else
            {
                //Picks the hero with the most speed
                CurrentHero = heroTurnPoints.Keys.Aggregate((a, b) => a.CurrentSpeed > b.CurrentSpeed ? a : b);
            }

            Debug.Log($"{CurrentHero.name} beginning turn", CurrentHero);
            CurrentHero.BeginTurn();
        }

        public int GetPointsFor(Hero hero) => heroTurnPoints.GetValueOrDefault(hero, turnCost);
    }
}