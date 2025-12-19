using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeywordManager : MonoBehaviour
{

    public KeywordData[] keywordDatas;
    public static KeywordManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public KeywordData GetKeywordData(string key) //텍스트와 키값이 같음
    {
        for (int i = 0; i < keywordDatas.Length; i++)
        {
            if (keywordDatas[i].key == key)
            {
                return keywordDatas[i];
            }
        }
        return null;
    }
    void Start()
    {

    }
    void Update()
    {
        
    }

}

[System.Serializable]
public class KeywordData
{
    public string key;
    public KeywordType keywordType;
    public int minFun;
    public int maxFun;

    public int minTouch;
    public int maxTouch;

    public int minStory;
    public int maxStory;
}

public enum KeywordType
{처음, 중간, 끝 }
