using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InOutScript : MonoBehaviour
{
    [Header("�Ώۂ̃v���C���[������")]
    public Transform target;
    [Header("���C���J����������")]
    public Camera targetCamera;
        
    public Image icon;
    public RawImage iconImage;
    public bool damage;
    Rect rect = new Rect(0,0,1,1);
    public static Rect canvasRect;
    public PlayerScript playerScript;
    void Start()
    {
        canvasRect = ((RectTransform)icon.canvas.transform).rect;
        canvasRect.Set(
            canvasRect.x + icon.rectTransform.rect.width * 0.5f,
            canvasRect.y + icon.rectTransform.rect.height * 0.5f,
            canvasRect.width - icon.rectTransform.rect.width,
            canvasRect.height - icon.rectTransform.rect.height);
        playerScript = target.GetComponent<PlayerScript>();
    }


    void Update()
    {
        //�J������viewportPoint���擾����
        var viewPort = targetCamera.WorldToViewportPoint(target.position);
        if(rect.Contains(viewPort))
        {
            //�A�C�R����\�����Ȃ�
            icon.enabled = false;
            iconImage.enabled = false;
            playerScript.OnCamera = true;
        }
        else
        {
            playerScript.OnCamera=false;
            //�Ώۂ�\������
            icon.enabled = true;
            iconImage.enabled = true;
            //��ʓ��őΏۂ�ǐՂ���
            viewPort.x = Mathf.Clamp01(viewPort.x);
            viewPort.y = Mathf.Clamp01(viewPort.y);
            icon.rectTransform.anchoredPosition = Rect.NormalizedToPoint(canvasRect, viewPort);
        }
    }
}
