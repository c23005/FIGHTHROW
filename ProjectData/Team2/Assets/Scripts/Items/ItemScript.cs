using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject Damage;
    public ItemDamageScript damageScript;
    SpriteRenderer sr;
    Sprite sp;
    public float xSize;
    public float ySize;
    public enum ITEM_SIZE//ƒAƒCƒeƒ€‚Ì‘å‚«‚³
    {
        SMALL,
        NORMAL,
        BIG
    }
    [Header("item‚Ì‘å‚«‚³‚ð‚±‚±‚É“ü‚ê‚é")] public ITEM_SIZE itemSize;
    public bool isItem = false;

    private void OnEnable()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        damageScript = GetComponentInChildren<ItemDamageScript>();
        sp = GetComponent<Sprite>();
        sr = GetComponent<SpriteRenderer>();
        sp = Resources.Load<Sprite>("Images/" + damageScript.itemFires[damageScript.FireNum][2]);
        sr.sprite = sp;
    }


    void Update()
    {
        Vector3 t = this.gameObject.transform.position;
        t.z = 0f;
        transform.position = t;
        if( body.IsSleeping())
        {
            Damage.SetActive(false);
        }
        else
        {
            Damage.SetActive(true);
        }
        transform.localScale = new Vector3(xSize, ySize, 1);
        if (isItem)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, -5));
        }
        if(transform.position.y <= -10)
        {
            GameObject itemBag = GameObject.Find("ItemBag");
            this.gameObject.transform.parent = itemBag.transform;
            ItemBagScript.items.Add(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            isItem = false;
        }
        if(collision.gameObject.tag == "Wool")
        {
            transform.position = new Vector3(0, -10, 0);
            isItem = false;
        }
    }
}
