using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.BuildManagerInstance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret selected");
        _buildManager.SelectTurretToBuild(standardTurret);
    }
    
    public void SelectMissileLauncher()
    {
        Debug.Log("Missle Launcher selected");
        _buildManager.SelectTurretToBuild(missileLauncher);
    }
}