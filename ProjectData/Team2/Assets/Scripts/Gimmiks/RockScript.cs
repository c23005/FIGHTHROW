using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public float startPosy;
    public float endPosy;
    public float yspeed;
    public bool falling = false;
    public float falltime;
    float nowtime;
    public ParticleSystem fallEff;
    private void OnEnable()
    {
        transform.position = new Vector3(0,startPosy,0);
        nowtime = 0;
    }
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 olPos = transform.position;
        nowtime += Time.deltaTime;
        if(nowtime >= falltime && falling)
        {
            Debug.Log("a");
        }
        if(transform.position.y >= endPosy)
        {
            transform.Translate(0, yspeed * 5, 0);
        }
        if(olPos != transform.position)
        {
            falling = true;
            fallEff.Play();
        }
        else
        {
            falling= false;
            fallEff.Stop();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            if (falling)
            {
                PlayerScript playerScript = collision.gameObject.GetComponent<PlayerScript>();
                
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerScript.play != PlayerScript.PLAY.INVINCIBLE)
                {
                    playerScript.isdamage = true;
                    playerScript.HP -= 100;
                    Vector2 distination = (transform.position - collision.transform.position).normalized;
                    rb.AddForce(distination * -10, (ForceMode2D)ForceMode.VelocityChange);
                    playerScript.play = PlayerScript.PLAY.INVINCIBLE;
                }
            }
        }
    }
}
