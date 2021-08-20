using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShakeText : MonoBehaviour
{
    private TMP_Text tmpMesh;

    Mesh mesh;
    Vector3[] vertices;

    void Awake()
    {
        tmpMesh = GetComponent<TMP_Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmpMesh.ForceMeshUpdate();
        mesh = tmpMesh.mesh;
        vertices = mesh.vertices;

        // for(int i = 0; i < vertices.Length; i++)
        // {
        //     Vector3 offset = Wobble(Time.time + i);
        //     vertices[i] = vertices[i] + offset;
        // }

        for(int i = 0; i < tmpMesh.textInfo.characterCount; i++){
            TMP_CharacterInfo c = tmpMesh.textInfo.characterInfo[i];
            int idx = c.vertexIndex;

            Vector3 offset = Wobble(Time.time + i);
            for(int j = 0; j < 4; j++){
                vertices[idx + j] += offset;
            }
        }

        mesh.vertices = vertices;
        tmpMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f) * 2, Mathf.Cos(time*2.5f)*2);
    }
}
