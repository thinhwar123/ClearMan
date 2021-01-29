using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [SerializeField] private GameObject CharacterGraphic;
    [SerializeField] private DashAbility dashAbility;
    [SerializeField] private GameObject DashEffectPrefab;
    [SerializeField] private float duration;
    [SerializeField] private bool isDash;
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField] private ParticleSystem wallSlideEffect;
    [SerializeField] private GameObject explosionEffect;
    public void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void Update()
    {
        if (dashAbility.isDash)
        {
            StartCoroutine(DashEffect());            
        }
    }
    public IEnumerator DashEffect()
    {
        GameObject tempObject = Instantiate(DashEffectPrefab, CharacterGraphic.transform.position, Quaternion.identity);
        tempObject.GetComponent<DashEffect>().Action(CharacterGraphic.GetComponent<SpriteRenderer>().sprite, duration, dashAbility.transform.localScale);
        yield return new WaitForSeconds(0.2f);
        if (dashAbility.isDash)
        {
            StartCoroutine(DashEffect());
        }

    }
    public void HealEffect()
    {
        ani.SetTrigger("heal");
    }
    public void JumpEffect()
    {
        jumpEffect.Play();
    }
    public void WallSlide(bool isPlay)
    {
        if (isPlay)
        {
            wallSlideEffect.Play();
        }
        else
        {
            wallSlideEffect.Stop();
        }
    }
    public void ExplosionEffect()
    {
        Instantiate(explosionEffect, transform.parent.parent.position, Quaternion.identity);
    }
}
