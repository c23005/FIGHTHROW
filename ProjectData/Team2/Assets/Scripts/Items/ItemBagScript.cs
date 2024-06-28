using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBagScript : MonoBehaviour
{
    public static List<GameObject> items = new List<GameObject>();
    public int itemNum;
    int nownum;
    public GameObject[] itempos;
    float fallPosx;
    void Start()
    {
        
    }


    void Update()
    {
        itemNum = gameObject.transform.childCount;
        //itemfall();
        if (items.Count != 0 || itemNum != 0)
        {
            //nownum = itemNum;
            itemfall();
        }
        itemNum = gameObject.transform.childCount;
    }
    public void itemfall()
    {
        fallPosx = UnityEngine.Random.Range(-8,8);
        items[0].transform.position = new Vector3(fallPosx,6,0);
        items[0].transform.rotation = Quaternion.identity;
        Rigidbody2D rigidbody2D = items[0].GetComponent<Rigidbody2D>();
        items[0].transform.parent = null;
        items.Remove(items[0]);
        rigidbody2D.velocity = Vector3.zero;
    }
}
