using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    public bool alwaysApplyDrag = false;
    
    private void FixedUpdate()
    {
        var position = transform.position;
        
        rigidBody.AddForceAtPosition(Physics.gravity/floaterCount, position, ForceMode.Acceleration);
        
        var waveHeight = WaveManager.Instance.GetWaveHeight(position);

        if (position.y < waveHeight)
        {
            var displacementMultiplier = Mathf.Clamp01((waveHeight - position.y) / depthBeforeSubmerged) * displacementAmount;
        
            rigidBody.AddForceAtPosition(new Vector3(0f, (Math.Abs(Physics.gravity.y) * displacementMultiplier), 0f) / floaterCount,
                position, ForceMode.Acceleration);

            if (!alwaysApplyDrag)
            {
                rigidBody.AddForce(-rigidBody.velocity * (displacementMultiplier * waterDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
                rigidBody.AddTorque(-rigidBody.angularVelocity * (displacementMultiplier * waterAngularDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
            }
        }

        if (alwaysApplyDrag)
        {
            rigidBody.AddForce(-rigidBody.velocity * ((waterDrag * Time.fixedDeltaTime / floaterCount)), ForceMode.VelocityChange);
            rigidBody.AddTorque(-rigidBody.angularVelocity * ((waterDrag * Time.fixedDeltaTime / floaterCount)), ForceMode.VelocityChange);
        }
        
    }
}
