using Helteix.ChanneledProperties.Priorities;
using TMPro;
using UnityEngine;

namespace Helteix.ChanneledProperties.Samples
{
    public class SamplePPManager : MonoBehaviour
    {
        [SerializeField]
        public Priority<string> texts;

        [SerializeField]
        private TextMeshProUGUI text;

        private void Awake()
        {
            texts = new Priority<string>("No channels");
            texts.AddOnValueChangeCallback(Texts_OnValueChanged);
        }

        private void Texts_OnValueChanged(string value)
        {
            text.SetText(value);
        }
    }
}