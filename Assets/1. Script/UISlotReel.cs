using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UISlotReel : MonoBehaviour
{
    [Header("릴 UI 컨테이너")]
    public RectTransform reelContent; // Vertical Layout Group 자식 컨테이너

    [Header("아이템 프리팹")]
    public GameObject itemPrefab;

    [Header("스핀 세팅")]
    public float spinSpeed = 300f; // px/sec
    public float deceleration = 600f; // 감속 속도

    [Header("릴 키워드 목록")]
    public string[] keywords;

    private bool spinning = false;
    private float currentSpeed;
    private float itemHeight;
    private int targetIndex;

    void Start()
    {
        SetupItems();
    }

    public void SetupItems()
    {
        // 기존 아이템 제거
        foreach (Transform child in reelContent)
        {
            Destroy(child.gameObject);
        }

        // 새 아이템 생성
        for (int i = 0; i < keywords.Length; i++)
        {
            GameObject go = Instantiate(itemPrefab, reelContent);
            TMP_Text txt = go.GetComponentInChildren<TMP_Text>();
            if (txt != null) txt.text = keywords[i];
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(reelContent);

        if (reelContent.childCount > 0)
            itemHeight = reelContent.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        else
            itemHeight = 0;
    }

    public void StartSpin()
    {
        spinning = true;
        currentSpeed = spinSpeed;
    }

    public void StopSpin(string targetWord)
    {
        targetIndex = System.Array.IndexOf(keywords, targetWord);
        StartCoroutine(SlowDownAndStop());
    }

    void Update()
    {
        if (!spinning) return;

        reelContent.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;

        // 순환 처리
        if (reelContent.anchoredPosition.y >= itemHeight)
        {
            reelContent.anchoredPosition -= new Vector2(0, itemHeight);
            Transform first = reelContent.GetChild(0);
            first.SetSiblingIndex(reelContent.childCount - 1);
        }
    }

    IEnumerator SlowDownAndStop()
    {
        while (currentSpeed > 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed < 0) currentSpeed = 0;
            yield return null;
        }

        spinning = false;

        // 목표 단어 위치 맞춤
        for (int i = 0; i < reelContent.childCount; i++)
        {
            var txt = reelContent.GetChild(i).GetComponentInChildren<TMP_Text>();
            if (txt != null && txt.text == keywords[targetIndex])
            {
                float offset = -i * itemHeight;
                reelContent.anchoredPosition = new Vector2(0, offset);
                break;
            }
        }
    }
}
