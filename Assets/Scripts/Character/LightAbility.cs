using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbility : CharacterAbility
{
    [SerializeField] private float intensity;
    [Header("References")]
    [SerializeField] private SoulWardAbility soulWardAbility;
    [Header("Stats")]
    [SerializeField] private bool isLight;
    protected override void Update()
    {
        if (!isUnlock && isLight)
        {
            isLight = false;
        }
        base.Update();
    }
    protected override void Action()
    {
        isLight = !isLight;
        soulWardAbility.soulward.soulwardObject.Light(isLight, intensity);
    }
}
