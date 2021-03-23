using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject sensor;

    public bool Detect(float range)
    {
        Collider[] cols = Physics.OverlapSphere(transform.localPosition, range);
        return cols.Length > 1 
            && cols.Any(x => x.transform != transform
                            && Vector3.Angle(sensor.transform.position - transform.position, x.transform.position - transform.position) < 45f
                            && Vector3.Distance(transform.position, x.transform.position) < range);
    }
}
