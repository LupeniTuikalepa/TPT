using UnityEngine;

namespace Helteix.Singletons.PrefabMapping
{
    public interface IAssetReference
    {
        bool IsValid();
        GameObject LoadSynchronously();
    }
}