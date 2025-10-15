using System.Collections.Generic;
using System.Reflection;
using Helteix.Tools.Editor.Settings.Drawers;
using Helteix.Tools.Settings;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Helteix.Tools.Editor.Settings
{
    public static class GameSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateMainSettingsProvider()
        {
            var provider = new SettingsProvider("Helteix", SettingsScope.Project)
            {
                activateHandler = (s, container) =>
                {
                    GameSettingsEditorPrefs instance = GameSettingsEditorPrefs.instance;

                    InspectorElement inspector = new InspectorElement(instance);
                    inspector.StyleSettingsContentWithTitle("Global Settings", new Color(0.2f, 0.54f, 0.8f));

                    container.Add(inspector);
                }
            };

            return provider;
        }

        [SettingsProviderGroup]

        public static SettingsProvider[] CreateOtherSettingsProvider()
        {
            List<SettingsProvider> providers = new List<SettingsProvider>();
            foreach (var type in GameSettingsAssetDatabase.CachedSettingsType)
            {
                GameSettingsTitleAttribute att = type.GetCustomAttribute(typeof(GameSettingsTitleAttribute)) as GameSettingsTitleAttribute;

                string title = att?.title?? type.Name;
                Color color = att?.color ?? GameSettingsTitleAttribute.DefaultColor;

                if(GameSettingsAssetDatabase.GetSettingsTypeQuantity(type) == 0)
                    continue;

                SettingsProvider provider = new($"Helteix/{title}", SettingsScope.Project)
                {
                    activateHandler = (s, container) =>
                    {
                        container.StyleSettingsContentWithTitle(title, color);
                        GameSettingsCollection collection = new GameSettingsCollection(type);
                        container.Add(collection);
                    },
                };
                providers.Add(provider);
            }

            return providers.ToArray();
        }
    }
}