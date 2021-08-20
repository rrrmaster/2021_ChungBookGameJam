using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Game/Crop")]
public class CropObject : ScriptableObject
{
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
}
[System.Flags]
public enum Season
{
    Spring, Summer, Autumn, Winter
}

