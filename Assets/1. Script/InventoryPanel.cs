using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    private static InventoryPanel instance;


    public static InventoryPanel Instance // propery 자산 속성
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryPanel>(true);
            }
            return instance;
        }
    }

}
