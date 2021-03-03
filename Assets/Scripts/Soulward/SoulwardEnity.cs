using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
public class SoulwardEnity : MonoBehaviour
{
    public SoulwardData baseData;
    public bool canTeleport { get; private set; }
    public bool hitObject { get; private set; }
    private Tween tweenX;
    private Sequence tweenY;
    private Tween tweenZ;
    private bool isLight;
    [SerializeField] private GameObject changeSoulwardUIPrefab;
    [SerializeField] private GameObject aimUIPrefab;
    [SerializeField] private ParticleSystemRenderer particleSystemRenderer;
    private GameObject canvas;
    private GameObject aimUI;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem ps;
    private Rigidbody2D rb;
    private CircleCollider2D coll;
    [SerializeField] private GameObject bullet;
    private IEnumerator timeTravel;
    public void Start()
    {
        canvas = GameObject.Find("Canvas");
        transform.localPosition = Vector3.right * baseData.distance;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        isLight = false;
        canTeleport = false;
        hitObject = false;
        Appear();
        Follow();
    }
    public void Update()
    {
    }
    public void Follow()
    {
        if (tweenZ == null)
        {
            tweenZ = transform.parent.transform.DORotate(new Vector3(0, 0, 45), 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else
        {
            tweenZ.Play();
        }
        tweenX = transform.DOLocalMoveX(-baseData.distance, 1 / baseData.movementSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(baseData.easeX);
        tweenY = DOTween.Sequence();
        tweenY.Append(transform.DOLocalMoveY(0.3f, 0.5f / baseData.movementSpeed).SetLoops(2, LoopType.Yoyo).SetEase(baseData.easeY)).Append(transform.DOLocalMoveY(-0.3f, 0.5f / baseData.movementSpeed).SetLoops(2, LoopType.Yoyo).SetEase(baseData.easeY));
        tweenY.SetLoops(-1, LoopType.Restart);
        tweenY.Play();
    }
    public void StopFollow()
    {
        tweenX.Pause();
        tweenY.Pause();
        tweenZ.Pause();
    }
    public void ContinueFollow()
    {
        tweenX.Play();
        tweenY.Play();
        tweenZ.Play();
    }
    public GameObject InstantiateChangeSoulawrdUI(Vector2 startMousePosition)
    {
        return Instantiate(changeSoulwardUIPrefab, startMousePosition, Quaternion.identity, canvas.transform);
    }
    public void ChangeType(int type)
    {
        sprite.sprite = baseData.spriteList[type];
        ParticleSystem.MainModule main = ps.main;
        main.startColor = baseData.colorList[type];
        if (type != 2)
        {
            EndLight();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (sprite.sprite == baseData.spriteList[6] || sprite.sprite == baseData.spriteList[7])
        {
            sprite.sortingOrder = 10;
        }
        else if (collision.transform.CompareTag("Player"))
        {
            sprite.sortingOrder = transform.localPosition.y > 0 ? -10 : 10;
            particleSystemRenderer.sortingOrder = sprite.sortingOrder - 1;
        }
    }
    public void EndLight()
    {
        GetComponent<Light2D>().intensity = 0;
        isLight = false;
    }
    public void SwitchLight()
    {
        if (isLight)
        {
            GetComponent<Light2D>().intensity = 0;
            isLight = false;
        }
        else
        {
            GetComponent<Light2D>().intensity = baseData.intensity;
            isLight = true;
        }
    }
    public void Aim(Vector3 aimDirection, int type, float aimTime)
    {
        StopFollow();
        if (type == 3)
        {
            sprite.sprite = baseData.spriteList[6];
        }
        else if (type == 5)
        {
            sprite.sprite = baseData.spriteList[7];
        }

        transform.position = transform.parent.transform.position + aimDirection.normalized * baseData.distanceAimTeleport;
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, aimDirection, Vector3.forward));

        if (aimUI == null)
        {
            aimUI = Instantiate(aimUIPrefab, transform.position, Quaternion.identity, canvas.transform);
            aimUI.GetComponent<AimUI>().StartAim(aimTime);
        }
        aimUI.GetComponent<AimUI>().ChangeDirection(aimDirection);
    }
    public void Shoot(Vector3 aimDirection, int type)
    {
        if (type == 3)
        {
            transform.parent = null;
            SetCanBock(true);
            if (CheckBlock())
            {
                HitObject(true);
            }
            else
            {
                timeTravel = TimeTravle();
                StartCoroutine(timeTravel);
                rb.velocity = aimDirection.normalized * baseData.shootVelocity;
            }
        }
        else if (type == 5)
        {
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Travel(aimDirection);
        }
        if (aimUI!= null)
        {
            aimUI.GetComponent<AimUI>().EndAim();
        }

    }
    public void SetCanBock(bool canBlock)
    {
        coll.isTrigger = !canBlock;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            HitObject(true);
            StopCoroutine(timeTravel);
        }
    }
    public void Appear()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, baseData.appearTime);
    }
    public void Disappear()
    {
        transform.DOScale(Vector3.zero, baseData.disappearTime);
        Destroy(gameObject, baseData.disappearTime);
    }
    public bool CheckBlock()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, baseData.whatIsGround);
    }
    public void HitObject(bool hit)
    {
        hitObject = hit;

        rb.velocity = Vector2.zero;
        SetCanBock(false);
        canTeleport = true;
    }
    IEnumerator TimeTravle()
    {
        yield return new WaitForSeconds(baseData.timeTravel);
        HitObject(false);

    }
}
