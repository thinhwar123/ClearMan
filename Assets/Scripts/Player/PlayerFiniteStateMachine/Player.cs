using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region PlayerAttribute
    //public float curHitPoint { get; private set; }
    //public float curManaPoint { get; private set; }
    #endregion
    #region State Variable
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerChangeSoulwardState changeSoulwardState { get; private set; }
    public PlayerBurstState burstState { get; private set; }
    public PlayerWingJumpState wingJumpState { get; private set; }
    public PlayerGlideState glideState { get; private set; }
    public PlayerTeleportState teleportState { get; private set; }
    public PlayerArrowState arrowState { get; private set; }
    public PlayerSwimState swimState { get; private set; }
    public PlayerBubbleState bubbleState { get; private set; }
    public PlayerMoveObjectState moveObjectState { get; private set; }
    public PlayerLookState lookState { get; private set; }
    public PlayerSaveState saveState { get; private set; }
    public PlayerOpenBookState openBookState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerBounceState bounceState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public PlayerHealState healState { get; private set; }
    public PlayerStunState stunState { get; private set; }
    [SerializeField] public PlayerAbilityData playerData;
    #endregion
    #region Components
    public Animator anim { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SoulwardEnity soulward { get; private set; }
    public CapsuleCollider2D playerCollider { get; private set; }

    #endregion
    #region CheckTranforms
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform windCheck;
    [SerializeField] private Transform waterCheck;
    #endregion

    #region Other Variable

    [SerializeField] public Transform soulwardPosition;
    [SerializeField] public SoulwardEnity soulwardPrefab;
    [SerializeField] public PlayerEffect effect;
    [SerializeField] public BoxCollider2D attackHitBox;
    [SerializeField] public BoxCollider2D attackUpHitBox;
    [SerializeField] public BoxCollider2D attackDownHitBox;
    public InteractionItem curInteractionItem { get; private set; }
    public bool isUnavailable { get; private set; }
    public Vector2 currentVelocity { get; private set; }
    public int facingDirection { get; private set; }

    private float normalGravityScale;
    private Vector2 workspace;
    private bool castInput;
    private bool stopCastInput;
    #endregion

    #region Debug Variable
    //[SerializeField] public bool isWind;
    //[SerializeField] public bool endWind;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
        dashState = new PlayerDashState(this, stateMachine, playerData, "dash");
        changeSoulwardState = new PlayerChangeSoulwardState(this, stateMachine, playerData, "idle");
        burstState = new PlayerBurstState(this, stateMachine, playerData, "burst");
        wingJumpState = new PlayerWingJumpState(this, stateMachine, playerData, "wingJump");
        glideState = new PlayerGlideState(this, stateMachine, playerData, "glide");
        teleportState = new PlayerTeleportState(this, stateMachine, playerData, "aim");
        arrowState = new PlayerArrowState(this, stateMachine, playerData, "aim");
        swimState = new PlayerSwimState(this, stateMachine, playerData, "swim");
        bubbleState = new PlayerBubbleState(this, stateMachine, playerData, "bubble");
        moveObjectState = new PlayerMoveObjectState(this, stateMachine, playerData, "moveObject");
        lookState = new PlayerLookState(this, stateMachine, playerData, "look");
        saveState = new PlayerSaveState(this, stateMachine, playerData, "save");
        openBookState = new PlayerOpenBookState(this, stateMachine, playerData, "openBook");
        attackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        bounceState = new PlayerBounceState(this, stateMachine, playerData, "inAir");
        hurtState = new PlayerHurtState(this, stateMachine, playerData, "hurt");
        deadState = new PlayerDeadState(this, stateMachine, playerData, "dead");
        healState = new PlayerHealState(this, stateMachine, playerData, "heal");
        stunState = new PlayerStunState(this, stateMachine, playerData, "stun");
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        soulward = Instantiate(soulwardPrefab, transform.position, Quaternion.identity, soulwardPosition);
        soulward.ChangeType(playerData.soulwardType);
        inputHandler = DataGlobe.instance.inputHandler;
        facingDirection = 1;
        normalGravityScale = rb.gravityScale;
        stateMachine.Initialize(idleState);
    }
    private void Update()
    {
        currentVelocity = rb.velocity;
        stateMachine.currentState.LogicUpdate();
        castInput = inputHandler.castAbilityInput;
        stopCastInput = inputHandler.stopCastAbilityInput;
        if (castInput )
        {
            if (playerData.soulwardType == 2)
            {
                inputHandler.UseCastAbilityInput();
                soulward.SwitchLight();
            }
        }
        else if (stopCastInput)
        {
            inputHandler.UseStopCastAbilityInput();
            if (playerData.soulwardType == 3 && teleportState.canTeleport && stateMachine.currentState != teleportState)
            {
                ResetSoulwardPosition();
            }
        }
    }
    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }
    public void SetVelocity(float velocityX, float velocityY)
    {
        workspace.Set(velocityX, velocityY);
        rb.velocity = workspace;
        currentVelocity = workspace;
    }
    public void SetGravityScale(float gravity)
    {
        if (gravity == -1)
        {
            rb.gravityScale = normalGravityScale;
        }
        else
        {
            rb.gravityScale = gravity;
        }        
    }
    public void SetWaterCheckTrigger(bool isTrigger)
    {
        waterCheck.GetComponent<CircleCollider2D>().isTrigger = isTrigger;
    }
    public void SetSoulwardType(int type)
    {
        playerData.soulwardType = type;
    }
    #endregion

    #region Check Functions
    public void CheckFlip(int inputX)
    {
        if (inputX !=0 && inputX != facingDirection)
        {
            Flip();
        }
    }
    public RaycastHit2D CheckIfTouchSpecialPlatform()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, playerData.onewayPlatformCheckDistance, playerData.whatIsSpecialPlatform);
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround) || CheckIfTouchSpecialPlatform();
    }
    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }
    public bool CheckIfInWindArea()
    {
        return Physics2D.OverlapCircle(windCheck.position, playerData.windCheckRadius, playerData.whatIsWind);
    }
    public bool CheckIfEndWindArea()
    {
        return Physics2D.OverlapCircle(windCheck.position + Vector3.down , playerData.windCheckRadius, playerData.whatIsWind);
    }
    public bool CheckIfInWarter()
    {
        return Physics2D.OverlapCircle(waterCheck.position, playerData.waterCheckRadius, playerData.whatIsWater);
    }
    public bool CheckIfInDeepWater()
    {
        return Physics2D.OverlapCircle(waterCheck.position, playerData.waterDeepCheckRadius, playerData.whatIsWater);
    }
    public bool CheckIfTouchingPhysicalObject()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerData.physicalObjectCheckDistance, playerData.whatIsPhysicalObject);
    }
    public bool CheckIfTouchingBounceObject( Vector2 dir)
    {
        return Physics2D.Raycast(transform.position, dir , playerData.bounceObjectCheckDistance, playerData.whatIsBounceObject);
    }
    public bool CheckIfTouchingInteractionItem()
    {
        Collider2D[] hit = Physics2D.OverlapCapsuleAll(transform.position, playerCollider.size, playerCollider.direction, 0, playerData.whatIsInteractionItem);

        if (hit.Length != 0)
        {
            InteractionItem newInteractionItem = hit[0].GetComponent<InteractionItem>();
            foreach (Collider2D item in hit)
            {
                if ((transform.position - item.transform.position).magnitude < (transform.position - newInteractionItem.transform.position).magnitude)
                {
                    newInteractionItem = item.GetComponent<InteractionItem>();
                }
            }
            if (curInteractionItem != newInteractionItem)
            {
                if (curInteractionItem != null)
                {
                    curInteractionItem.UnChooseItem();
                }
                curInteractionItem = newInteractionItem;
                curInteractionItem.ChooseItem();
            }
        }
        else
        {
            if (curInteractionItem != null)
            {
                curInteractionItem.UnChooseItem();
                curInteractionItem = null;
            }
        }
        return hit.Length != 0;
    }
    #endregion

    #region Other Functions
    public void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.localScale = new Vector3(facingDirection, 1, 1);
        soulwardPosition.localScale = new Vector3(facingDirection, 1, 1);
    }
    public void ResetSoulwardPosition()
    {
        if (playerData.soulwardType == 3)
        {
            if (soulward.hitObject)
            {
                inAirState.SetSoulwardHitObject();
            }
            teleportState.SetCanTeleport(false);
        }

        soulward.Disappear();

        soulward = Instantiate(soulwardPrefab, transform.position, Quaternion.identity, soulwardPosition);
        soulward.ChangeType(playerData.soulwardType);

    }
    public void TakeDame(AttackDetails attackDetails)
    {
        if (hurtState.CheckCanHurt())
        {
            DataGlobe.instance.playerAttributeData.curHitPoint -= attackDetails.attackDamage;
            DataGlobe.instance.systemUI.SetHitPoint();
            if (DataGlobe.instance.playerAttributeData.curHitPoint <= 0)
            {
                stateMachine.ChangeState(deadState);
            }
            else
            {
                hurtState.SetAttackDetails(attackDetails);
                stateMachine.ChangeState(hurtState);
            }            
        }
    }
    public void Heal()
    {
        DataGlobe.instance.playerAttributeData.curHitPoint += playerData.healValue;
        DataGlobe.instance.playerAttributeData.curManaPoint -= playerData.manaUseHeal;
        DataGlobe.instance.systemUI.SetHitPoint();
        DataGlobe.instance.systemUI.SetManaPoint();
    }
    public void GainMana(float manaPoint)
    {
        DataGlobe.instance.playerAttributeData.curManaPoint += manaPoint;
        if (DataGlobe.instance.playerAttributeData.curManaPoint > DataGlobe.instance.playerAttributeData.maxManaPoint)
        {
            DataGlobe.instance.playerAttributeData.curManaPoint = DataGlobe.instance.playerAttributeData.maxManaPoint;
        }
        DataGlobe.instance.systemUI.SetManaPoint();
    }
    public IEnumerator Unvailable()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("HitObject"), true);
        yield return new WaitForSeconds(playerData.unavailableTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("HitObject"), false);
    }
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }
    public void AnimationFinish()
    {
        stateMachine.currentState.AnimationFinish();
    }

    #endregion
}
