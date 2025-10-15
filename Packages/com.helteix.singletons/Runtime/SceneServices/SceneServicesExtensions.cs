using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helteix.Singletons.SceneServices
{
    public static class SceneServicesExtensions
    {
        public static T GetService<T>(this Scene scene) where T : SceneService<T> =>
            SceneService<T>.GetFor(scene);
        public static bool TryGetService<T>(this Scene scene, out T sceneService) where T : SceneService<T> =>
            SceneService<T>.TryGetFor(scene, out sceneService);
        public static T GetService<T>(this GameObject gameObject) where T : SceneService<T> =>
            GetService<T>(gameObject.scene);

        public static bool TryGetService<T>(this GameObject gameObject, out T sceneService) where T : SceneService<T> =>
            TryGetService(gameObject.scene, out sceneService);
        public static T GetService<T>(this Component component) where T : SceneService<T> =>
            GetService<T>(component.gameObject);

        public static bool TryGetService<T>(this Component component, out T sceneService) where T : SceneService<T> =>
            TryGetService(component.gameObject, out sceneService);

    }
}