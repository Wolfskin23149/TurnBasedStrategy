using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimator; // 動畫控制器
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition; // 目標位置
    
    protected override void Awake()
    {   
        base.Awake();
        targetPosition = transform.position; // 初始時將目標位置設定為物體當前位置
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }
        Vector3 moveDirection = (targetPosition - transform.position).normalized; // 移動方向，指向目標位置的單位向量

        float stoppingDistance = .1f; // 停止距離，用於判斷是否到達目標位置

        // 如果物體與目標位置之間的距離大於停止距離
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            
            float moveSpeed = 4f; // 移動速度
            transform.position += moveDirection * moveSpeed * Time.deltaTime; // 使物體沿著移動方向移動
            unitAnimator.SetBool("IsWalking", true); // 設定動畫參數，播放行走動畫
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false); // 設定動畫參數，停止播放行走動畫
            isActive = false;
        }


        float rotateSpeed = 10f; // 旋轉速度
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed); // 使物體朝向移動方向
    }
    
    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition); // 將傳入的目標位置設定為新的目標位置
        isActive = true;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for(int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if (unitGridPosition == testGridPosition)
                {
                    // Same Grid Position where the unit is already at
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //Gird Position already occupied with another Unit
                    continue;
                }
                validGridPositionList.Add(testGridPosition);
                        
            }

        }

        return validGridPositionList;
    }
}
