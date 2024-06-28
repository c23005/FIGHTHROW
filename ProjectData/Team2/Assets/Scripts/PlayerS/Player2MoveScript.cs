using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.SceneManagement;

public class Player2MoveScript : MonoBehaviour
{
    [Header("ジャンプの高さ")] public float jump;
    [Header("持たせられるアイテムの上限")] public int maxNum ;
    [Header("子供オブジェクトのGuardを入れる")] public GameObject Shield;
    [Header("子供オブジェクトのKnaif02を入れる")] public GameObject attack;
    BoxCollider2D attackBox;
    GameObject player;
    PlayerScript ps;
    List<GameObject> list2 = new List<GameObject>();
    int itemNum = 0;
    int xPosNum;
    [Header("触れるな")] public bool JB;
    bool GuardBool;
    Rigidbody2D rb;
    float px;
    float py;
    public float speed;
    [Header("アイテムを投げる強さ")] public float ShotSpeed;
    PlayerScript pS;
    public Animator move;
    public float jumpTime;
    public bool land;
    public SpriteRenderer Knife;
    bool attackBool = false;
    float attackTime;
    public AudioClip[] playerSEs;
    [Header("何も入っていないオウディオを入れる")] public AudioSource playerSA;
    [Header("Walkの入っているオウディオを入れる")] public AudioSource walkAS;
    bool walk = false;
    int walkint = 0;
    float hp;
    public GameObject landobj;
    public bool itembool = false;
    float throwtime;
    float coolTime;
    void Start()
    {
        player = gameObject;
        ps = player.GetComponent<PlayerScript>();
        GuardBool = false;
        attack.SetActive(false);
        Shield.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        pS = GetComponent<PlayerScript>();
        attackBox = attack.GetComponent<BoxCollider2D>();
        attackBox.enabled = false;
        hp = ps.HP;
        speed = 25f;
    }


    void Update()
    {
        //プレイヤーが死んでいなければ動く
        throwtime += Time.deltaTime;
        coolTime += Time.deltaTime;
        px = Input.GetAxisRaw("Horizontal_2");
        py = Input.GetAxisRaw("Vertical_2");
        if (ps.play == PlayerScript.PLAY.NORMAL || ps.play == PlayerScript.PLAY.INVINCIBLE)
        {
            if(itemNum < maxNum)
            {
                JGuard();
            }
            GuardAttack();
        }
        if (Input.GetButtonDown("Jump_2") &&!GuardBool)
        {
            move.SetBool("ThrowBool", true);
            playerSA.PlayOneShot(playerSEs[4]);
            //Invoke("Shot", 0.1f);
            StartCoroutine(ShotIE());
        }
        if (itemNum < 0)
        {
            itemNum = 0;
        }
        if (itemNum >= maxNum)
        {
            itemNum = maxNum;
        }
        if (move.GetBool("ThrowBool"))
        {
            Invoke("Throw", 0.1f);
        }
        if (walk && walkint == 0)
        {
            walkAS.Play();
            walkint = 1;
        }
        if (walk == false)
        {
            walkint = 0;
            walkAS.Stop();
        }
        if (move.GetBool("KnockBackBool")) move.SetBool("KnockBackBool", false);
        isDamage();
        if(hp <= 0 && ps.play == PlayerScript.PLAY.DEAD)
        {
            move.SetBool("EndBool", true);
            landobj.SetActive(false);
        }
        if (ps.play == PlayerScript.PLAY.DEAD)
        {
            landobj.SetActive(false);
            move.SetBool("EndBool", true);
        }
        if (Input.GetButtonDown("Fire1_2"))
        {
            if (JB)
            {
                IsJump();
            }
        }
        list2.RemoveAll(item => item == null);
    }
    private void FixedUpdate()
    {
        Move();
        if (land == true)
        {
            Invoke("Idol", 0.02f);
        }
    }

    public void Move()
    {
        // プレイヤーの移動とジャンプの調整とアニメーション
        if (GuardBool == false && ps.play != PlayerScript.PLAY.DEAD)
        {
            rb.AddForce(new Vector3(speed * px, 0, 0));
            if (px < -0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                xPosNum = -1;
                move.SetBool("WalkBool", true);
                walk = true;
            }
            else if (px > 0.1f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                xPosNum = 1;
                move.SetBool("WalkBool", true);
                walk = true;
            }
            if (px <= 0.3f && px >= -0.3f)
            {
                move.SetBool("WalkBool", false);
                walk = false;
            }
            /*if (JB == true)
            {
                if (Input.GetButtonDown("Fire1_2"))
                {
                    JB = false;
                    move.SetBool("JumpBool", true);
                    Invoke("Jump", jumpTime);
                    //playerSA.PlayOneShot(playerSEs[1]);
                }
            }*/
        }
    }
    void IsJump()
    {
        JB = false;
        move.SetBool("JumpBool", true);
        Invoke("Jump", jumpTime);
    }

