using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.BuildManagerInstance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret selected");
        _buildManager.SetTurretToBuild(_buildManager.standardTurretPrefab);
    }
    
    public void PurchaseMissileLauncher()
    {
        Debug.Log("Missle Launcher selected");
        _buildManager.SetTurretToBuild(_buildManager.missileLauncherPrefab);
    }
}