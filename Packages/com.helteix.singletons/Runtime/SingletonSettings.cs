using System;
using System.Collections.Generic;
using Helteix.Singletons.PrefabMapping;
using Helteix.Tools.Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helteix.Singletons
{
    [GameSettingsTitle("Singletons", "#a0d6e9")]
    [AutoGenerateGameSettings]
    public class SingletonSettings : GameSettings<SingletonSettings>
    {
        public enum ServiceInstanceBehaviourChoice
        {
            [Tooltip("Looks inside the active scene to find the main instance")]
            PickActiveSceneInstance,
            [Tooltip("Looks for the first existing instance")]
            PickFirstInstance,
        }

        [field: Header("Editor")]
        [field: SerializeField, Range(1, 20)]
        [field: Tooltip("How many times the window will refresh per seconds")]
        public int WindowRefreshRate { get; private set; } = 2;

        [field: Header("Services")]
        [field: SerializeField]
        public ServiceInstanceBehaviourChoice ServiceInstanceBehaviour { get; private set; } =
            ServiceInstanceBehaviourChoice.PickFirstInstance;


        [field: Header("Mono Singletons")]
        [field: SerializeField]
        public SingletonPrefabReference[] PrefabReferences { get; private set; }

        private readonly Dictionary<Type, GameObject> prefabs = new();

        protected override void OnLoaded()
        {
            SingletonPrefabReference[] prefabReferences = PrefabReferences;

            for (int i = 0; i < prefabReferences.Length; i++)
            {
                var reference = prefabReferences[i];
                if (reference.IsValid() && !prefabs.ContainsKey(reference.SingletonType))
                {
                    //Debug.Log($"Adding prefab for {reference.SingletonType.Name}");
                    prefabs.Add(reference.SingletonType, reference.Load());
                }
            }
        }


        public void AddPrefabFor<T>(GameObject prefab) => prefabs[typeof(T)] = prefab;

        internal bool TryGetPrefabFor<T>(out GameObject gameObject) => prefabs.TryGetValue(typeof(T), out gameObject);
    }
}