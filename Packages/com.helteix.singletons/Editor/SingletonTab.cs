using System.Collections.Generic;
using Helteix.Singletons.MonoSingletons;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Helteix.Singletons.Editor
{
    public class SingletonTab<T>
    {
        private readonly Tab tab;
        private ScrollView scrollView;
        private IVisualElementScheduledItem schedule;
        private bool separateScenes;

        private long last = -1;

        public SingletonTab(Tab tab, bool separateScenes)
        {
            this.tab = tab;
            this.separateScenes = separateScenes;

            scrollView = tab.Q<ScrollView>();
            schedule = tab.contentContainer.schedule.Execute(Refresh);
        }

        public void Refresh(TimerState timerState)
        {
            int currentWindowRefreshRate = SingletonSettings.Current.WindowRefreshRate;
            float refreshRate = (1f / currentWindowRefreshRate) * 1000;
            long rate = (long)refreshRate;
            if (rate != last)
            {
                last = rate;
                schedule.Every(rate);
            }
            scrollView.Clear();


            if (separateScenes)
                DrawForEveryScene();
            else
                DrawAllAtOnce();
        }

        private void DrawAllAtOnce()
        {
            MonoBehaviour[] all = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            VisualElement container = CreateContainer();
            bool any = false;
            for (int k = 0; k < all.Length; k++)
            {
                if (all[k] is not T child)
                    continue;
                any = true;
                VisualElement row = CreateSingletonRow(child);
                container.Add(row);
            }
            if(!any)
                container.Add(new HelpBox("Nothing found", HelpBoxMessageType.Info));

            scrollView.Add(container);
        }

        private void DrawForEveryScene()
        {
            for (int i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                VisualElement sceneContainer = CreateContainer();

                sceneContainer.Add(new Label(scene.name)
                {
                    style =
                    {
                        unityFontStyleAndWeight = FontStyle.Bold,
                        fontSize = 20,
                        unityTextAlign = TextAnchor.MiddleCenter,
                        paddingBottom = 15,
                        paddingTop = 5,
                    }
                });

                bool any = false;
                var roots = scene.GetRootGameObjects();
                for (int j = 0; j < roots.Length; j++)
                {
                    var root = roots[j];
                    var children = root.GetComponentsInChildren<MonoBehaviour>();
                    for (int k = 0; k < children.Length; k++)
                    {
                        if (children[k] is not T child)
                            continue;
                        any = true;
                        VisualElement row = CreateSingletonRow(child);
                        sceneContainer.Add(row);
                    }
                }

                if (!any)
                    sceneContainer.Add(new HelpBox("Nothing found in the scene", HelpBoxMessageType.Info));

                scrollView.Add(sceneContainer);
            }
        }

        private static VisualElement CreateContainer()
        {
            return new VisualElement()
            {
                style =
                {
                    backgroundColor = Color.gray2,
                    paddingBottom = 5,
                    paddingLeft = 5,
                    paddingRight = 5,
                    paddingTop = 5,
                    marginBottom = 3,
                    marginTop = 3,
                    borderBottomLeftRadius = 2,
                    borderBottomRightRadius = 2,
                    borderTopLeftRadius = 2,
                    borderTopRightRadius = 2,
                }
            };
        }

        private VisualElement CreateSingletonRow(T child)
        {
            VisualElement container = new VisualElement()
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    marginBottom = 5,
                    flexGrow = 1,
                    backgroundColor = Color.gray1,
                    paddingLeft = 5,
                    paddingRight = 5,
                    paddingTop = 5,
                    paddingBottom = 5,
                }
            };

            Label label = new Label(child.GetType().Name)
            {
                style =
                {
                    paddingRight = 20,
                    width = 200,
                    unityTextAlign = TextAnchor.MiddleLeft,
                    unityFontStyleAndWeight = FontStyle.Italic
                }
            };
            container.Add(label);

            ObjectField objectField = new ObjectField()
            {
                value = child as MonoBehaviour,
                style =
                {
                    flexGrow = 1,
                }
            };
            objectField.SetEnabled(false);

            container.Add(objectField);
            return container;
        }
    }
}