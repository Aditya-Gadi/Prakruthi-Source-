using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform
{
    public Vector3 pos;
    public Quaternion rot;

    public PlayerTransform(Vector3 _pos, Quaternion _rot)
    {
        pos = _pos;
        rot = _rot;
    }
}
