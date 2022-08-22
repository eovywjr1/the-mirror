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
    List<string> []dialogList;

    public CSVReader()
    {
        StreamReader reader = new StreamReader(path);

        //인스턴스 생성
        dialogList = new List<string>[7];
        for (int i = 0; i < 7; i++)
            dialogList[i] = new List<string>();

        string line = reader.ReadLine(); //맨 윗줄 패스
        line = reader.ReadLine();
        while (line != null)
        {
            string[] items = line.Split("@");

            for (int i = 0; i < 7; i++)
                dialogList[i].Add(items[i]);
            line = reader.ReadLine();   //이거 없어서 무한반복 발생;;
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

    public string GetDialogNo(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return dialogList[0][index];
    }

    public string GetCharacterid(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return dialogList[2][index];
    }

    public string GetContent(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return dialogList[3][index];
    }

    public string GetSelected(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return dialogList[4][index];
    }

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public string GetImpossibleIndex(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return dialogList[5][index];
    }

    public string GetChangeId(int index)
    {
        if (CheckInvalidIndex(index))
            return "";
        return dialogList[6][index];
    }
}