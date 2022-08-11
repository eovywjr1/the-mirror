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
    List<string> isSelected; //선택지 유무
    
    public CSVReader()
    {
        StreamReader reader = new StreamReader(path);

        //인스턴스 생성
        names = new List<string>();
        contents = new List<string>();
        isSelected = new List<string>();

        string line = reader.ReadLine(); //맨 윗줄 패스
        line = reader.ReadLine();
        while (line != null)
        {
            string[] items = line.Split(",");
            names.Add(items[1]);
            contents.Add(items[2]);
            isSelected.Add(items[3]);
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

    public string GetSelected(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return isSelected[index];
    }

    public bool IsLoaded()
    {
        return isLoaded;
    }


}