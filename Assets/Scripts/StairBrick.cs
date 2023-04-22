using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBrick : MonoBehaviour
{
    public ColorData color;
    public MeshRenderer renderers;
    void Start()
    {
        ChangeColor();
    }
    private void ChangeColor()
    {
        renderers.sharedMaterial = color.GetColorBrick();

    }
}
