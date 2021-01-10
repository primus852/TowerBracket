using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager BuildManagerInstance;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;

    private TurretBlueprint _turretToBuild;
    private Node _selectedNode;

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

    public void SelectNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        _selectedNode = node;
        _turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        _selectedNode = null;
        nodeUI.Hide();
    }


    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        _turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return _turretToBuild;
    } 
}