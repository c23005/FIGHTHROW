using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject RetryImage;
    bool Retry;
    public string winner;
    public Text winTxt;
    public AudioClip[] tap;
    public AudioSource tapSource;
    int tapnum;
    public GameObject[] winimg;
    void Start()
    {
        RetryImage.SetActive(false);
        Retry = false;
        winner = PlayerPrefs.GetString("WINNER", GameControllerScript.winner);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if(!Retry)
            {
                tapSource.PlayOneShot(tap[0]);
            }
            RetryImage.SetActive(true);
            Invoke("ChengeBool", 0.2f);
        }
        NextStage();
        if (winner == "")
        {
            winTxt.text = "à¯Ç´ï™ÇØ";
        }
        else
        {
            winTxt.text = winner + "ÇÃèüÇø";
        }
        if (winner == "player1")
        {
            winimg[0].SetActive(true);
            winimg[1].SetActive(false);
        }
        else if (winner == "player2")
        {
            winimg[1].SetActive(true);
            winimg[0].SetActive(false);
        }
    }

    public void NextStage()
    {
        if (Retry == true)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                tapSource.PlayOneShot(tap[1]);
                tapnum = 1;
                StartCoroutine(LoadScene());
            }
            if (Input.GetButtonDown("Fire1_1") || Input.GetButtonDown("Fire1_2"))
            {
                tapSource.PlayOneShot(tap[2]);
                tapnum = 2;
                StartCoroutine(LoadScene());
            }
        }
    }

    public void ChengeBool()
    {
        Retry = true;
    }
    IEnumerator LoadScene()
    {
            yield return new WaitForSeconds(1f);
            if (tapnum == 1)
            {
                SceneManager.LoadScene("StageSelectScene");
            }
            else
            {
                SceneManager.LoadScene("TitleScene");
            }
    }

}
