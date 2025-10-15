using System;
using System.Collections.Generic;
using UnityEngine;

namespace Helteix.Tools.TypeMapping
{
    [Serializable]
    public struct TypeMapEntry
    {
        [SerializeField]
        public string guid;
        [SerializeField]
        public string typeInfos;
    }

    public class TypeMappingCollection : ScriptableObject
    {
        [SerializeField]
        public List<TypeMapEntry> entries;
    }
}