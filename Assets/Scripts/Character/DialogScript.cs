using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;


public class DialogScript : MonoBehaviour
{
    //인스펙터 수정 가능 변수들
    [SerializeField]
    int id; //시작할 대사 ID
    string path = "Assets\\script.CSV";

    [SerializeField]
    string name;//캐릭터 이름

    [SerializeField]
    GameObject speech_bubble_prefab; //말풍선 prefab

    SpriteRenderer renderer; //캐릭터 스프라이트

    float axis_celibration = 0.01f; //좌표 보정용


    CSVReader reader;
    // Start is called before the first frame update
    void Start()
    {
        reader = new CSVReader(path);
        renderer = gameObject.GetComponent<SpriteRenderer>();
        StartConversation();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartConversation()
    {
        //말풍선 생성
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + renderer.sprite.rect.size.y/2 * axis_celibration + 50 * axis_celibration, 0);
        Vector3 rot = new Vector3(0, 0, 0);
        GameObject speech_bubble_object = Instantiate(speech_bubble_prefab, pos, Quaternion.Euler(rot), null);
       
        //Conversation 코루틴 호출
        StartCoroutine(Conversation(speech_bubble_object));
        
    }
    void EndConversation(GameObject speech_bubble_object)
    {
        Destroy(speech_bubble_object);

    }
    IEnumerator Conversation(GameObject speech_bubble_object){
        
        RectTransform rectTransform = speech_bubble_object.GetComponent<RectTransform>(); //말풍선 transform
        

        //대사 출력 수행
        TextMeshProUGUI textMesh = speech_bubble_object.transform.GetChild(2).GetComponent<TextMeshProUGUI>(); //말풍선속 텍스트상자
        int index = id; //ID부터 Conversation 시작
        
        index -= 1; //실제로는 첫번째 원소가 ID 1 이므로 id에서 하나 빼서 저장할 예정

        string script = reader.GetContent(index);
        

        while (script != "" && reader.GetName(index) == name)
        {
            //캐릭터 이름이 달라질 때 까지 시작
            textMesh.text = script; //대사 출력
            rectTransform.sizeDelta = new Vector2(CalculateSize(script), 50  * axis_celibration); //말풍선 계산 수행
            for(int i=0; i<speech_bubble_object.transform.childCount; i++)
            {
                Transform child = speech_bubble_object.transform.GetChild(i); //자식 오브젝트 한개
                //말풍선 세모모양 부분은 별도처리
                if (i == 0)
                {
                    continue;
                }
                Debug.Log(i);
                TextMeshProUGUI textbox = speech_bubble_object.transform.GetChild(i).GetComponent<TextMeshProUGUI>();
                if (textbox) //텍스트 상자가 들어있는 오브젝트일때
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2((CalculateSize(script) - 30) * axis_celibration, 50 * axis_celibration); //양쪽 여백 15이어야 하므로 30 차감
                }
                else
                {
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(CalculateSize(script) * axis_celibration, 50 * axis_celibration); //말풍선 계산 수행
                }
            }
            yield return new WaitForSeconds(0.2f); //대사 2개 한번에 넘어가는거 방지
            while (!Input.GetMouseButtonDown(0)) //버튼 눌릴때까지 기다림
            {
                yield return new WaitForSeconds(Time.deltaTime);
            }
            index++;
            script = reader.GetContent(index);
        }

        //분기문은 필요하면 나중에 구현 예정

        //말풍선 삭제
        EndConversation(speech_bubble_object);

    }

    //계산 수행하는 함수 필요
    int CalculateSize(string s) //픽셀 단위 계산
    {
        int size = 0;
        char[] values = s.ToCharArray();
        size += 50;//앞+뒤 여백
        foreach (char c in values)
        {

            int value = Convert.ToInt32(c);
            if (value >= 0x80)
                size += 20;
            else
            {
                size += 7;
            }

        }
        return size;
    }


}
