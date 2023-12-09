using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MovingGround : MonoBehaviour
{
    public Vector2 offset = Vector2.zero;

    MeshRenderer m_Renderer;

    void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Renderer.material.mainTextureOffset = Time.time * offset;
    }
}
