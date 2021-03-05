using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnewayPlatform : MonoBehaviour
{
    [SerializeField] private float timeDeactive;
    private PlatformEffector2D platformEffector2D;
    private bool active;
    private float startTime;
    public void Start()
    {
        active = true;
        startTime = -10;
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }
    public void Update()
    {
        if (Time.time > startTime + timeDeactive && !active)
        {
            platformEffector2D.rotationalOffset = 0;
            active = true;
        }
    }
    public void DeactivePlatform()
    {
        startTime = Time.time;
        active = false;
        platformEffector2D.rotationalOffset = 180;
    }
}
