using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ScoreButton : MonoBehaviour
    
{
    public Vector3 targetPosition;

    Animator anim;
    AudioSource keySound;
    AudioSource doorSound;
    AudioSource lightSwitchSound;
    AudioSource shalalaSound;

    public GameObject background;
    public GameObject door;
    public GameObject halfDoor;
    public GameObject candle;
    public GameObject cakeTopper;
    public GameObject lightEffect;
    bool isScoreButtonClicked = false;
    public static bool isLightOn = false;

    int rank = 0;

    // 열쇠 클릭 시 열쇠, 문, 배경 비활성화
    public void ScoreButtonClickHandler()
    {
        isScoreButtonClicked = true;
        keySound.Play(0);
        Debug.Log("Click ScoreButton");
        GameManager.ScoreHandler();
        StartCoroutine(SetTimeOutMoveKey());
    }
    
    IEnumerator SetTimeOutMoveKey()
    {
        // 열쇠 회전
        yield return new WaitForSeconds(1.5f);
        isScoreButtonClicked = false;
        anim.Play("keyRotation");
        Debug.Log("Play keyRotation");

        // 문 열림
        yield return new WaitForSeconds(1.0f);
        transform.position = new Vector3(-1300, 0, 0);
        halfDoor.SetActive(false);
        Door.anim.Play("door_open");
        doorSound.Play(0);
        Debug.Log("Play doorOpen");
        
        // 불 켜짐, 편지지 열림
        yield return new WaitForSeconds(1.5f);
        isLightOn = true;
        lightSwitchSound.Play(0);
        shalalaSound.Play(0);
        door.SetActive(false);
        background.SetActive(false);
        GameManager.AddSelectedParts();
        Envelope.anim.Play("Envelope_open");

        // 촛불 켜짐
        yield return new WaitForSeconds(1f);
        candle.SetActive(true);
        lightEffect.SetActive(true);
        Debug.Log("Play candleLight");

        // candle.SetActive(true) 기다리기
        yield return new WaitForSeconds(0.00001f);
        if (GameManager.score == 100) {
            rank = 1;
            CandleLight.rectTransform.anchoredPosition = new Vector2(138f, 131f); //138 131
            LightEffect.rectTransform.anchoredPosition = new Vector2(138f, 103f);
        }
        else if(GameManager.score > 85) { // 95, 90
            rank = 2;
            CandleLight.rectTransform.anchoredPosition = new Vector2(401.7f, 8.4f);
            LightEffect.rectTransform.anchoredPosition = new Vector2(401.7f, -19.96f);
        }
        else if(GameManager.score > 75) { // 85, 80, 75
            rank = 3;
            CandleLight.rectTransform.anchoredPosition = new Vector2(624.3f, -67.8f);//624.3, 67.8
            LightEffect.rectTransform.anchoredPosition = new Vector2(624.3f, -100f);
        } else {                         // 70 ... 0
            rank = 4;
            CandleLight.rectTransform.anchoredPosition = new Vector2(825.7f, -166f); // 166.2
            LightEffect.rectTransform.anchoredPosition = new Vector2(825.7f, -194f);
            cakeTopper.SetActive(true);
        }
        // 점수 출력
        Debug.Log("Score: " + GameManager.score + ", Rank: " + rank +"등");

    }

    void Start()
    {
        // 투명배경 무시
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
        anim = GetComponent<Animator>();

        // var audioSources = GetComponent<AudioSource>();
        AudioSource[] aSources = GetComponents<AudioSource>();
        keySound = aSources[0];
        lightSwitchSound = aSources[1];
        shalalaSound = aSources[2];
        doorSound = door.GetComponent<AudioSource>();

        background.SetActive(true);
        door.SetActive(true);
        halfDoor.SetActive(true);
        gameObject.SetActive(true);
        candle.SetActive(false);
        cakeTopper.SetActive(false);
        lightEffect.SetActive(false);

        targetPosition = new Vector3(-1, transform.position.y, 0);
    }

    void Update()
    {
        if (isScoreButtonClicked)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 70.0f * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("스페이스 키 누름");
            SceneManager.LoadScene("Start Scene");
        }
    }
}
