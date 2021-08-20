using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Game/Crop")]
public class CropObject : ScriptableObject
{
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
}
[System.Flags]
public enum Season
{
    Spring, Summer, Autumn, Winter
}

