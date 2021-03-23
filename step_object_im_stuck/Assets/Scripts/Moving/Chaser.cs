using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Wanderer
{
    private Soul supreme;

    public override void Move(Vector3 direction, bool obstacle = false)
    {
        transform.localPosition += transform.forward * (obstacle ? 0 : Speed) * Time.deltaTime;
        Rotate(supreme.transform.position);
    }

    public void SetSupreme(Soul sup)
    {
        supreme = sup;
    }

    public Soul.SoulType GetSupremeType()
    {
        return supreme.SoulKind;
    }

}
