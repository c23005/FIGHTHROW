using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScript : MonoBehaviour
{
    PlayerMoveScript PlayerMoveScript;
    void Start()
    {
        PlayerMoveScript = gameObject.GetComponentInParent<PlayerMoveScript>();
    }


    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item"|| collision.gameObject.tag == "player")
        {
            if (PlayerMoveScript.move.GetBool("JumpBool"))
            {
                PlayerMoveScript.move.SetBool("LandBool", true);
                PlayerMoveScript.land = true;
                PlayerMoveScript.move.SetBool("JumpBool", false);
            }
            PlayerMoveScript.JB = true;
        }
    }
}
