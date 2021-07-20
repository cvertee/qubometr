using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ParticleInfo
    {
        public string name;
        public GameObject prefab;
    }
    
    [CreateAssetMenu(fileName = "ParticleDataName", menuName = "Particles/Particle Data", order = 0)]
    public class ParticleDataSO : ScriptableObject
    {
        public List<ParticleInfo> particleInfos;
    }
}