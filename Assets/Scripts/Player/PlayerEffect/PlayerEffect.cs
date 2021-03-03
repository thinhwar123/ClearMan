using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] private Player player;
    [Header("DashEffect")]
    [SerializeField] private GameObject dashEffectPrefab;
    [SerializeField] private float dashEffectDuration;
    private bool isDash;
    [Header("JumpEffect")]
    [SerializeField] private ParticleSystem jumpEffect;
    [Header("ExplosionEffect")]
    [SerializeField] private GameObject explosionEffectPrefab;
    [Header("HurtEffect")]
    [SerializeField] private int fadeTime;
    [SerializeField] private List<ParticleSystem> hurtEffectList;
    [Header("HealEffecr")]
    [SerializeField] private GameObject healEffectPrefab;
    private GameObject temp;
    public void StartDashEffect()
    {
        isDash = true;
        StartCoroutine(DashEffect());
    }
    public void StopDashEffect()
    {
        isDash = false;
    }
    public IEnumerator DashEffect()
    {
        while (isDash)
        {
            GameObject tempObject = Instantiate(dashEffectPrefab, player.transform.position, Quaternion.identity);
            tempObject.GetComponent<DashEffect>().Action(player.GetComponent<SpriteRenderer>().sprite, dashEffectDuration, player.transform.localScale);
            yield return new WaitForSeconds(0.03f);
        }
    }
    public void StartHealEffect()
    {
        temp = Instantiate(healEffectPrefab, player.transform.position, Quaternion.identity);
        temp.GetComponent<HealEffect>().SetPlayerEffect(this);
    }
    public void StopHealEffect()
    {
        Destroy(temp);
    }
    public void TriggerHealEffect()
    {
        temp.GetComponent<Animator>().SetTrigger("heal");
    }
    public void AnimationHealFinsh()
    {
        player.Heal();
    }

    public void TriggerJumpEffect()
    {
        jumpEffect.Play();
    }
    public void TriggerExplosionEffect()
    {
        Instantiate(explosionEffectPrefab, player.transform.position, Quaternion.identity);
    }
    public void TriggerHurtEffect()
    {
        player.GetComponent<SpriteRenderer>().DOColor(new Color(0.5f, 0.5f, 0.5f), player.playerData.unavailableTime/ fadeTime/2).SetLoops( fadeTime*2, LoopType.Yoyo);
    }
}
