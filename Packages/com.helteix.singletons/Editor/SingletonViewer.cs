using Helteix.Singletons.Editor;
using Helteix.Singletons.MonoSingletons;
using Helteix.Singletons.SceneServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SingletonViewer : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;


    private SingletonTab<IMonoSingleton> monoSingletonTab;
    private SingletonTab<ISceneService> sceneServicesTab;

    [MenuItem("Window/Helteix/Singleton Viewer", priority = -50)]
    public static void ShowExample()
    {
        SingletonViewer wnd = GetWindow<SingletonViewer>();
        wnd.titleContent = new GUIContent("SingletonViewer");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        VisualElement content = m_VisualTreeAsset.Instantiate();

        monoSingletonTab = new SingletonTab<IMonoSingleton>(content.Q<Tab>("MonoSingletons"), false);
        sceneServicesTab = new SingletonTab<ISceneService>(content.Q<Tab>("SceneServices"), true);

        root.Add(content);
    }
}