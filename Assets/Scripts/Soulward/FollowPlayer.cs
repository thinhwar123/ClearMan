using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Start()
    {
        target = GameObject.Find("SoulwardTarget");
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    public void Follow()
    {
        transform.position = target.transform.position;
    }
}
