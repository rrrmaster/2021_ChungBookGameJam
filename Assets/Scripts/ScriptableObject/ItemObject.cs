using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item")]
public class ItemObject : ScriptableObject
{
    [Header("¾ÆÀÌµð")]
    public int ID;
    public string Name;
    public Sprite Icon;
}
