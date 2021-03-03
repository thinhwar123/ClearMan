using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : MonoBehaviour
{
    private PlayerEffect playerEffect; 
    public void AnimationFinish()
    {
        playerEffect.AnimationHealFinsh();
    }
    public void SetPlayerEffect(PlayerEffect playerEffect)
    {
        this.playerEffect = playerEffect;
    }
}
