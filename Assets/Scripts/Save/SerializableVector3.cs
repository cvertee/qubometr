using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Save
{
    [Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3(Vector3 vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
        }

        public Vector3 ToNormalVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}