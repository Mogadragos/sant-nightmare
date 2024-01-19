using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MovingGround : MonoBehaviour
{
    MeshRenderer m_Renderer;
    Vector2 _offset;

    void Awake()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    public void UpdateSpeed(float ZOffset)
    {
        _offset = new Vector2(0, ZOffset);
    }

    public void Move(float time)
    {
        m_Renderer.material.mainTextureOffset = time * _offset;
    }
}
