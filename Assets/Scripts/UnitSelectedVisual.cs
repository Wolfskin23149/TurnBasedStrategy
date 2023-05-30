using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;  // 可在 Unity 編輯器中指定的單位物件
        
    private MeshRenderer meshRenderer;  // Mesh 渲染器

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();  // 獲取附加的 Mesh 渲染器組件
         Debug.Log("獲取附加的 Mesh 渲染器組件");
    }

    private void Start()
    {
        
        // 訂閱 UnitActionSystem 的 OnSelectedUnitChanged 事件
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
        Debug.Log("訂閱 UnitActionSystem 的 OnSelectedUnitChanged 事件");
             
        UpdateVisual();  // 更新顯示狀態
        
        Debug.Log("更新顯示狀態");
    }
    //如果不把 UpdateVisual();放在Update()，就無法正確啟動，感覺是Start()的問題
    private void Update()
    {
        UpdateVisual();

        /*if (Input.GetKeyDown(KeyCode.V))
        {
            UpdateVisual();
        }*/
    }


    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual(); // 當選擇的單位改變時，更新顯示狀態
        Debug.Log("當選擇的單位改變時，更新顯示狀態");
    }

    private void UpdateVisual()
    {
        // 檢查當前選擇的單位是否等於這個腳本指定的單位
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit) 
        {
            meshRenderer.enabled = true;  // 啟用 Mesh 渲染器，顯示該單位
            Debug.Log("啟用 Mesh 渲染器，顯示該單位");
        }
        else 
        {
            meshRenderer.enabled = false;  // 禁用 Mesh 渲染器，隱藏該單位
            Debug.Log("禁用 Mesh 渲染器，隱藏該單位");
        }
    }
    
}
