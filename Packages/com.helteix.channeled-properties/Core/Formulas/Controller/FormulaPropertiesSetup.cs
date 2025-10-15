using UnityEngine;

namespace Helteix.ChanneledProperties.Formulas
{
    public static class FormulaPropertiesSetup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            if(ChanneledPropertiesSettings.Current.LogInitialisationMessages)
                Debug.Log($"[Channeled properties] Initializing default operators...");

            CalculatorController<float>.Register<FloatCalculator>();
            CalculatorController<int>.Register<IntCalculator>();
            CalculatorController<Vector2>.Register<Vector2Calculator>();
            CalculatorController<Vector3>.Register<Vector3Calculator>();
        }
    }
}