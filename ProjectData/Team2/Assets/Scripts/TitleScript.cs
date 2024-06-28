using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    float speed = 1f;
    float time;
    AudioSource audioSource;
    public AudioClip start;
    public Image Image;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Invoke("StartButton", 1);
            audioSource.PlayOneShot(start);
        }
        Image.color = TextColor(Image.color);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
    Color TextColor(Color color)
    {
        time += Time.deltaTime * speed * 5;
        color.a = Mathf.Sin(time);

        return color;
    }
}
