using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    List<string[]> list = new List<string[]>();
    GameObject player;
    TextAsset csvFire;
    [Header("何も入れるな")] public float HP;
    public int playerNum;
    public float attack;
    float Dtime;
    public bool isDead = false;
    [Header("無敵になる時間")]public float coolTime;
    public bool isGuard;
    public GameObject guard;
    Rect rect = new Rect(0,0,1,1);
    public bool isdamage = false;
    public enum PLAY
    {
        START,
        NORMAL,
        DEAD,
        INVINCIBLE,
    }
    public PLAY play;
    public bool OnCamera = true;
    BoxCollider2D box;
    void Start()
    {
        player = gameObject;
        csvFire = Resources.Load("PlayerParameter") as TextAsset;
        StringReader reader = new StringReader(csvFire.text);

        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            list.Add(line.Split(',')); // , 区切りでリストに追加
        }

        HP = Convert.ToSingle(list[playerNum][1]);
        attack = Convert.ToSingle(list[playerNum][2]);
        player.name = list[playerNum][0];
        play = PLAY.START;
        box = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (HP <= 0)
        {
            isDead = true;
            HP = 0;
            play = PLAY.DEAD;
        }
        if (play == PLAY.INVINCIBLE)//無敵
        {
            Dtime += Time.deltaTime;
            if(Dtime > coolTime)
            {
                play = PLAY.NORMAL;
                Dtime = 0;
            }
        }
        if(guard.activeSelf)//ガードをしているか
        {
            isGuard = true;
        }
        else
        {
            isGuard = false;
        }
        if(!OnCamera)//カメラ外に出たら少しづつダメージを受ける
        {
            HP -= 0.3f;
        }
    }
}
