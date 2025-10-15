using System;
using Helteix.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Helteix.Singletons.Samples.SceneServices
{
    public class OnePerSceneSetup : MonoBehaviour
    {
        [SerializeField, Range(2, 10)]
        private int sceneCount = 2;

        [SerializeField]
        private SampleServiceUI template;

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Image background;


        private void OnEnable()
        {
#pragma warning disable UDR0005
            SampleSceneService.SetBackgroundColor += OnColorChanged;
#pragma warning restore UDR0005
        }

        private void OnDisable()
        {
            SampleSceneService.SetBackgroundColor -= OnColorChanged;
        }

        private void OnColorChanged(Color color)
        {
            background.color = color;
        }

        void Start()
        {
            container.ClearChildren(template.transform);
            for (int i = 0; i < sceneCount; i++)
            {
                Scene scene = SceneManager.CreateScene($"Scene {i + 1}");
                GameObject go = new GameObject($"Service {i + 1}");

                SampleSceneService sampleSceneService = go.AddComponent<SampleSceneService>();

                SampleServiceUI ui = GameObject.Instantiate(template, container);
                ui.associatedScene = scene;
                ui.gameObject.SetActive(true);

                sampleSceneService.MoveToScene(scene);

                ui.Sync();
            }

        }
    }
}