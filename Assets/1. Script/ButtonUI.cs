using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonUI : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    //기능을 가지고 있는게 아니고 구조만 있어서 직접 컴포넌트에 붙지는 않음 
    public abstract void OnClicked();

}
