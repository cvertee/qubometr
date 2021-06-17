using System;
using System.Collections;
using UnityEngine;

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

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var svec3 = obj as SerializableVector3;
        return svec3.x == x && svec3.y == y && svec3.z == z;
    }
}