using Helteix.Singletons.Interfaces;
using Helteix.Tools;
using UnityEngine;


namespace Helteix.Singletons.MonoSingletons
{
    [DefaultExecutionOrder(-20)]
    public abstract class MonoSingleton<T, TFactory, TFinder> : MonoBehaviour, ISingleton, IMonoSingleton
        where T : MonoSingleton<T, TFactory, TFinder>, ISingleton
        where TFactory : ISingletonFactory<T>, new()
        where TFinder : ISingletonFinder<T>, new()
    {

        public bool IsInstance => HasInstance && Holder.IsInstance;
        public static bool HasInstance => Holder.InternalHasInstance;
        public static T Instance
        {
            get
            {
                if (!Application.isPlaying)
                {
                    Debug.LogError("Cannot create an instance in Editor Mode");
                    return null;
                }

                return Holder.InternalInstance;
            }
        }

        private static MonoSingletonHolder<T, TFactory, TFinder> holder;
        private static MonoSingletonHolder<T, TFactory, TFinder> Holder => holder ??= new();
        internal MonoSingletonHolder<T, TFactory, TFinder> InstanceHolder => Holder;

        private void Awake()
        {
            if (Instance != this)
            {
                Holder.Dispose();
                OnExistingInstanceFound(Instance);
            }

            DontDestroyOnLoad(gameObject);
            OnAwake();
        }

        protected virtual void OnAwake() { }
        private void Reset()
        {
            if (!Application.isPlaying)
            {
                Debug.LogWarning(
                    @"MonoSingleton should not be added in editor. 
They should instead be dynamically created on the go during runtime. 
If you want to configure as such your singletons consider using SceneServices instead.");
            }
        }

        protected virtual void OnExistingInstanceFound(T existingInstance)
        {

        }

        protected virtual void OnDestroy()
        {
            if (IsInstance)
            {
                holder?.Dispose();
                holder = null;
            }
        }
    }
    [DefaultExecutionOrder(-20)]
    public abstract class MonoSingleton<T, TFactory> : MonoSingleton<T,TFactory, MonoSingletonFinder<T>>
        where T : MonoSingleton<T,TFactory, MonoSingletonFinder<T>>, ISingleton
        where TFactory : ISingletonFactory<T>, new()
    {

    }
    [DefaultExecutionOrder(-20)]
    public abstract class MonoSingleton<T> : MonoSingleton<T,MonoSingletonFactory<T>, MonoSingletonFinder<T>>, ISingleton
        where T :MonoSingleton<T,MonoSingletonFactory<T>, MonoSingletonFinder<T>>, ISingleton
    {

    }
}