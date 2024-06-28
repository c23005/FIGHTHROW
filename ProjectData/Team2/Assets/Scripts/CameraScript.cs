using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    Vector3 playPos;
    void Start()
    {
    }


    void Update()
    {
        playPos = player.transform.position;
        transform.position = new Vector3(playPos.x, playPos.y + 0.4f, -10);
    }
}
