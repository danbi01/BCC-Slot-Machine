using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ribbon : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject candle;
    public GameObject lightEffect;
    public GameObject cakeTopper;
    AudioSource envelopeSound;

    public static bool isRibbonClicked;

    int rank = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter ribbon");
        GetComponent<RectTransform>().localScale = new Vector2(1.02f, 1.02f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit ribbon");
        GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
    }

    public void RibbonClickHandler()
    {
        isRibbonClicked = true;
        Debug.Log("isRibbonClicked: "+isRibbonClicked);
        Envelope.anim.Play("Envelope_open");
        envelopeSound.Play(0);

        // 리본 클릭했을 때 촛불, 효과 실행
        StartCoroutine(setTimeOutClickRibbon());

        GetComponent<Image>().enabled= false;
    }

    IEnumerator setTimeOutClickRibbon()
    {
        yield return new WaitForSeconds(0.5f); // TODO: 편지지 넣은 후에 수정하기
        candle.SetActive(true);
        lightEffect.SetActive(true);
        Debug.Log("Play candleLight");
        try
        {
            if (GameManager.score == 100)
            {
                rank = 1;
                CandleLight.rectTransform.anchoredPosition = new Vector2(138f, 131f);
                LightEffect.rectTransform.anchoredPosition = new Vector2(138f, 103f);
            }
            else if (GameManager.score > 85)
            {
                rank = 2;
                CandleLight.rectTransform.anchoredPosition = new Vector2(401.7f, 8.4f);
                LightEffect.rectTransform.anchoredPosition = new Vector2(401.7f, -19.96f);
            }
            else if (GameManager.score > 75)
            {
                rank = 3;
                CandleLight.rectTransform.anchoredPosition = new Vector2(624.3f, -67.8f);
                LightEffect.rectTransform.anchoredPosition = new Vector2(624.3f, -100f);
            }
            else
            {
                rank = 4;
                CandleLight.rectTransform.anchoredPosition = new Vector2(825.7f, -166f);
                LightEffect.rectTransform.anchoredPosition = new Vector2(825.7f, -194f);
                cakeTopper.SetActive(true);
            }
        } catch
        {
            StartCoroutine(setTimeOutClickRibbon());
        }
    }

    void Start()
    {
        // 투명부분 무시
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
        envelopeSound = GetComponent<AudioSource>();

        isRibbonClicked = false;

        candle.SetActive(false);
        cakeTopper.SetActive(false);
        lightEffect.SetActive(false);
    }

}
