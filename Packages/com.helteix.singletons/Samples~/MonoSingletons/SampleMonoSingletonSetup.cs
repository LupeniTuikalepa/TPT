using System;
using UnityEngine;

namespace Helteix.Singletons.Samples.MonoSingletons
{
    public class SampleMonoSingletonSetup : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Press space to create the MonoSingleton...");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                enabled = false;
                SampleMonoSingleton.Instance.Log();
            }
        }
    }
}