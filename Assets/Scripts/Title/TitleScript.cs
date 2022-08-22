using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleScript : MonoBehaviour
{
    public Animator startingAnim;
    public GameObject start;
    public GameObject load;
    public GameObject quit;
    public GameObject quitAnim;
    public void StartGame()//게임 실행
    {
        Debug.Log("게임 실행");
        start.SetActive(false);
        load.SetActive(false);
        quit.SetActive(false);
        quitAnim.SetActive(false);
        startingAnim = GetComponent<Animator>();
        startingAnim.SetBool("StartLoad", true);
    }
    public void ExitGame()//게임 종료
    {
        Debug.Log("게임 종료");
        Application.Quit();
    } 
    public void DeleteFirstStartData()
    {
        PlayerPrefs.DeleteKey("FirstStart");
        Debug.Log("테스트용:게임 첫 실행 데이터 제거");
    }
}
