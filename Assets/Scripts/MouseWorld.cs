using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{   
    // 用於序列化的屬性，使得 mousePlanLayMask 可以在 Unity 編輯器中可見和設定
    private static MouseWorld instance;

    [SerializeField] private LayerMask mousePlanLayMask;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
       transform.position = MouseWorld.GetPosition();
       //range();
    }

    public static Vector3 GetPosition()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlanLayMask);
       return raycastHit.point;
    }
    
}
