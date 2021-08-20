using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogPanel : MonoBehaviour
{
    private List<TextVO> list;
    private RectTransform panel;

    public TMP_Text dialogText;
    private WaitForSeconds shortWs = new WaitForSeconds(0.1f);
    private WaitForSeconds longWs = new WaitForSeconds(0.8f);

    private bool clickToNext = false;
    private bool isOpen = false;
    public GameObject nextIcon;
    public Image profileImage;

    public AudioClip typeClip;

    private int currentIndex;
    private RectTransform textTransform;


    private Dictionary<int, Sprite> imageDictionary = new Dictionary<int, Sprite>();

    void Awake()
    {
        panel = GetComponent<RectTransform>();
        textTransform = dialogText.GetComponent<RectTransform>();
    }

    public void StartDialog(List<TextVO> list)
    {
        this.list = list;
        ShowDialog();
    }

    public void ShowDialog()
    {
        panel.DOScale(new Vector3(1, 1, 1), 0.8f).OnComplete(() =>
        {

            //아이콘 처리 부분도 여기 들어가야 해
            TypeIt(list[currentIndex]);
            isOpen = true;
        });
    }

    void Update()
    {
        if (!isOpen) return;

        if (Input.GetButtonDown("Jump") && clickToNext)
        {

            if (currentIndex >= list.Count)
            {
                panel.DOScale(new Vector3(0, 0, 1), 0.8f).OnComplete(() =>
                {
                    isOpen = false;
                });
            }
            else
            {
                TypeIt(list[currentIndex]);
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            clickToNext = true;
        }
    }

    public void TypeIt(TextVO vo)
    {
        int idx = vo.icon;
        if (!imageDictionary.ContainsKey(idx))
        {
            Sprite img = Resources.Load<Sprite>($"profile{idx}");
            imageDictionary.Add(idx, img);
        }
        profileImage.sprite = imageDictionary[idx];

        dialogText.text = vo.msg;
        nextIcon.SetActive(false);
        clickToNext = false;
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        dialogText.ForceMeshUpdate(); //이걸 안해주면 정보가 업데이트가 안되서 0으로 뜬다.

        dialogText.maxVisibleCharacters = 0;
        int totalVisibleChar = dialogText.textInfo.characterCount;
        for (int i = 1; i <= totalVisibleChar; i++)
        {
            dialogText.maxVisibleCharacters = i;

            Vector3 pos = dialogText.textInfo.characterInfo[i - 1].bottomRight;

            //해당 트랜스폼에 해당하는 상대좌표로 변경해준다.
            Vector3 tPos = textTransform.TransformPoint(pos);

            dialogText.transform.DOShakePosition(0.2f, 3f);
            if (clickToNext)
            {
                dialogText.maxVisibleCharacters = totalVisibleChar;
                break;
            }
            yield return shortWs;
        }
        currentIndex++;
        clickToNext = true;
        nextIcon.SetActive(true);
    }

}
