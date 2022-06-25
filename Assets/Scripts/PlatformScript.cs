using System;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject platformRG;
    public GameObject playerManagerObject;

    public float xRotationConstraint = 45f;
    public float zRotationConstraint = 45f;

    private Rigidbody platformRigidbody;
    private PlayerManager _playerManagerScript;

    private float xRotationMinConstraint;
    private float zRotationMinConstraint;

    private bool isStartGameTriggered = false;


    private void Awake()
    {
        xRotationMinConstraint = 360 - xRotationConstraint;
        zRotationMinConstraint = 360 - zRotationConstraint;

        _playerManagerScript = playerManagerObject.GetComponent<PlayerManager>();
        platformRigidbody = platformRG.GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        RotationCheck();
    }

    private void FixedUpdate()
    {
        if (!_playerManagerScript.GetStartGame() || isStartGameTriggered) return;
        print("GAME HAS BEGUN");
        isStartGameTriggered = true;
        PlatformUnfreeze();
    }

    private void PlatformUnfreeze()
    {
        platformRigidbody.constraints = ~RigidbodyConstraints.FreezeRotationX & ~RigidbodyConstraints.FreezeRotationZ;      
    }

    private void RotationCheck()
    {
        var platformRotation = platformRG.transform.localEulerAngles;

        if (platformRotation.x > xRotationConstraint && platformRotation.x < 90)
        {
            platformRotation.x = xRotationConstraint;
        }

        if (platformRotation.x < xRotationMinConstraint && platformRotation.x > 270)
        {
            platformRotation.x = xRotationMinConstraint;
        }
        
        if (platformRotation.z > zRotationConstraint && platformRotation.z < 90)
        {
            platformRotation.z = zRotationConstraint;
        }

        if (platformRotation.z < zRotationMinConstraint && platformRotation.z > 270)
        {
            platformRotation.z = zRotationMinConstraint;
        }

        platformRG.transform.localEulerAngles = platformRotation;
    }
}
