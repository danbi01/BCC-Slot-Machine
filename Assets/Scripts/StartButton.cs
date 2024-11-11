using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    AudioSource cherryBombSound;

    AudioSource cherryBombBurnSound;

    public Animator CherryBombAnimation;
    public GameObject cherryBomb;
    public Animator CherryBombBurnAnimation;
    public GameObject cherryBombBurn;

    public void StartButtonClickHandler()
    {
        Debug.Log("Click StartButton");
        StartCoroutine(Bomb());
    }

    IEnumerator Bomb() {
        cherryBombBurn.SetActive(true);
        CherryBombBurnAnimation.Play("CherryBombBurnAnimation");
        cherryBombBurnSound.Play();
        yield return new WaitForSeconds(2f);
        cherryBomb.SetActive(true);
        CherryBombAnimation.Play("CherryBombAnimation");
        cherryBombSound.Play();
        yield return new WaitForSeconds(1.125f);
        SceneManager.LoadScene("Play Scene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter StartButton");
        this.transform.Rotate(0, 0, 3);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit StartButton");
        this.transform.Rotate(0, 0, -3);
    }

    void Start()
    {
        // 투명부분 무시
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
        AudioSource[] aSources = GetComponents<AudioSource>();
        cherryBombBurnSound = aSources[0];
        cherryBombSound = aSources[1];
    }
}
