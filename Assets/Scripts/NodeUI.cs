using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node _target;

    public void SetTarget(Node target)
    {
        _target = target;

        transform.position = target.GetBuildPosition();

        if (!_target.isUpgraded)
        {
            upgradeCost.text = "$" + _target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "max.";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + _target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.BuildManagerInstance.DeselectNode();
    }
    
    public void Sell()
    {
        _target.SellTurret();
        BuildManager.BuildManagerInstance.DeselectNode();
    }
}