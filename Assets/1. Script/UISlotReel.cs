using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UISlotReel : MonoBehaviour
//현재 게임 오브젝트 기준 하위
{
    public SlotItem[] slotItems;
    public SlotItem currentSlotItem;
    public List<string> contentList = new List<string>();
    public bool rolling;
    public float maxSlotSpeed = 1000;
    public float slotSpeed;

    public float slotItemGap;
    public float changeBottomY;


    private void Start()
    {
        StartRoll(new string[] {
            "는(은) 고백이 받고 싶어!",
": 영원의 기록",
"인데 아무도 안믿어준다!?",
"는(은) 딱 질색이니까!",
"를(을) 쓰러뜨리겠습니다만!?",
"의 주인이 되어버렸다!",
"의 사건 파일" });
    }

    public string[] texts;
    //원하는 타이밍에 호출해야됌 + 랜덤하게 보여질 글씨들을 같이 전달하기
    public void StartRoll(string[] texts)
    {
        this.texts = texts;
        contentList.AddRange(texts); // 배열에 있는 데이터들을 전부 리스트에 담음
        ShuffleArray(contentList);
        for (int i = 0; i < slotItems.Length; i++)
        {

            slotItems[i].text.text = contentList[0];
            contentList.RemoveAt(0);
        }

        rolling = true;
        currentSlotItem = slotItems[1];

        StartCoroutine(CoStopSlot());

        //코루틴 함수로 5초 후  로그 찍고 + slotSpeed를 점점점 감소 시키게 처리해주세요.
    }
private void ShuffleArray(List<string> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1); // 0부터 i까지 랜덤 인덱스 선택
            // 두 값을 교환
            string temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    IEnumerator CoStopSlot()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("스핀 멈추기");
        while (true)
        {
            if (slotSpeed <= 30)
            {
                break;
            }

            slotSpeed -= Time.deltaTime * 500;
            yield return null;
        }

        //현재 기준 가운데에 해당하는 슬롯 아이템을 정가운데로 오도록 처리!
        rolling = false;

        while (true)
        {
            for (int i = 0; i < slotItems.Length; i++)
            {
                currentSlotItem.rectTr.anchoredPosition += Vector2.down * slotSpeed * Time.deltaTime;
                slotItems[0].rectTr.anchoredPosition = new Vector2(0, currentSlotItem.rectTr.anchoredPosition.y + slotItemGap);
                slotItems[2].rectTr.anchoredPosition = new Vector2(0, currentSlotItem.rectTr.anchoredPosition.y - slotItemGap);


                if (currentSlotItem.rectTr.anchoredPosition == new Vector2(0, 0))
                {
                    Debug.Log("진짜 끝");
                    yield break; //코루틴 함수 종료!
                }
            }
        }
    }

    private void Update()
    {
        if (!rolling)
            return;

        for (int i = 0; i < slotItems.Length; i++)
        {
            currentSlotItem.rectTr.anchoredPosition += Vector2.down * slotSpeed * Time.deltaTime;
            slotItems[0].rectTr.anchoredPosition = new Vector2(0, currentSlotItem.rectTr.anchoredPosition.y + slotItemGap);
            slotItems[2].rectTr.anchoredPosition = new Vector2(0, currentSlotItem.rectTr.anchoredPosition.y - slotItemGap);


            if (currentSlotItem.rectTr.anchoredPosition.y <= changeBottomY)
            {
                slotItems[2].rectTr.anchoredPosition = new Vector2(0, changeBottomY + 2 * slotItemGap);
                SlotItem slotItem = slotItems[0];
                slotItems[0] = slotItems[2];
                slotItems[2] = slotItems[1];
                slotItems[1] = slotItem;

                currentSlotItem = slotItem;

                //여기에서 맨 아래에 있는 슬롯 아이템의 text.text값을 
                //contentlist리스트에 남아있는 데이터로 설정하면서 데이터를 반복적으로 보여주기
            }
        }


    }
}

//     [Header("릴 UI 컨테이너")]
//     public RectTransform reelContent; // Vertical Layout Group 자식 컨테이너

//     [Header("아이템 프리팹")]
//     public GameObject itemPrefab;

//     [Header("스핀 세팅")]
//     public float spinSpeed = 300f; // px/sec
//     public float deceleration = 600f; // 감속 속도

//     [Header("릴 키워드 목록")]
//     public string[] keywords;

//     public bool spinning = false;
//     private float currentSpeed;
//     private float itemHeight;
//     private int targetIndex;

//     void Start()
//     {
//         SetupItems();
//     }

//     public void SetupItems()
//     {
//         // 기존 아이템 제거
//         foreach (Transform child in reelContent)
//         {
//             Destroy(child.gameObject);
//         }

//         // 새 아이템 생성
//         for (int i = 0; i < keywords.Length; i++)
//         {
//             GameObject go = Instantiate(itemPrefab, reelContent);
//             TMP_Text txt = go.GetComponentInChildren<TMP_Text>();
//             if (txt != null) txt.text = keywords[i];
//         }

//         LayoutRebuilder.ForceRebuildLayoutImmediate(reelContent);

//         if (reelContent.childCount > 0)
//             itemHeight = reelContent.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
//         else
//             itemHeight = 0;
//     }

//     public void StartSpin()
//     {
//         spinning = true;
//         currentSpeed = spinSpeed;
//     }

//     public void StopSpin(string targetWord)
//     {
//         targetIndex = System.Array.IndexOf(keywords, targetWord);
//         StartCoroutine(SlowDownAndStop());
//     }

//     void Update()
//     {
//         if (!spinning) return;

//         reelContent.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;

//         // 순환 처리
//         if (reelContent.anchoredPosition.y >= itemHeight)
//         {
//             reelContent.anchoredPosition -= new Vector2(0, itemHeight);
//             Transform first = reelContent.GetChild(0);
//             first.SetSiblingIndex(reelContent.childCount - 1);
//         }
//     }

//     IEnumerator SlowDownAndStop()
//     {
//         while (currentSpeed > 0)
//         {
//             currentSpeed -= deceleration * Time.deltaTime;
//             if (currentSpeed < 0) currentSpeed = 0;
//             yield return null;
//         }

//         spinning = false;

//         // 목표 단어 위치 맞춤
//         for (int i = 0; i < reelContent.childCount; i++)
//         {
//             var txt = reelContent.GetChild(i).GetComponentInChildren<TMP_Text>();
//             if (txt != null && txt.text == keywords[targetIndex])
//             {
//                 float offset = -i * itemHeight;
//                 reelContent.anchoredPosition = new Vector2(0, offset);
//                 break;
//             }
//         }
//     }
// }