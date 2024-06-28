using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsScript : MonoBehaviour
{
    public GameObject item;
    ItemDamageScript itemDamageScript;
    public int itemnum;
    void Start()
    {
        itemDamageScript = item.GetComponentInChildren<ItemDamageScript>();
    }


    void Update()
    {
        
    }
}
