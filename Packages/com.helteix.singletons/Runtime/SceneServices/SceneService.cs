using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Helteix.Singletons.SceneServices
{
    public class SceneService<T> : MonoBehaviour, ISceneService where T : SceneService<T>
    {
        private static readonly Dictionary<Scene, T> Services = new();

        static SceneService()
        {
            Debug.Log("constructor");
        }

        public static bool TryGetFor(Scene scene, out T service)
        {
            if (Services.TryGetValue(scene, out service))
            {
                return true;
            }

            if (SingletonSettings.Current.TryGetPrefabFor<T>(out GameObject prefab))
            {
                GameObject instance = Instantiate(prefab, scene) as GameObject;
                if (instance == null)
                    return false;

                if(!instance.TryGetComponent(out service))
                    service = instance.AddComponent<T>();

                return true;
            }
            return false;
        }

        public static T GetFor(Scene scene)
        {
            if(TryGetFor(scene, out T service))
                return service;

            return null;
        }

        public static bool TryGetFor(GameObject gameObject, out T service) => TryGetFor(gameObject.scene, out service);
        public static T GetFor(GameObject gameObject) => GetFor(gameObject.scene);

        public static  bool TryGetFor(Component component, out T service) => TryGetFor(component.gameObject, out service);

        public static T GetFor(Component component) => GetFor(component.gameObject);

        public static  bool TryGetForActiveScene(out T service) => TryGetFor(SceneManager.GetActiveScene(), out service);
        public static T GetForActiveScene() => GetFor(SceneManager.GetActiveScene());

        public static bool TryGetFirst(out T service)
        {
            service = null;
            if (Services.Count == 0)
                return false;

            foreach ((_, T sceneService) in Services)
            {
                if(sceneService != null)
                    return sceneService;
            }
            if (SingletonSettings.Current.TryGetPrefabFor<T>(out GameObject prefab))
            {
                GameObject instance = Instantiate(prefab);
                if (instance == null)
                    return false;

                if(!instance.TryGetComponent(out service))
                    service = instance.AddComponent<T>();

                return true;
            }
            return false;
        }

        public static T GetFirst() => TryGetFirst(out T service) ? service : null;

        public static T Instance => SingletonSettings.Current.ServiceInstanceBehaviour switch
        {
            SingletonSettings.ServiceInstanceBehaviourChoice.PickActiveSceneInstance => GetForActiveScene(),
            SingletonSettings.ServiceInstanceBehaviourChoice.PickFirstInstance => GetFirst(),
            _ => null
        };

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }


        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Services.TryAdd(scene, null);
        }

        private static void OnSceneUnloaded(Scene scene)
        {
            if (Services.Remove(scene, out T service))
                service.DisposeService(scene);
        }

        private static void OnActiveSceneChanged(Scene scene1, Scene scene2)
        {

        }

        private void OnEnable()
        {
            Register(this);
        }

        private void OnDisable()
        {
            Unregister(this);
        }

        private void Register(SceneService<T> sceneService)
        {
            Scene scene = sceneService.gameObject.scene;
            if (Services.TryGetValue(scene, out T service))
                service.Deactivate();

            if (sceneService is not T t)
                return;

            Services[scene] = t;
            t.Activate();
            Debug.Log($"Registering {sceneService.name} for scene {scene.name}");
        }

        private void Unregister(SceneService<T> sceneService)
        {
            Scene scene = sceneService.gameObject.scene;
            if (Services.TryGetValue(scene, out T service))
            {
                Debug.Log($"Unregistering {sceneService.name} for scene {scene.name}");
                service.Deactivate();
            }
        }


        protected virtual void Deactivate() { }
        protected virtual void Activate() { }

        protected virtual void InitializeService(Scene scene) { }
        protected virtual void DisposeService(Scene scene) { }


        public bool MoveToScene(Scene to)
        {
            if (Services.ContainsKey(to))
            {
                Debug.LogError($"[SceneService] Cannot move to scene {to.name} because it already contains a service of type {typeof(T).Name}");
                return false;
            }

            Unregister(this);
            SceneManager.MoveGameObjectToScene(gameObject, to);
            Register(this);
            return true;
        }
    }
}