using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public void OnClickExit()
    {
        SceneManager.LoadScene("Play Scene");
        Debug.Log("��ư ����");
    }

    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
    }
}
