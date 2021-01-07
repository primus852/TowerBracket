using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager BuildManagerInstance;
    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;
    public GameObject buildEffect;

    private TurretBlueprint _turretToBuild;

    public bool CanBuild => _turretToBuild != null;
    public bool HasMoney => PlayerStats.Money >= _turretToBuild.cost;

    private void Awake()
    {
        if (BuildManagerInstance != null)
        {
            Debug.LogError("Duplicate BuildManager");
            return;
        }

        BuildManagerInstance = this;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < _turretToBuild.cost)
        {
            Debug.Log("Not enough credentials");
            return;
        }

        PlayerStats.Money -= _turretToBuild.cost;

        GameObject turret =
            (GameObject) Instantiate(_turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject) Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);
    }


    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        _turretToBuild = turret;
    }
}