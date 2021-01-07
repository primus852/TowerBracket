using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    
    [Header("Optional")]
    public GameObject turret;

    private Renderer _renderer;
    private Color _startColor;
    private BuildManager _buildManager;


    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
        _buildManager = BuildManager.BuildManagerInstance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !_buildManager.CanBuild)
        {
            return;
        }
        
        
        if (turret != null)
        {
            Debug.Log("Can't build here");
            return;
        }

        _buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !_buildManager.CanBuild)
        {
            return;
        }

        _renderer.material.color = _buildManager.HasMoney ? hoverColor : notEnoughMoneyColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}