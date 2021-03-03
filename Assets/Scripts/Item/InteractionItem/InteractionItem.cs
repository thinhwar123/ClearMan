using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InteractionItem : MonoBehaviour
{
    public SpriteRenderer sprite { get; private set; }
    [SerializeField] protected ItemData baseData;
    [SerializeField] private CanvasGroup group;
    [SerializeField] private RectTransform line;
    public int count;
    private Tween fadeTween;
    private Tween scaleTween;
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Non_InteractionItem");
        sprite = GetComponent<SpriteRenderer>();
    }
    public virtual void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Non_InteractionItem");

        DataGlobe.instance.PickItem(baseData, count);
        sprite.DOFade(0, 0.2f);
        Destroy(gameObject, 0.3f);

    }
    public void ChooseItem()
    {
        if (fadeTween != null)
        {
            fadeTween.Kill();
        }
        if (scaleTween!= null)
        {
            scaleTween.Kill();
        }
        fadeTween = group.DOFade(1, 0.5f);
        scaleTween = line.DOScaleX(1, 0.5f);
    }
    public void UnChooseItem()
    {
        fadeTween.Kill();
        scaleTween.Kill();
        fadeTween = group.DOFade(0, 0.5f);
        scaleTween = line.DOScaleX(0, 0.5f);
        StopAllCoroutines();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.layer = LayerMask.NameToLayer("InteractionItem");
        }
    }
}
