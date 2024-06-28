using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDamageScript : MonoBehaviour
{
    BoxCollider2D Damage;
    public List<string[]> itemFires = new List<string[]>();
    float Dm;
    [Header("�A�C�e���ԍ�������"),Range(0,8)]public int FireNum;
    float KBP = -7f;
    TextAsset IcsvFires;
    GameObject item;
    Rigidbody2D rigidbody2;
    public string ItemName;
    private void OnValidate()
    {
        switch (FireNum)
        {
            case 1 : ItemName = "�����K"; break;
            case 2: ItemName = "�n���}�["; break;
            case 4: ItemName = "��"; break;
            case 5: ItemName = "���"; break;
            case 6: ItemName = "�X�R�b�v"; break;
            case 7: ItemName = "���j��"; break;
            case 8: ItemName = "��͂�"; break;
            default:ItemName = "�����Ȃ���"; break;
        }

    }
    void Start()
    {
        IcsvFires = Resources.Load("ItemParameter") as TextAsset;
        StringReader reader = new StringReader(IcsvFires.text);
        item = gameObject.transform.parent.gameObject;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            itemFires.Add(line.Split(',')); 
        }
        Dm = Convert.ToSingle(itemFires[FireNum][3]);
        gameObject.transform.parent.name = itemFires[FireNum][4];
        rigidbody2 = item.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            PlayerScript PS = collision.gameObject.GetComponent<PlayerScript>();
            Rigidbody2D rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            if(PS.play == PlayerScript.PLAY.NORMAL)//�v���C���[�����G���łȂ�������_���[�W��^����
            {
                PS.HP = PS.HP - Dm;
                //�m�b�N�o�b�N�̔���
                Vector2 distination = (transform.position - collision.transform.position).normalized;
                rb2.velocity = Vector3.zero;
                rb2.AddForce(distination * KBP, (ForceMode2D)ForceMode.VelocityChange);
                PS.play = PlayerScript.PLAY.INVINCIBLE;//�v���C���[�𖳓G�ɂ���
                item.transform.position = new Vector3(0, -10, 0);
                PS.isdamage = true;
                rigidbody2.velocity = Vector3.zero;
            }
        }
    }

}
