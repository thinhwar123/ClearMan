using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : InteractionItem
{
    [SerializeField] private bool opened;
    public void Start()
    {
        CheckIfBeenOpened();
    }
    public override void Interact()
    {
        if (!opened)
        {
            gameObject.layer = LayerMask.NameToLayer("Non_InteractionItem");
            GetComponent<Animator>().SetTrigger("open");

            foreach (ItemInChess itemInChess in ((ChestItemData)baseData).itemInChessList)
            {
                for (int i = 0; i < itemInChess.count; i++)
                {                   
                    GameObject temp = Instantiate(itemInChess.itemPrefab, transform.position, Quaternion.identity);
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), 1).normalized * 5f;
                }
            }
        }
    }
    public void AnimationFinish()
    {
        opened = true;
        GetComponent<Animator>().SetBool("opened", true);
    }
    public void CheckIfBeenOpened()
    {
        if (opened)
        {
            gameObject.layer = LayerMask.NameToLayer("Non_InteractionItem");
            GetComponent<Animator>().SetBool("opened", true);
        }        
    }
}
