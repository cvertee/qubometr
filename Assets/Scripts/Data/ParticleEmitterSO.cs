using System.Linq;
using Core;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Particleemitter", menuName = "Particles/Emitter", order = 0)]
    public class ParticleEmitterSO : ScriptableObject, IParticleEmitter
    {
        public ParticleDataSO particleData;
        
        public void Emit(string particleName, Vector3 position)
        {
            var particle = particleData.particleInfos.FirstOrDefault(x => x.name == particleName);
            if (particle == null)
            {
                Debug.LogWarning($"Can't find particle {particleName}");
                return;
            }

            var particleGo = Instantiate(particle.prefab);
            particleGo.transform.position = position;
        }
    }
}