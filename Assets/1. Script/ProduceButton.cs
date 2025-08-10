using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceButton : ButtonUI
{

    public override void OnClicked()
    {
        ProduceCanvas.Instance.gameObject.SetActive(true);
        //        ProduceCanvas.Instance(컴포넌트).gameObject(컴포넌트가 있는 오브젝).SetActive(true);
    }

}
