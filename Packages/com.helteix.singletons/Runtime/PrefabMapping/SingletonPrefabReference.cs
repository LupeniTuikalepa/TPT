using System;
using Helteix.Singletons.MonoSingletons;
using Helteix.Tools.TypeMapping;
using UnityEngine;

namespace Helteix.Singletons.PrefabMapping
{
    [Serializable]
    public struct SingletonPrefabReference : ISerializationCallbackReceiver
    {
        [SerializeField/*, TypeRefOf(typeof(MonoSingleton<,,>))*/]
        private TypeRef singletonType;

        [SerializeField]
        private PrefabReferenceType referenceType;

        [SerializeReference]
        private IAssetReference assetRef;

        public Type SingletonType => singletonType.Type;
        public bool IsValid()
        {
            if(!singletonType.IsValid)
                return false;

            return assetRef.IsValid();
        }

        public GameObject Load()
        {
            return assetRef.LoadSynchronously();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            switch (referenceType)
            {
                case PrefabReferenceType.Prefab:
                    if (assetRef is PrefabReference)
                        return;

                    assetRef = new PrefabReference();
                    break;
                case PrefabReferenceType.Resource:
                    if (assetRef is ResourceReference)
                        return;
                    assetRef = new ResourceReference();
                    break;
            }
        }
    }
}