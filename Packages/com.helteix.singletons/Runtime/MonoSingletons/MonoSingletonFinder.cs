using Helteix.Singletons.Interfaces;
using UnityEngine;

namespace Helteix.Singletons.MonoSingletons
{
    public class MonoSingletonFinder<T> : ISingletonFinder<T> where T : MonoBehaviour, ISingleton
    {
        public bool TryFindExistingInstance(out T instance)
        {
            instance = GameObject.FindFirstObjectByType<T>();
            return instance != null;
        }
    }
}