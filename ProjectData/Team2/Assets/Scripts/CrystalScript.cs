using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    float nowTime;
    [Header("‚¢‚ÂÁ‚¦‚é‚©Œˆ‚ß‚é")]public float desTime;
    void Start()
    {
        
    }


    void Update()
    {
        nowTime += Time.deltaTime;
        transform.Translate(0,-0.25f,0);
        if(nowTime >= desTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            PlayerScript PS = collision.gameObject.GetComponent<PlayerScript>();
            PS.HP -= 5;
            Destroy(gameObject);
        }
    }
}
