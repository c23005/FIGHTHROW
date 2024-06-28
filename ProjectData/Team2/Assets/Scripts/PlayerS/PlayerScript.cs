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
    [Header("����������")] public float HP;
    public int playerNum;
    public float attack;
    float Dtime;
    public bool isDead = false;
    [Header("���G�ɂȂ鎞��")]public float coolTime;
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

        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            list.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
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
        if (play == PLAY.INVINCIBLE)//���G
        {
            Dtime += Time.deltaTime;
            if(Dtime > coolTime)
            {
                play = PLAY.NORMAL;
                Dtime = 0;
            }
        }
        if(guard.activeSelf)//�K�[�h�����Ă��邩
        {
            isGuard = true;
        }
        else
        {
            isGuard = false;
        }
        if(!OnCamera)//�J�����O�ɏo���班���Â_���[�W���󂯂�
        {
            HP -= 0.3f;
        }
    }
}
