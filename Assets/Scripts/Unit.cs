using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition gridPosition; // 儲存單位的網格位置
    
    private MoveAction moveAction; // 單位的移動行為

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>(); // 獲取移動行為組件
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position); // 根據物體位置獲取初始網格位置
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this); // 將單位放置在初始網格位置
    }

    private void Update()
    {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position); // 根據物體當前位置獲取新的網格位置
        if (newGridPosition != gridPosition)
        {
            // 單位改變了網格位置
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition); // 通知網格系統單位的位置變更
            gridPosition = newGridPosition; // 更新儲存的網格位置
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction; // 返回單位的移動行為
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition; // 返回單位的網格位置
    }
}
