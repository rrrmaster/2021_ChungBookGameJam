using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public Renderer renderer;
    public OrderType orderType;
    public enum OrderType
    {
        Dynamic,Static
    }
    private void Start()
    {
        if(orderType == OrderType.Static)
            renderer.sortingOrder = -Mathf.FloorToInt(transform.position.y * 5);

    }
    private void Update()
    {
        if(orderType == OrderType.Dynamic)
            renderer.sortingOrder = -Mathf.FloorToInt(transform.position.y*5);
    }

 
}
