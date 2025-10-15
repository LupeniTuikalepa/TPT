using Helteix.Singletons.MonoSingletons;
using UnityEngine;

namespace Helteix.Singletons.Samples.MonoSingletons
{
    public class SampleMonoSingleton : MonoSingleton<SampleMonoSingleton>
    {
        public void Log() => Debug.Log("Logging from the instance");
    }
}