using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    public bool JG;
    float GTime;
    public float JGTime;
    public GameObject item;
    public GameObject player;
    bool player1;
    bool player2;
    private void OnValidate()
    {
        if (player.name == "player1")
        {
            player1 = true;
        }
        else if (player.name == "player2")
        {
            player2 = true;
        }
    }
    private void Awake()
    {
        if (player.name == "player1")
        {
            player1 = true;
        }
        else if (player.name == "player2")
        {
            player2 = true;
        }
    }
    public void OnEnable()
    {
        GTime = 0;
    }
    void Start()
    {
    }


    void Update()
    {
        GTime += Time.deltaTime;
        JG = false;
    }

    private void OnDisable()
    {
        JG = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (GTime < JGTime)
            {
                if(player1)
                {
                    PlayerMoveScript playerMove = GetComponentInParent<PlayerMoveScript>();
                    if(playerMove.itembool ==  false)
                    {
                        JG = true;
                        item = collision.gameObject;
                    }
                }
                if (player2)
                {
                    Player2MoveScript player2Move = GetComponentInParent<Player2MoveScript>();
                    if(player2Move.itembool == false)
                    {
                        JG = true;
                        item = collision.gameObject;
                    }
                }
            }
        }
    }
}
