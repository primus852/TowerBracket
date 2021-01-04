using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer _renderer;
    private Color _startColor;
    private GameObject _turret;
    private BuildManager _buildManager;


    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
        _buildManager = BuildManager.BuildManagerInstance;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || _buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        
        
        if (_turret != null)
        {
            Debug.Log("Can't build here");
            return;
        }

        GameObject turretToBuild = _buildManager.GetTurretToBuild();
        
        var transform1 = transform;
        _turret = (GameObject)Instantiate(turretToBuild, transform1.position + positionOffset, transform1.rotation);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || _buildManager.GetTurretToBuild() == null)
        {
            return;
        }
        
        _renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
}