using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour, IMovable
{
    
    public float Speed = 1f;
    protected float turnRate = 1f;
    public virtual void Move(Vector3 direction, bool obstacle = false)
    {
        transform.localPosition += transform.forward * (obstacle ? 0 : Speed) * Time.deltaTime;
        Rotate(direction);
    }

    public virtual void Rotate(Vector3 direction)
    {
        var targetRotation = Quaternion.LookRotation(direction - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnRate);
    }


}
