using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMakingBindMovementBetweenObjects : MonoBehaviour
{
    public Transform toMove;
    private Vector3 offset;

    void Start()
    {
        if (this.name != "HiddenPositionTrack")
        {
            offset = toMove.position - transform.position;
        }
    }
    void Update()
    {
        if (this.name != "HiddenPositionTrack") {
            toMove.position = transform.position - offset;
            transform.rotation = toMove.rotation;
        }
    }

}
