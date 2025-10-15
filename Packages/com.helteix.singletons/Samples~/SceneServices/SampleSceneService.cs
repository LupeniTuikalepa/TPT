using System;
using Helteix.Singletons.SceneServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Helteix.Singletons.Samples.SceneServices
{
    public class SampleSceneService : SceneService<SampleSceneService>
    {
        public static event Action<Color> SetBackgroundColor;

        public Color color;

        private void Awake()
        {
            color = Random.ColorHSV();
            color.a = 1;
        }

        public void Trigger()
        {
            SetBackgroundColor?.Invoke(color);
        }
    }
}