    public void GuardAttack()
    {
        //攻撃とガードの判定とアニメーション
        if (Input.GetButton("Fire3_2"))
        {
            if(throwtime >= 0.3f)
            {
                Shield.SetActive(true);
                GuardBool = true;
                attack.SetActive(false);
                move.SetBool("GuardBool", true);
            }
        }
        else
        {
            Shield.SetActive(false);
            GuardBool = false;
            attack.SetActive(true);
            move.SetBool("GuardBool", false);
        }

        if (GuardBool == false)
        {
            if (Input.GetButton("Fire2_2"))
            {
                attackTime += Time.deltaTime;
                if (attackBool == false && coolTime >= 0.15f)
                {
                    move.SetBool("AttackBool", true);
                    attackBool = true;
                    playerSA.PlayOneShot(playerSEs[3],0.2f);
                }
                if (attackTime <= 0.2f)
                {
                    attackBox.enabled = true;
                }
                else
                {
                    move.SetBool("AttackBool", false);
                    attackBox.enabled = false;
                    coolTime = 0;
                }
            }
            else
            {
                move.SetBool("AttackBool", false);
            }
            if (Input.GetButtonUp("Fire2_2"))
            {
                attackBox.enabled = false;
                move.SetBool("AttackBool", false);
                attackBool = false;
                attackTime = 0;
                coolTime = 0;
            }
        }

    }

    public void JGuard()
    {
        // ジャストガード(アイテムキャッチ)判定
        GuardScript nowGuard = Shield.GetComponent<GuardScript>();
        if (nowGuard.JG)
        {
            list2.Add(nowGuard.item);
            nowGuard.item = null;
            list2[itemNum].transform.parent = gameObject.transform;
            list2[itemNum].gameObject.SetActive(false);
            //list = list.Union(list).ToList();
            itemNum++;
            itembool = true;
        }
    }

    public void Shot()
    {
        // アイテム投げの判定とアニメーション
        Vector3 itempos = new Vector3(list2[0].transform.localPosition.x, list2[0].transform.localPosition.y, 0);
        itempos.y = transform.localPosition.y + 1;
        itempos.x = Shield.transform.position.x + xPosNum * 1.2f;
        list2[0].transform.position = itempos;
        list2[0].transform.rotation = transform.rotation;
        list2[0].SetActive(true);
        Rigidbody2D itemRB = list2[0].GetComponent<Rigidbody2D>();
        list2[0].transform.parent = null;
        if (px == 0 && py == 0)
        {
            itemRB.AddForce(new Vector2(ShotSpeed * xPosNum, ShotSpeed));
        }
        else
        {
            itemRB.AddForce(new Vector2(ShotSpeed * px * 2, ShotSpeed * py * 2));
        }
        ItemScript itemScript = list2[0].GetComponent<ItemScript>();
        if (itemScript.itemSize != ItemScript.ITEM_SIZE.SMALL)
        {
            itemScript.isItem = true;
        }
        //list.RemoveAt(0);
        //list.Clear();
        itemNum--;
        itembool = false;
        throwtime = 0;
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jump);
    }
    public void Idol()
    {
        move.SetBool("LandBool", false);
        land = false;
        playerSA.PlayOneShot(playerSEs[2]);
    }
    public void Throw()
    {
        move.SetBool("ThrowBool", false);
    }
    void isDamage()
    {
        if (ps.isdamage)
        {
            move.SetBool("KnockBackBool", true);
            ps.isdamage = false;
        }
    }
    IEnumerator ShotIE()
    {
        Shot();
        yield return new WaitForSeconds(0.1f);
        list2.Clear();
        StartCoroutine(ItemOBJ());
    }
    IEnumerator ItemOBJ()
    {
        yield return new WaitForSeconds(0.1f);
        Transform children = gameObject.GetComponentInChildren<Transform>();
        GameObject obj = null;
        foreach (Transform ob in children)
        {
            if (ob.tag == "Item")
            {
                ob.transform.parent = null;
                ob.transform.position = new Vector3(0, -10, 0);
                obj = ob.gameObject;
            }
        }
        if (obj != null)
        {
            obj.SetActive(true);
            Debug.Log("a");
            itemNum = 0;
            list2.Clear();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            if (move.GetBool("EndBool"))
            {
                move.SetBool("DieBool", true);
                playerSA.PlayOneShot(playerSEs[5]);
            }
        }
    }
}
