using UnityEngine;

namespace Helteix.ChanneledProperties.Samples
{
    public class FormulaGroupUI : MonoBehaviour
    {
        public int Group => transform.GetSiblingIndex();

        private FormulaUI formulaUI;

        public void Destroy() => Destroy(gameObject);
    }
}