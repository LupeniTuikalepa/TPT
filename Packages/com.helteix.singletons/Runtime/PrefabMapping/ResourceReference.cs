using UnityEngine;

namespace Helteix.Singletons.PrefabMapping
{
    [System.Serializable]
    public class ResourceReference : IAssetReference
    {
        [SerializeField]
        private string path;

        public bool IsValid() => !string.IsNullOrEmpty(path);
        public GameObject LoadSynchronously() => Resources.Load<GameObject>(path);
    }
}