using System.Globalization;
using Helteix.ChanneledProperties.Formulas;
using TMPro;
using UnityEngine;

namespace Helteix.ChanneledProperties.Samples
{
    public class FormulaUI : MonoBehaviour
    {
        [SerializeField]
        private float startValue = 50;
        [SerializeField]
        private TextMeshProUGUI result;
        [SerializeField]
        private Transform content;
        [SerializeField]
        private FormulaGroupUI prefab;

        [field: SerializeField]
        public Formula<float> Formula { get; private set; }

        private void Awake()
        {
            Formula = new Formula<float>(startValue, 64, true);
            Formula.AddOnValueChangeCallback(ctx =>
            {
                result.text = ctx.ToString(CultureInfo.InvariantCulture);
            }, true);

            Formula.AddOperation(ChannelKey.GetUniqueChannelKey("Formula"));
            Formula.AddOperation(ChannelKey.GetUniqueChannelKey());
        }


        public void AddNew() => Instantiate(prefab, content);
    }
}