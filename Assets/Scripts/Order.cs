using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public Renderer renderer;
    void Update()
    {
        renderer.sortingOrder = -Mathf.FloorToInt(transform.position.y) * 5;
    }

 
}
