using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class CSVReader
{

    const string path = "Assets\\script.CSV";
    bool isLoaded = false;//파일 로딩 확인
    int lines = 0;//줄 수 기록
    //ID는 그냥 csv 파일에만 기록, 불러오지는 않을 예정
    List<string> names;//캐릭터 이름 목록
    List<string> contents;//대사 목록
    

    public CSVReader()
    {
        StreamReader reader = new StreamReader(path);

        //인스턴스 생성
        names = new List<string>();
        contents = new List<string>();

        string line = reader.ReadLine(); //맨 윗줄 패스
        line = reader.ReadLine();
        while(line != null)
        {
            string[] items = line.Split(",");
            names.Add(items[1]);
            contents.Add(items[2]);
            line = reader.ReadLine();//이거 없어서 무한반복 발생;;
            lines++;
        }
        isLoaded = true;
        
    }

    public int GetLine()
    {
        return lines;
    }
    public bool CheckInvalidIndex(int index)
    {
        if (lines <= index || index < 0)
        {
            return true;
        }
        else
            return false;
    }
    public string GetName(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return names[index];
    }
    public string GetContent(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return contents[index];
    }
    public bool IsLoaded()
    {
        return isLoaded;
    }


}


public class DialogScript : MonoBehaviour
{
    //인스펙터 수정 가능 변수들
    [SerializeField]
    int id; //시작할 대사 ID
    
    [SerializeField]
    string name;//캐릭터 이름

    /*
     index == -1 : StartConversation() 추가 실행하고 첫 대사부터 실행(index = id로 바꾸고 Conversation())
     getContent == null이거나 인물 불일치 조건 도달 : 말풍선 없애고 index = -1
    나머지 : Conversation()
    Conversation() : 말풍선에 대화 출력하고 index++

     */
    CSVReader reader;
    // Start is called before the first frame update
    void Start()
    {
        reader = new CSVReader();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StartConversation()
    {

        //ID부터 Conversation 시작
        
        //초기 인덱스 id로 초기화
        int index = id;
        //실제로는 첫번째 원소가 ID 1 이므로 id에서 하나 빼서 저장할 예정
        index--;
        string script = reader.GetContent(index);
        while (script != "" && reader.GetName(index) == name)
        {
            //캐릭터 이름이 달라질 때 까지 시작
            Debug.Log(script);
            index++;
            script = reader.GetContent(index);
        }

        //분기문은 필요하면 나중에 구현 예정
    }
}
