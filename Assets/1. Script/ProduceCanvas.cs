using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceCanvas : MonoBehaviour
{
    private static ProduceCanvas instance;


    public static ProduceCanvas Instance // propery 자산 속성
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ProduceCanvas>(true);
            }
            return instance;           
        }
    }



}
