using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour
{
    GameObject warning;
    float speed = 1.5f;
    float time;
    SpriteRenderer warningRenderer;
    void Start()
    {
        warning = GetComponent<GameObject>();
        warningRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        warningRenderer.color = WarningColor(warningRenderer.color);
    }

    Color WarningColor(Color color)
    {
        time += Time.deltaTime * speed * 5;
        color.a = Mathf.Sin(time);

        return color;
    }
}
