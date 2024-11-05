using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ribbon : MonoBehaviour
{
    public void RibbonCickHandler()
    {
        //GetComponent<RectTransform>().localScale= new Vector2(1.5f, 1.5f);
        Envelope.anim.Play("Envelope_open");
        gameObject.SetActive(false);
    }

    void Start()
    {
        // 투명배경 무시
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
    }
    void Update()
    {

    }
}
