using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitat : MonoBehaviour
{
    [Header("Config")]
    public Vector3 habitatSize;
    public int soulPopulation;
    public int raceDiversity;
    public int minWeight;
    public int maxWeight;
    public float captureDistance;

    [Header("Soul Prefab")]
    public Soul proto;

    [Space(10)]
    public Camera mainCamera;

    private void Start()
    {
        transform.localScale = habitatSize;
        for (int i = 0; i < soulPopulation; ++i)
        {
            Soul soul = Instantiate(proto, Vector3.zero, Quaternion.identity);
            soul.SetSoul(habitatSize, 
                Random.Range(minWeight, maxWeight),
                Random.Range(0,raceDiversity),
                captureDistance);
        }

    }

}
