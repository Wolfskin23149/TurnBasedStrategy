using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit; // 單位物件

    private void Start()
    {
        //Debug.Log(new GridPosition(5,7));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(unit.GetMoveAction().GetValidActionGridPositionList());
            Debug.Log("KeyCode.T");
        }

        //Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
    }
}
