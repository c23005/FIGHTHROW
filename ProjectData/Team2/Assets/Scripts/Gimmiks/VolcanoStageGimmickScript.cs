using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoStageGimmickScript : MonoBehaviour
{
    [Header("ギミックをおこす頻度")]public float gimmickTime;
    float nowTime = 0;
    public GameObject[] players;
    public PlayerScript[] ps;
    public BoxCollider2D[] bx;
    public PlayerMoveScript move1;
    public Player2MoveScript move2;
    [Header("Gimmicksの中に入ってるNormalを入れる")]public PhysicsMaterial2D normal;
    float effectTime;
    public enum GIMMICK
    {
        NORMAL,
        ATTACKUP,
        SPEEDDOWN,
        CAMUP,
        ROCK,
    }
    public GIMMICK gimmick;
    [Header("ギミックをおこしている時間")]public float gimmickCount;
    bool onStatus = false;
    Camera mainCam;
    public GameObject rock;
    RockScript rockScript;
    public GameObject warning;
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
        rockScript = rock.GetComponent<RockScript>();
        rock.SetActive(false);
        warning.SetActive(false);
        atkUp[0].SetActive(false);
        atkUp[1].SetActive(false);
        Down[0].SetActive(false);
        Down[1].SetActive(false);
        atkSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        OnTime();
        OnAttackUp();
        OnMove();
        OnCamUp();
        OnRock();
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
                atkUp[0].SetActive (false);
                atkUp[1].SetActive (false);
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
            Down[0].SetActive(true);
            Down[1].SetActive(true);
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
    public void OnRock()
    {
        if(gimmick == GIMMICK.ROCK)
        {
            warning.SetActive(true);
            rock.SetActive(true);
            if (rockScript.falling == false)
            {
                warning.SetActive(false);
                effectTime += Time.deltaTime;
            }
            if(effectTime >= gimmickCount)
            {
                effectTime = 0;
                rock.SetActive(false);
                gimmick = GIMMICK.NORMAL;
            }
        }
    }
}
