using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Image[] UIs;
    public RectTransform[] UITrans;
    void Start()
    {
        for (int i = 0; i < UIs.Length; i++)
        {
            //UITrans[i] = UIs[i].GetComponent<RectTransform>();
            var corners = new Vector3[4];
            UITrans[i].GetWorldCorners(corners); // UI‚Ì4‚Â‚ÌŠp‚ðŽæ“¾
        }
    }


    void Update()
    {
        var player1ScreenPos = Camera.main.WorldToScreenPoint(player1.transform.position);
        var player2ScreenPos = Camera.main.WorldToScreenPoint(player2.transform.position);
    }
}
