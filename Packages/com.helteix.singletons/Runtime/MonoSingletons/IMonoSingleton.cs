using UnityEngine;

namespace Helteix.Singletons.MonoSingletons
{
    public interface IMonoSingleton
    {
        // ReSharper disable once InconsistentNaming
        GameObject gameObject { get; }
    }
}