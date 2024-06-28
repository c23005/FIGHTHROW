using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectScript : MonoBehaviour
{
    [Header("ここにステージ1～3のボタンを入れる")] public Button[] stageButton;
    int nowNum;
    bool hol;
    AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        nowNum = 1;
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        float px = Input.GetAxisRaw("Horizontal");
        float py = Input.GetAxisRaw("Vertical");
    }

    public void ButtonClick(int number)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(LoadScene(number));//コルーチンを呼び出している
    }
    IEnumerator LoadScene(int number)
    {
        yield return new WaitForSeconds(1);//処理を()秒停止させて下のプログラムを動かす
        nowNum = number;
        SceneManager.LoadScene("Stage" + (nowNum));
    }
}
