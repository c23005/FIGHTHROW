using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoGroundScript : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();
            playerScript.HP -= 0.15f;
        }
    }
}
