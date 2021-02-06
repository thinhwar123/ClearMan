using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("explosion");
        Destroy(gameObject, 3);
    }
    
}
