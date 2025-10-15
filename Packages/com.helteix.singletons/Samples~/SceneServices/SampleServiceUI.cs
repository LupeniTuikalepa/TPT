using System;
using Helteix.Singletons.SceneServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Helteix.Singletons.Samples.SceneServices
{
    public class SampleServiceUI : MonoBehaviour
    {
        public Scene associatedScene;

        [SerializeField]
        private Image image;


        public void Sync() => image.color = associatedScene.GetService<SampleSceneService>().color;

        public void Trigger() => associatedScene.GetService<SampleSceneService>().Trigger();
    }
}