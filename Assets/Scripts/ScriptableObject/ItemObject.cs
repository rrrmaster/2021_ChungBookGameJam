using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public class ItemObject : ScriptableObject
{
    [Header("���̵�")]
    public int ID;
    public string Name;
    public string Description;
    public Sprite Icon;

    public bool IsCrop;
    public int CropID;
}
