using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2Int pos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(pos.x, pos.y, 0) + Vector3.one / 2;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.one / 2, Vector3.one);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(pos.x, pos.y, 0) + Vector3.one / 2, Vector3.one);


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.one / 2, new Vector3(pos.x, pos.y, 0) + Vector3.one / 2);
    }
#endif
}
