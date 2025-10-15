using Helteix.Singletons.Interfaces;
using UnityEngine;

namespace Helteix.Singletons.MonoSingletons
{
    public class MonoSingletonFactory<T> : ISingletonFactory<T> where T : MonoBehaviour, ISingleton
    {
        public virtual T CreateSingleton()
        {
            string fullName = typeof(T).ToString();
            var nameArray = fullName.Split('.');
            string name = $"{nameArray[^1]} Instance";

            GameObject singletonObject;
            if (SingletonSettings.Current.TryGetPrefabFor<T>(out GameObject prefab))
            {
                singletonObject = Object.Instantiate(prefab);
                singletonObject.name = name;
                Debug.Log($"{name} was created using a prefab reference ({prefab.name}) because none was found",
                    singletonObject);
            }
            else
            {
                singletonObject = new GameObject(name);
                Debug.Log($"{name} was created because none was found", singletonObject);
            }

            var instance = singletonObject.AddComponent<T>();
            return instance;
        }
    }
}