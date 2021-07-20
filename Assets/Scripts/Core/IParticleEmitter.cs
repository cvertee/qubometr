using UnityEngine;

namespace Core
{
    public interface IParticleEmitter
    {
        void Emit(string name, Vector3 position);
    }
}