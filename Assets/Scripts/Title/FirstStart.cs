using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject warning;
    [SerializeField] private GameObject warningMessage;
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject fadeIn;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstStart") == 0) //게임 첫 실행시 경고문 출력
        {
            PlayerPrefs.SetInt("FirstStart", 1);
            Debug.Log("경고문 출력");

            Invoke("stopWarning",18f); //corontine 으로 변경 필요
            PlayerPrefs.Save();
        }
        else //게임 실행 두 번째 부터는 경고문 출력x
        {
            Debug.Log("경고문 출력 x");
            canvas.GetComponent<Animator>().enabled = false;
            warning.SetActive(false);
            warningMessage.SetActive(false);
            fadeOut.SetActive(false);
            fadeIn.SetActive(false);

        }
    }
    void stopWarning()
    {
        Debug.Log("중지");
        warning.SetActive(false);
        warningMessage.SetActive(false);
        fadeOut.SetActive(false);
        fadeIn.SetActive(false);
        CancelInvoke("stopWarning");
    }
}
