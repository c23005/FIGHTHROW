using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStageGimmickScript : MonoBehaviour
{
    [Header("ギミックをおこす頻度")]public float gimmickTime;
    float nowTime = 0;
    [Header("プレイヤー2人を入れる")]
    public GameObject[] players;
    public PlayerScript[] ps;
    public BoxCollider2D[] bx;
    [Header("1pのMoveScriptを入れる")]
    public PlayerMoveScript move1;
    [Header("2pのMoveScriptを入れる")]
    public Player2MoveScript move2;
    [Header("Gimmicksの中に入ってるNormalを入れる")]public PhysicsMaterial2D normal;
    float effectTime;
    public enum GIMMICK
    {
        NORMAL,
        WIND,
        ATTACKUP,
        SPEEDDOWN,
        CAMUP,
    }
    public GIMMICK gimmick;
    [Header("ギミックをおこしている時間")]public float gimmickCount;
    [Header("Windsを入れる")]public GameObject winds;
    bool onStatus = false;
    Camera mainCam;
    [Header("プレイヤー1と2のAttackEffectを入れる")] public GameObject[] atkUp;
    [Header("プレイヤー1と2のDownEffectを入れる")] public GameObject[] Down;
    public AudioClip atkClip;
    AudioSource atkSource;
    public AudioClip downClip;
    void Start()
    {
        mainCam = Camera.main;
        for (int i = 0;i < 2;i++)
        {
            bx[i] = players[i].GetComponent<BoxCollider2D>();
            ps[i] = players[i].GetComponent<PlayerScript>();
        }

        move1 = players[0].GetComponent<PlayerMoveScript>();
        move2 = players[1].GetComponent<Player2MoveScript>();
        gimmick = GIMMICK.NORMAL;
        atkUp[0].SetActive(false);
        atkUp[1].SetActive(false);
        Down[0].SetActive(false);
        Down[1].SetActive(false);
        atkSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        OnTime();
        OnWind();
        OnAttackUp();
        OnMove();
        OnCamUp();
    }
    public void OnTime()
    {
        nowTime += Time.deltaTime;
        if (nowTime > gimmickTime)
        {
            int maxCount = Enum.GetNames(typeof(GIMMICK)).Length;//enum型の数を取得する
            int count = UnityEngine.Random.Range(0, maxCount);//ランダムな数を取得する
            gimmick = (GIMMICK)Enum.ToObject(typeof(GIMMICK), count);//int型をenum型に変換する
            Debug.Log(gimmick);
            nowTime = 0;
        }
        if (gimmick != GIMMICK.NORMAL)
        {
            nowTime = 0;
        }
    }
    public void OnWind()
    {
        if(gimmick == GIMMICK.WIND)
        {
            effectTime += Time.deltaTime;
            winds.SetActive(true);
            if(effectTime >= gimmickCount)
            {
                winds.SetActive(false);
                gimmick = GIMMICK.NORMAL;
                effectTime = 0;
            }
        }
    }
    public void OnAttackUp()
    {
        if(gimmick == GIMMICK.ATTACKUP)
        {
            if(onStatus == false)
            {
                ps[0].attack *= 2;
                ps[1].attack *= 2;
                onStatus = true;
                atkSource.PlayOneShot(atkClip);
            }
            effectTime += Time.deltaTime;
            atkUp[0].SetActive(true);
            atkUp[1].SetActive(true);
            if (effectTime >= gimmickCount)
            {
                ps[0].attack /= 2;
                ps[1].attack /= 2;
                gimmick = GIMMICK.NORMAL;
                effectTime = 0;
                onStatus = false;
                atkUp[0].SetActive(false);
                atkUp[1].SetActive(false);
            }
        }
    }
    public void OnMove()
    {
        if(gimmick == GIMMICK.SPEEDDOWN)
        {
            if(onStatus == false)
            {
                move1.speed /= 1.5f;
                move2.speed /= 1.5f;
                onStatus = true;
                atkSource.PlayOneShot(downClip);
            }
            effectTime += Time.deltaTime;
            Down[0].SetActive (true);
            Down[1].SetActive (true);
            if (effectTime >= gimmickCount)
            {
                move1.speed *= 1.5f;
                move2.speed *= 1.5f;
                gimmick = GIMMICK.NORMAL;
                effectTime = 0;
                onStatus = false;
                Down[0].SetActive(false);
                Down[1].SetActive(false);
            }
        }
    }
    public void OnCamUp()
    {
        if(gimmick == GIMMICK.CAMUP)
        {
            effectTime += Time.deltaTime;
            mainCam.orthographicSize -= 0.005f;
            Vector3 campos = mainCam.transform.position;
            campos.y += 0.0045f;
            mainCam.transform.position = campos;
            if (mainCam.orthographicSize < 3)
            {
                mainCam.orthographicSize = 3;
            }
            if(mainCam.transform.position.y > 1.15f)
            {
                campos.y = 1.15f;
                mainCam.transform.position = campos;
            }
            if(effectTime  >= gimmickCount)
            {
                mainCam.orthographicSize = 5;
                campos.y = 0;
                mainCam.transform.position = campos;
                gimmick = GIMMICK.NORMAL;
                effectTime = 0;
            }
        }
    }
}
