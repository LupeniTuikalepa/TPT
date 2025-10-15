using System.Collections.Generic;
using TPT.Data.Heroes;
using TPT.Gameplay.Heroes;
using UnityEngine;

namespace TPT.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private List<HeroData> heroesData;
        [SerializeField]
        private Transform[] spawnPoints;

        [field: SerializeField]
        public Color PlayerColor { get; private set; }

        private Dictionary<HeroData, Hero>  heroes = new Dictionary<HeroData, Hero>();

        public IEnumerable<Hero> Heroes => heroes.Values;

        public void Initialize()
        {
            int index = 0;
            foreach (var data in heroesData)
            {
                GameObject heroGameObject = Instantiate(data.Prefab);
                Hero hero = heroGameObject.GetComponent<Hero>();

                if (heroes.Remove(data, out Hero existingHero))
                {
                    Destroy(existingHero.gameObject);
                }

                hero.name = $"{name}_{data.Name}";
                hero.transform.SetParent(transform);

                hero.Initialize(data);
                heroes.Add(data, hero);
                if (spawnPoints.Length > index)
                {
                    hero.transform.position = spawnPoints[index].position;
                    index++;
                }
            }
        }
    }
}