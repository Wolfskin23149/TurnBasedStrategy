using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContianerTransform;

    private void Start()
    {
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        Debug.Log("CreateUnitActionButtons()");
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Instantiate(actionButtonPrefab, actionButtonContianerTransform);
            Debug.Log("Instantiate(actionButtonPrefab, actionButtonContianerTransform);");
        }
    }
}
