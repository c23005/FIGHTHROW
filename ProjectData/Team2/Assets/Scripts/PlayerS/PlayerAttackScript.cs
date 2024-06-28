using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    BoxCollider2D attack;
    public PlayerScript playerScript;
    float damage;
    public float KnockBackPower;
    public bool isGuard;
    public AudioSource guardAS;
    public AudioClip guardClip;
    void Start()
    {
        attack = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        damage = playerScript.attack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            GameObject enemy = collision.gameObject;
            PlayerScript PS = enemy.gameObject.GetComponent<PlayerScript>();
            Rigidbody2D rb2 = enemy.GetComponent<Rigidbody2D>();
            if (PS.play == PlayerScript.PLAY.NORMAL && !PS.isGuard)
            {
                PS.HP = PS.HP - damage;
                rb2.velocity = Vector3.zero;
                Vector2 distination = (transform.position - collision.transform.position).normalized;
                rb2.AddForce(new Vector3(distination.x,0,0) * KnockBackPower, (ForceMode2D)ForceMode.VelocityChange);
                PS.play = PlayerScript.PLAY.INVINCIBLE;
                PS.isdamage = true;
            }
            if(PS.isGuard)
            {
                guardAS.PlayOneShot(guardClip);
            }
        }
    }

}
