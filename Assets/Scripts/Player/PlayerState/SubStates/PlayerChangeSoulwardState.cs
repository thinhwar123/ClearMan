using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeSoulwardState : PlayerAbilityState
{

    protected bool changeSoulwardInput;
    protected int startSoulwardType;
    protected int lastSoulwardType;
    protected GameObject tempSoulwardUI;

    private float cameraHeight;
    private float cameraWidth;
    public PlayerChangeSoulwardState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
        player.SetVelocityY(0);

        Time.timeScale = playerData.changeSoulwardTimeScale;
        startSoulwardType = player.playerData.soulwardType;
        lastSoulwardType = player.playerData.soulwardType;
        tempSoulwardUI = player.soulward.InstantiateChangeSoulawrdUI(GetMousePosition(true));
        tempSoulwardUI.GetComponent<SoulwardUI>().Appear();

    }

    public override void Exit()
    {
        Time.timeScale = 1;
        tempSoulwardUI.GetComponent<SoulwardUI>().Disappear();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        changeSoulwardInput = player.inputHandler.changeSoulwardInput;
        if (!isExitingState)
        {
            if (changeSoulwardInput)
            {
                tempSoulwardUI.GetComponent<SoulwardUI>().ChooseType(GetSoulType());
                ChangeType(GetSoulType());
            }
            else
            {
                isAbilityDone = true;
            }
        }

    }
    public Vector3 GetMousePosition(bool isFix)
    {

        Vector3 res = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!isFix)
        {
            return new Vector3(res.x, res.y, 0);
        }
        if (cameraWidth / 2 - Mathf.Abs(res.x - Camera.main.transform.position.x) < playerData.safeUIDistance)
        {
            res = new Vector3((res.x - Camera.main.transform.position.x < 0 ? -1 : 1) * (cameraWidth / 2 - playerData.safeUIDistance) + Camera.main.transform.position.x, res.y, 0);
        }
        if (cameraHeight / 2 - Mathf.Abs(res.y - Camera.main.transform.position.y) < playerData.safeUIDistance)
        {
            res = new Vector3(res.x, (res.y - Camera.main.transform.position.y < 0 ? -1 : 1) * (cameraHeight / 2 - playerData.safeUIDistance) + Camera.main.transform.position.y, 0);
        }
        return new Vector3(res.x, res.y, 0);
    }
    public int GetSoulType()
    {
        Vector3 mouseDirection = GetMousePosition(false) - tempSoulwardUI.transform.position;
        if (mouseDirection.magnitude < 1)
        {
            return -1;
        }
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, mouseDirection, Vector3.forward);
            int type = Mathf.RoundToInt((angle / 60) + 3f);
            if (type >= 5.5)
            {
                type = 0;
            }

            return type;
        }
    }
    public void ChangeType(int typeChange)
    {
        if (typeChange == -1)
        {
            player.playerData.soulwardType = startSoulwardType;
            player.soulward.ChangeType(player.playerData.soulwardType);

        }
        else if (typeChange != lastSoulwardType)
        {
            player.playerData.soulwardType = typeChange;
            lastSoulwardType = typeChange;
            player.soulward.ChangeType(player.playerData.soulwardType);

        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanChangeSoulwardType()
    {
        return !player.teleportState.canTeleport;
    }
}
