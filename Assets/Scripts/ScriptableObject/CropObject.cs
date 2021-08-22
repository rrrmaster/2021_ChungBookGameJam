using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Game/Crop")]
public class CropObject : ScriptableObject
{
    [Header("아이디")]
    public int ID;
    [Header("상점 구매 아이탬 아이디")]
    public int ItemID;
    [Header("열매 아이탬 아이디")]
    public int SellItemID;
    [Header("기본 가격")]
    public int BasePrice;

    [Header("성장 기한")]
    public int GrowDay;



    [Header("이름")]
    public string Name;
    
    [TextArea]
    [Header("설명")]
    public string Description;

    [Header("계절")]
    public Season Seasons;

    [EnumFlags]
    [Header("수확 계절")]
    public Season HarvestSeasons;

    [Header("아이콘")]
    public Sprite Icon;

    [Header("애니메이션")]
    public Sprite[] Animation;


    [Header("아이템")]
    public List<ItemObject>  ItemObjects;
}

    
[System.Flags]
public enum Season
{
    Spring =1, Summer=2, Autumn=4, Winter=8
}

