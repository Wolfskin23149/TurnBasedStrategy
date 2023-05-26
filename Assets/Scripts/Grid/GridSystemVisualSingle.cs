using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void Show()
    {
        meshRenderer.enabled = true;
        //Debug.Log("meshRenderer Show");
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
        //Debug.Log("meshRenderer Hide");
    }
}
