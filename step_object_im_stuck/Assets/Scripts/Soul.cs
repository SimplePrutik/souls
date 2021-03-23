using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public enum SoulType
    {
        Air,
        Earth,
        Fire,
        Water
    }

    private int weight;
    private SoulType soulKind;
    private Vector3 direction;
    private Vector3 boundaries;

    private IMovable mover => GetComponent<IMovable>();

    public int Weight => weight;
    public SoulType SoulKind => soulKind;

    public List<Material> souls;
    public MeshRenderer mesh;

    private SphereCollider capturer => GetComponent<SphereCollider>();
    private Detector detector => GetComponent<Detector>();

    private float lastObstacleChangeDir = 0;
    private float cooldownObstacleChangeDir = 2f;

    private void Update()
    {
        var obstacle = detector.Detect(capturer.radius);
        mover.Move(direction, obstacle);
        if (obstacle && lastObstacleChangeDir + cooldownObstacleChangeDir < Time.timeSinceLevelLoad)
        {
            lastObstacleChangeDir = Time.timeSinceLevelLoad;
            ChangeDirection();
        }
        if ((transform.localPosition - direction).magnitude < 0.25f)
        {
            ChangeDirection();
        }
        if (chaser)
        {
            var supremeType = (mover as Chaser).GetSupremeType();
            if (supremeType != SoulKind)
                SetSoulColor((int)supremeType);
        }
    }

    public void SetSoul(Vector3 habitat, int weight, int type, float capture)
    {

        this.weight = weight;
        transform.localScale = Vector3.one * weight;

        //var soulSize = detector * transform.localScale;
        boundaries = habitat / 2.0f - capturer.radius * Vector3.one;

        transform.localPosition = RandomPointInside();
        Debug.Log("Start position: " + transform.localPosition);

        capturer.radius = capture;

        SetSoulColor(type);

        ChangeDirection();
    }

    private void ChangeDirection()
    {
        direction = RandomPointInside();
        Debug.Log("Changed direction: " + direction);
    }

    private Vector3 RandomPointInside()
    {
        return new Vector3(
            Random.Range(-boundaries.x, boundaries.x),
            Random.Range(-boundaries.y, boundaries.y),
            Random.Range(-boundaries.z, boundaries.z));
    }

    private void SetSoulColor(int colour)
    {
        soulKind = (SoulType)colour;
        mesh.material = souls[Mathf.Min(souls.Count, colour)];
    }

    private bool chaser = false;

    private void OnTriggerEnter(Collider other)
    {
        if (chaser || other.transform == transform)
            return;

        var _soul = other.transform.GetComponent<Soul>();
        if (_soul.Weight > Weight)
        {
            Destroy(GetComponent<Wanderer>());
            gameObject.AddComponent<Chaser>().SetSupreme(_soul);
            SetSoulColor((int)_soul.soulKind);
            chaser = true;
        }
    }

    
}
