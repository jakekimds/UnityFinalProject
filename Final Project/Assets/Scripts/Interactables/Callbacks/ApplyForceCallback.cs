using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForceCallback : Callback
{

    public Rigidbody RB;
    public Vector3 Direction;
    public float Force;

    public override void OnCall()
    {
        RB.isKinematic = false;
        RB.AddForce(Direction.normalized * Force, ForceMode.Impulse);
    }
}
