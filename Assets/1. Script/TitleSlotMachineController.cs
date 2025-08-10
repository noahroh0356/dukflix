using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TitleSlotMachineController : MonoBehaviour
{
    [Header("슬롯 릴 3개")]
    public UISlotReel reel1;
    public UISlotReel reel2;
    public UISlotReel reel3;

    [Header("아이템 선택 버튼들")]
    public Button[] inventoryButtons;

    [Header("선택된 슬롯 버튼 2개")]
    public Button slotButton1;
    public Button slotButton2;

    [Header("최종 제목 표시")]
    public TMP_Text finalTitleText;

    private string selectedItem1 = "";
    private string selectedItem2 = "";

    // 일본 애니 감성 키워드 (릴용)
    private string[] part1 = {
     "거대 로봇", "눈 떠보니", "엘리트 학원 속", "초차원 마법 소녀", "포니테일", "도내 최고 미소녀"
    };

    private string[] part2 = {
        "마왕", "용사", "전학생", "먼치킨", "검성","황녀", "서큐버스"
    };

    private string[] part3 = {
    "를(을) 쓰러뜨리겠습니다만?",
    "이군요!? 정말인가요!?",
    "가(이) 무리입니다만?",
    "인데 아무도 안믿어준다!?",
    "와(과) 계약을 맺다",
    "는(은) 딱 질색이니까",
    "의 주인이 되어버린" };

    void Start()
    {
        // 아이템 선택 버튼에 클릭 이벤트 등록
        foreach (var btn in inventoryButtons)
        {
            string itemName = btn.GetComponentInChildren<TMP_Text>().text;
            btn.onClick.AddListener(() => OnInventoryItemSelected(itemName));
        }

        // 슬롯 버튼 초기 텍스트 클리어
        slotButton1.GetComponentInChildren<TMP_Text>().text = "선택 1";
        slotButton2.GetComponentInChildren<TMP_Text>().text = "선택 2";
    }

    void OnInventoryItemSelected(string itemName)
    {
        if (string.IsNullOrEmpty(selectedItem1))
        {
            selectedItem1 = itemName;
            slotButton1.GetComponentInChildren<TMP_Text>().text = itemName;
        }
        else if (string.IsNullOrEmpty(selectedItem2))
        {
            selectedItem2 = itemName;
            slotButton2.GetComponentInChildren<TMP_Text>().text = itemName;
        }
        else
        {
            Debug.Log("슬롯이 가득 찼습니다.");
        }
    }

    public void OnClickGenerateTitle()
    {
        if (string.IsNullOrEmpty(selectedItem1) || string.IsNullOrEmpty(selectedItem2))
        {
            Debug.LogWarning("두 슬롯 모두 아이템을 선택하세요.");
            return;
        }

        StartCoroutine(SpinAllReels());
    }

    IEnumerator SpinAllReels()
    {
        // 릴에 키워드 세팅 + 아이템 넣기 (각 릴에 아이템 이름도 포함)
        reel1.keywords = InsertItemToKeywords(part1, selectedItem1);
        reel2.keywords = InsertItemToKeywords(part2, selectedItem2);
        reel3.keywords = part3;

        reel1.SetupItems();
        reel2.SetupItems();
        reel3.SetupItems();

        // 스핀 시작
        reel1.StartSpin();
        reel2.StartSpin();
        reel3.StartSpin();

        // 1번 릴부터 멈추기 (시간 간격)
        yield return new WaitForSeconds(1.8f);
        reel1.StopSpin(GetRandomKeyword(reel1.keywords));

        yield return new WaitForSeconds(1.8f);
        reel2.StopSpin(GetRandomKeyword(reel2.keywords));

        yield return new WaitForSeconds(1.8f);
        reel3.StopSpin(GetRandomKeyword(reel3.keywords));

        // 릴 모두 멈출 때까지 대기
        while (reel1.spinning || reel2.spinning || reel3.spinning)
            yield return null;

        // 최종 제목 조합 및 출력
        string title = $"{selectedItem1} {reel1.keywords[Random.Range(0, reel1.keywords.Length)]} " +
                       $"{selectedItem2} {reel2.keywords[Random.Range(0, reel2.keywords.Length)]} " +
                       $"{reel3.keywords[Random.Range(0, reel3.keywords.Length)]}";

        finalTitleText.text = title;
    }

    string[] InsertItemToKeywords(string[] baseKeywords, string item)
    {
        string[] newKeywords = new string[baseKeywords.Length + 1];
        newKeywords[0] = item;
        for (int i = 0; i < baseKeywords.Length; i++)
            newKeywords[i + 1] = baseKeywords[i];
        return newKeywords;
    }

    string GetRandomKeyword(string[] keywords)
    {
        return keywords[Random.Range(0, keywords.Length)];
    }
}
