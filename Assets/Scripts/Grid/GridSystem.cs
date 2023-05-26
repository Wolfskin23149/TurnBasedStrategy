using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;          // 網格的寬度
    private int height;         // 網格的高度
    private float cellSize;     // 網格的單位大小
    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        
        
        // 在建構函式中繪製網格
        gridObjectArray = new GridObject[width,height];

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                // 使用 Debug.DrawLine 方法在 Scene 視圖中繪製網格線
                // 由 GetWorldPosition 方法計算出網格位置的世界座標，並畫一條線段
                // 從網格位置的座標到右方一小段距離的座標，顏色為白色，持續時間為 1000 毫秒
                //Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * 0.2f, Color.white, 1000);
                GridPosition gridPosition = new GridPosition(x,z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    // 將網格位置轉換為世界座標
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        // 通過將網格位置的 x 和 z 座標乘以 cellSize，得到該位置的世界座標
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    // 將世界座標轉換為網格位置
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        // 通過將世界座標的 x 和 z 值除以 cellSize 並四捨五入為整數，得到該位置的網格位置
        return new GridPosition
        (
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public void CreateDebugObject(Transform debugPrefab)
    {
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition),Quaternion.identity);           
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));            
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x,gridPosition.z];
    }

     public bool IsValidGridPosition(GridPosition gridPosition)
    {
    // 判斷網格位置是否有效
    // 如果網格位置的 x 座標大於等於 0、z 座標大於等於 0、x 座標小於寬度、z 座標小於高度
    // 則視為有效的網格位置，返回 true；否則返回 false
    return gridPosition.x >= 0 &&
           gridPosition.z >= 0 &&
           gridPosition.x < width &&
           gridPosition.z < height;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
