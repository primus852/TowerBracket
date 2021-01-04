using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager BuildManagerInstance;
    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;
    
    private GameObject _turretToBuild;

    private void Awake()
    {
        if (BuildManagerInstance != null)
        {
            Debug.LogError("Duplicate BuildManager");
            return;
        }
        BuildManagerInstance = this;
    }

    public GameObject GetTurretToBuild()
    {
        return _turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        _turretToBuild = turret;
    }
}