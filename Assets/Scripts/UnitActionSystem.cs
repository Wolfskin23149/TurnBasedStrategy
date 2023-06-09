using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private bool isBusy;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
            Instance = this;
    }

    private void Update()
    {
        if(isBusy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            if(TryHandleUnitSelection()) return;

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            
            Debug.Log("Start_TryHandleUnitSelection");

            if  (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }

            
            //selectedUnit.GetMoveAction().Move(MouseWorld.GetPosition());  
        }
        if(Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);   
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private bool TryHandleUnitSelection()
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
       {
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                selectedUnit = unit; 
                return true;
            }

       }
       return false;


    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        
        //OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        
        //這一段等於上一段，但更加精簡
        if(OnSelectedUnitChanged != null)
        {
            Debug.Log("OnSelectedUnitChanged");
            OnSelectedUnitChanged(this, EventArgs.Empty);
        }
         
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
