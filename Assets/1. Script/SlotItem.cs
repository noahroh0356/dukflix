using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotItem : MonoBehaviour
{
    public TMP_Text text;
    public RectTransform rectTr;

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
    }

}
