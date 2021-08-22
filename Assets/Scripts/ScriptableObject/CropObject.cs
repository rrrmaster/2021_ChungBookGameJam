using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Game/Crop")]
public class CropObject : ScriptableObject
{
    [Header("���̵�")]
    public int ID;
    [Header("���� ���� ������ ���̵�")]
    public int ItemID;
    [Header("���� ������ ���̵�")]
    public int SellItemID;
    [Header("�⺻ ����")]
    public int BasePrice;

    [Header("���� ����")]
    public int GrowDay;



    [Header("�̸�")]
    public string Name;
    
    [TextArea]
    [Header("����")]
    public string Description;

    [Header("����")]
    public Season Seasons;

    [EnumFlags]
    [Header("��Ȯ ����")]
    public Season HarvestSeasons;

    [Header("������")]
    public Sprite Icon;

    [Header("�ִϸ��̼�")]
    public Sprite[] Animation;


    [Header("������")]
    public List<ItemObject>  ItemObjects;
}

    
[System.Flags]
public enum Season
{
    Spring =1, Summer=2, Autumn=4, Winter=8
}

