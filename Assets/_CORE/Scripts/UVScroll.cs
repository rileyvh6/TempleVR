using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scroll uv texture over time
/// </summary>
[ExecuteInEditMode]
public class UVScroll : MonoBehaviour
{
    private Material mat;
    [SerializeField] Vector2 speed;

    void Awake()
    {
        mat = GetComponent<Renderer>().sharedMaterial;
    }

    void LateUpdate()
    {
        mat.mainTextureOffset += speed * Time.deltaTime;
    }
}
