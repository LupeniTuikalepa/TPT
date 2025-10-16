using System.Collections.Generic;
using TPT.Gameplay.Heroes;
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
                //Debug.Log("HA");
            }

            CurrentHero = GetNextHero();
            CurrentHero.BeginTurn();
            /*
            foreach ((Hero hero, int points) in heroTurnPoints)
            {
                Debug.Log($"{hero.name} has {points} points");
            }
            */
        }

        public int GetPointsFor(Hero hero) => heroTurnPoints.GetValueOrDefault(hero, turnCost);
    }
}