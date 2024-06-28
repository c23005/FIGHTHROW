using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.SceneManagement;

public class PlayerMoveScript : MonoBehaviour
{
    [Header("ジャンプの高さ")]public float jump;
    [Header("持たせられるアイテムの上限")]public int maxNum = 3;
    [Header("子供オブジェクトのGuardを入れる")]public GameObject Shield;
    [Header("子供オブジェクトのKnaif02を入れる")] public GameObject attack;
    BoxCollider2D attackcol;
    GameObject player;
    PlayerScript ps;
    public List<GameObject> list = new List<GameObject>();
    int itemNum = 0;
    int xPosNum;
    [Header("触れるな")]public bool JB;
    bool GuardBool;
    Rigidbody2D rb;
    float px;
    float py;
    public float speed = 20f;
    [Header("アイテムを投げる強さ")]public float ShotSpeed;
    PlayerScript pS;
    public Animator move;
    public float jumpTime;
    public bool land;
    public SpriteRenderer Knife;
    bool attackBool = false;
    float attackTime;
    float coolTime;
    public AudioClip[] playerSEs;
    [Header("何も入っていないオウディオを入れる")]public AudioSource playerSA;
    [Header("Walkの入っているオウディオを入れる")]public AudioSource walkAS;
    bool walk = false;
    int walkint = 0;
    public GameObject landobj;
    public bool itembool = false;
    float throwtime;
    void Start()
    {
        player = gameObject;
        ps = player.GetComponent<PlayerScript>();
        GuardBool = false;
        attack.SetActive(false);
        Shield.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        pS = GetComponent<PlayerScript>();
        attackcol = attack.GetComponent<BoxCollider2D>();
        attackcol.enabled = false;
        speed = 20f;
    }


    void Update()
    {
        px = Input.GetAxisRaw("Horizontal_1");
        py = Input.GetAxisRaw("Vertical_1");
        throwtime += Time.deltaTime;
        coolTime += Time.deltaTime;
        if(ps.play == PlayerScript.PLAY.NORMAL || ps.play == PlayerScript.PLAY.INVINCIBLE)
        {
            GuardAttack();
            JGuard();
        }
        if (Input.GetButtonDown("Jump_1") &&!GuardBool)
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
        if(walk && walkint == 0)
        {
            walkAS.Play();
            walkint = 1;
        }
        if(walk == false || move.GetBool("JumpBool"))
        {
            walkint = 0;
            walkAS.Stop();
        }
        if (move.GetBool("KnockBackBool")) move.SetBool("KnockBackBool", false);
        isDamage();
        if(ps.play == PlayerScript.PLAY.DEAD)
        {
            landobj.SetActive(false);
            move.SetBool("EndBool", true);
        }
        if (Input.GetButtonDown("Fire1_1"))
        {
            if (JB)
            {
                IsJump();
            }
        }
        list.RemoveAll(item => item == null);
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
        // プレイヤーの移動とアニメーション
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
            if(px <=0.3f && px >= -0.3f)
            {
                move.SetBool("WalkBool", false);
                walk = false;
            }
            /*if (JB == true)
            {
                if (Input.GetButtonDown("Fire1_1"))
                {
                    JB = false;
                    move.SetBool("JumpBool", true);
                    Invoke("Jump",jumpTime);
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
        if (Input.GetButton("Fire3_1"))
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
            if (Input.GetButton("Fire2_1"))
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
                    attackcol.enabled = true;
                }
                else
                {
                    move.SetBool("AttackBool", false);
                    attackcol.enabled = false;
                    coolTime = 0;
                }
            }
            else
            {
                move.SetBool("AttackBool", false);
            }
            if (Input.GetButtonUp("Fire2_1"))
            {
                attackcol.enabled = false;
                move.SetBool("AttackBool", false);
                attackBool = false;
                attackTime = 0;
                coolTime = 0;
            }
        }
        
    }

    public void JGuard()
    {
        // ジャストガード(アイテムキャッチ)の判定
        if (itemNum < maxNum)
        {

            GuardScript nowGuard = Shield.GetComponent<GuardScript>();
            if (nowGuard.JG)
            {
                list.Add(nowGuard.item);
                list[itemNum].transform.parent = gameObject.transform;
                list[itemNum].gameObject.SetActive(false);
                list = list.Union(list).ToList();
                itemNum++;
                itembool = true;
            }
        }
    }

    public void Shot()
    {
            // アイテム投げの判定とアニメーション
            throwtime = 0;
            Vector3 itempos = new Vector3(list[0].transform.localPosition.x, list[0].transform.position.y, 0);
            itempos.y = transform.localPosition.y + 1f;
            itempos.x = Shield.transform.position.x + xPosNum * 1.2f;
            list[0].transform.position = itempos;
            list[0].transform.rotation = transform.rotation;
            list[0].SetActive(true);
            Rigidbody2D itemRB = list[0].GetComponent<Rigidbody2D>();
            list[0].transform.parent = null;
            if (px == 0 && py == 0)
            {
                itemRB.AddForce(new Vector2(ShotSpeed * xPosNum, ShotSpeed));
            }
            else
            {
                itemRB.AddForce(new Vector2(ShotSpeed * px * 2, ShotSpeed * py * 2));
            }
            ItemScript itemScript = list[0].GetComponent<ItemScript>();
            if (itemScript.itemSize != ItemScript.ITEM_SIZE.SMALL)
            {
                itemScript.isItem = true;
            }
            //list.Clear();
            itemNum--;
            itembool = false;
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
        if(ps.isdamage && ps.play != PlayerScript.PLAY.DEAD)
        {
            move.SetBool("KnockBackBool", true);
            ps.isdamage = false;
        }
    }
    IEnumerator ShotIE()
    {
        Shot();
        yield return new WaitForSeconds(0.1f);
        //StartCoroutine(ClearList());
        list.Clear();
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
        if(obj != null)
        {
            obj.SetActive(true);
            Debug.Log("a");
            itemNum = 0;
            list.Clear();
        }
        if (list.Count != 0)
        {
            list.RemoveAt(0);
            Debug.Log("!");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            if(move.GetBool("EndBool"))
            {
                move.SetBool("DieBool", true);
                playerSA.PlayOneShot(playerSEs[5]);
            }
        }
    }
}
