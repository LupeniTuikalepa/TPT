using System.Threading.Tasks;
using UnityEngine;

namespace Helteix.Singletons.PrefabMapping
{
    [System.Serializable]
    public class PrefabReference : IAssetReference
    {
        [SerializeField]
        private GameObject prefab;

        public bool IsValid() => prefab != null;


        public GameObject LoadSynchronously() => prefab;
    }
}