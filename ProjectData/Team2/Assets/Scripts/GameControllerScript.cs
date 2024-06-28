using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public GameObject[] Players;
    public PlayerScript[] pS;
    public static string winner;
    public static bool end;
    public float startTime;
    public float endTime;
    public float nowTime;
    [Header("BGM‚ª“ü‚Á‚Ä‚¢‚éAudioSource‚ð“ü‚ê‚é")]public AudioSource BGM;
    void Start()
    {
        Application.targetFrameRate = 60;
        end = false;
        nowTime = startTime;
    }


    void Update()
    {
        nowTime -= Time.deltaTime;
        if(nowTime < 0)
        {
            pS[0].play = PlayerScript.PLAY.NORMAL;
            pS[1].play = PlayerScript.PLAY.NORMAL;
            nowTime = endTime;
        }
        if (pS[0].isDead && end == false)
        {
            winner = Players[1].name;
            PlayerPrefs.GetString("WINNER", winner);
            PlayerPrefs.Save();
            end = true;
        }
        else if (pS[1].isDead && end == false)
        {
            winner = Players[0].name;
            PlayerPrefs.GetString("WINNER", winner);
            PlayerPrefs.Save();
            end = true;
        }
        if(end)
        {
            BGM.Stop();
            Invoke("OnResultScene", 2f);
        }
    }
    void OnResultScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
