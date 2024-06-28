using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMeterScript : MonoBehaviour
{
    Image hpMeter;
    public PlayerScript playerScript;
    public RectTransform hpMeterRect;
    public float meterPosX;
    public float tes;
    void Start()
    {
        hpMeter = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        hpMeterRect.sizeDelta = new Vector2(400 - (playerScript.HP * 2), 64);
        hpMeterRect.localPosition = new Vector3(meterPosX - (playerScript.HP) * tes, 280, 0);
    }
}
