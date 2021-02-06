using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
public class SoulwardObject : MonoBehaviour
{
    [Header("FollowConfig")]
    [SerializeField] private ParticleSystemRenderer particleSystemRenderer;

    [SerializeField] private float movementSpeed;
    [SerializeField] public float distance;
    [SerializeField] private Ease easeX;
    [SerializeField] private Ease easeY;
    private Tween tweenX;
    private Sequence tweenY;
    private Tween tweenZ;
    [Header("ChangeTypeConfig")]
    [SerializeField] private GameObject temp;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private List<Sprite> listSprite;
    [SerializeField] private List<Color> listColor;
    [Header("TeleportConfig")]
    [SerializeField] private LayerMask whatIsBlockObject;
    [SerializeField] private bool isBlock;
    [SerializeField] private bool canBeBlock;
    [Header("ArrowConfig")]
    [SerializeField] private GameObject bullet;
    private Tween tweenShoot;
    [Header("Stats")]
    [SerializeField] private Transform soulward;


    public void Start()
    {
        soulward = transform.parent.transform;
        transform.localPosition = Vector3.right * distance;
        Follow();
    }
    public void Update()
    {
        BlockObject();
    }
    public void Follow()
    {
        tweenZ = soulward.GetComponent<Soulward>().tweenZ;
        if (!tweenZ.IsPlaying())
        {
            tweenZ.Play();
        }
        tweenX = transform.DOLocalMoveX(-distance, 1 / movementSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(easeX);
        tweenY = DOTween.Sequence();
        tweenY.Append(transform.DOLocalMoveY(0.3f, 0.5f / movementSpeed).SetLoops(2, LoopType.Yoyo).SetEase(easeY)).Append(transform.DOLocalMoveY(-0.3f, 0.5f / movementSpeed).SetLoops(2, LoopType.Yoyo).SetEase(easeY));
        tweenY.SetLoops(-1, LoopType.Restart);
        tweenY.Play();
    }
    public void StopFollow()
    {
        tweenX.Kill();
        tweenY.Kill();
        tweenZ.Pause();
    }
    public void BlockObject()
    {
        isBlock = Physics2D.OverlapCircle(transform.position, 0.05f, whatIsBlockObject);
        if (canBeBlock && isBlock)
        {
            StartCoroutine(HitObject());
        }
    }
    public void Aim(Vector3 aimDirection, float distance, int type)
    {       
        StopFollow();
        if (type == 3)
        {
            sprite.sprite = listSprite[6];
        }
        else if (type == 5)
        {
            sprite.sprite = listSprite[7];
        }

        transform.position = soulward.transform.position + aimDirection.normalized * distance;
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, aimDirection, Vector3.forward));
    }
    public void Shoot(Vector3 aimDirection, float maxDistance, float speed, int type)
    {
        if (type == 3)
        {
            transform.parent = null;

            canBeBlock = true;
            tweenShoot = transform.DOMove(aimDirection.normalized * maxDistance + soulward.position, maxDistance / speed);
        }
        else if (type == 5)
        {
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().Travel(aimDirection, speed, speed);
        }

    }
    public void ResetPosition(float time,int type)
    {
        soulward.GetComponent<Soulward>().Follow(); // reset vi tri soulwar o tam
        canBeBlock = false;
        GetComponent<CircleCollider2D>().isTrigger = true;
        transform.parent = soulward;
        transform.DOLocalMove(Vector3.right * distance, 0.1f).OnComplete(Follow);
        if (type == 3)
        {
            Debug.Log(type);
            sprite.sprite = listSprite[3];
        }
        else if (type == 5)
        {
            sprite.sprite = listSprite[5];
        }
    }
    public void Appear(float time , int type)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, time);
        if (type == 3)
        {
            sprite.sprite = listSprite[3];
        }
        else if (type == 5)
        {
            sprite.sprite = listSprite[5];
        }
    }
    public void Disappear(float time)
    {
        transform.DOScale(Vector3.zero, time);
        Destroy(gameObject, time);
    }
    public void ChangeType(int type)
    {
        sprite.sprite = listSprite[type];
        ParticleSystem.MainModule main = temp.GetComponent<ParticleSystem>().main;
        main.startColor = listColor[type];
        if (type != 2)
        {
            Light(false, 0);
        }
    }
    public void Light(bool isLight, float intensity)
    {
        if (isLight)
        {
            GetComponent<Light2D>().intensity = intensity;
        }
        else
        {
            GetComponent<Light2D>().intensity = 0;
        }
    }
    public IEnumerator HitObject()
    {
        canBeBlock = false;
        GetComponent<CircleCollider2D>().isTrigger = false;
        tweenShoot.Kill();
        soulward.GetComponent<Soulward>().target.GetComponent<TeleportAbility>().HitBlockObject();
        yield return new WaitForSeconds(0.1f);
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (sprite.sprite == listSprite[6] || sprite.sprite == listSprite[7])
        {
            sprite.sortingOrder = 10;
        }
        else if (collision.transform.CompareTag("Player"))
        {
            sprite.sortingOrder = transform.localPosition.y > 0 ? -10 : 10;
            GetComponent<TrailRenderer>().sortingOrder = sprite.sortingOrder - 1;
            particleSystemRenderer.sortingOrder = sprite.sortingOrder - 1;
        }
    }

}
