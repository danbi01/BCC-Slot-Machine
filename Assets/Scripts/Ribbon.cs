using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ribbon : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public Material mat;
    public GameObject lightEffect;
    public GameObject candleLight;
    public GameObject cakeTopper;
    public GameObject letterPaper;
    public GameObject envelope;
    public GameObject envelopeTop;
    public GameObject[] cakes = new GameObject[4];
    AudioSource envelopeSound;
    AudioSource checkSound;
    RectTransform rectTransform;
    
    public static bool isRibbonClicked;
    public static bool isEnvelopeOpened;
    public static bool isLetterPaperMoved;
    public static bool isScoreDisplayed;

    int rank = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter ribbon");
        rectTransform.localScale = new Vector2(1.02f, 1.02f);
        checkSound.Play(0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit ribbon");
        rectTransform.localScale = new Vector2(1f, 1f);
    }

    public void RibbonClickHandler()
    {
        isRibbonClicked = true;
        Debug.Log("isRibbonClicked: "+isRibbonClicked);
        envelope.GetComponent<Animator>().Play("Envelope_open");
        envelopeSound.Play(0);

        // 리본 클릭했을 때 촛불, 효과 실행
        StartCoroutine(setTimeOutClickRibbon());

        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
    }

    IEnumerator setTimeOutClickRibbon()
    {
        yield return new WaitForSeconds(0.5f); // 열리기 기다림
        isEnvelopeOpened = true;

        yield return new WaitForSeconds(0.2f); // 편지지 올라가기 기다렸다가, *잠깐 멈추기(멈추자마자 envelopeTop 눈 끄기)*
        isEnvelopeOpened = false;
        envelopeTop.GetComponent<Image>().enabled = false;


        yield return new WaitForSeconds(0.2f); // 편지지 올라가고 잠깐 멈췄다가, *내려가기*
        isLetterPaperMoved = true;

        yield return new WaitForSeconds(0.2f); // 편지지 내려가기 기다렸다가, *촛불, 불꽃효과 등장* //TODO: 점수 등장
        isLetterPaperMoved = false;
        isScoreDisplayed = true;
        candle.SetActive(true);
        lightEffect.SetActive(true);
        foreach(GameObject cake in cakes)  // 케이크 흑백처리
            cake.GetComponent<Image>().material = mat;
        Debug.Log("Play candleLight");
        if (GameManager.score == 100)
        {
            rank = 1;
            candleLight.GetComponent<RectTransform>().anchoredPosition = new Vector2(138f, 131f);
            lightEffect.GetComponent<RectTransform>().anchoredPosition = new Vector2(138f, 103f);
        }
        else if (GameManager.score > 85)
        {
            rank = 2;
            candleLight.GetComponent<RectTransform>().anchoredPosition = new Vector2(401.7f, 8.4f);
            lightEffect.GetComponent<RectTransform>().anchoredPosition = new Vector2(401.7f, -19.96f);
        }
        else if (GameManager.score > 75)
        {
            rank = 3;
            candleLight.GetComponent<RectTransform>().anchoredPosition = new Vector2(624.3f, -67.8f);
            lightEffect.GetComponent<RectTransform>().anchoredPosition = new Vector2(624.3f, -100f);
        }
        else
        {
            rank = 4;
            candleLight.GetComponent<RectTransform>().anchoredPosition = new Vector2(825.7f, -166f);
            lightEffect.GetComponent<RectTransform>().anchoredPosition = new Vector2(825.7f, -194f);
            cakeTopper.SetActive(true);
        }
        cakes[rank-1].GetComponent<Image>().material= null;
    }

    void Start()
    {
        // 투명부분 무시
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
        AudioSource[] aSources = GetComponents<AudioSource>();
        rectTransform= GetComponent<RectTransform>();

        envelopeSound = aSources[0];
        checkSound = aSources[1];

        isRibbonClicked = false;
        isEnvelopeOpened = false;
        isScoreDisplayed = false;

        candleLight.SetActive(true);
        cakeTopper.SetActive(false);
        lightEffect.SetActive(true);
    }

    void Update() {
        if (!isScoreDisplayed) { // 편지지 모션 두 번 실행 방지
            if(isEnvelopeOpened) {
                letterPaper.transform.position += new Vector3(0, 10.0f * Time.deltaTime, 0);
            }
            if(isLetterPaperMoved) {
                letterPaper.transform.position -= new Vector3(0, 10.0f * Time.deltaTime, 0);
            }
        }
        
    }

}
