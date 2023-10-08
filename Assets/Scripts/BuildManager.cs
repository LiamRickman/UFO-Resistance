using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public GameObject buildEffect;
    public GameObject sellEffect;

    [SerializeField] NodeUI nodeUI;
    [SerializeField] TurretUpgradeStats turretUI;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return GameStats.currentMoney >= turretToBuild.level1Cost; } }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public void SelectNode (Node _node)
    {
        if (selectedNode == _node)
        {
            DeselectNode();
            return;
        }

        selectedNode = _node;
        turretToBuild = null;
        nodeUI.SetTarget(_node);
        turretUI.SetTarget(_node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
