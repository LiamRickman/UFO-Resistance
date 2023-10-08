using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0

public class Node : MonoBehaviour
{
    private Renderer rend;

    [SerializeField] Color hoverColourYes;
    [SerializeField] Color hoverColourNo;
    private Color startColour;

    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    //[HideInInspector] public bool fullyUpgraded = false;
    [HideInInspector] public int upgradeLevel = 1;

    //Preview Turrets
    private GameObject mgPreview;
    [SerializeField] GameObject mgPreviewPrefab;
    private GameObject rocketPreview;
    [SerializeField] GameObject rocketPreviewPrefab;
    private GameObject laserPreview;
    [SerializeField] GameObject laserPreviewPrefab;

    public Vector3 positionOffset;

    BuildManager buildManager;

    private Turret t;

    [SerializeField] bool tutorialNode = false;

    //Turret Range Values
    [SerializeField] Image currentRangeIMG;

    public static float currentRange = 2;
    [SerializeField] float rangeMultiplier = 0.17f;

    private Shop shop;

    private bool showRange = false;

    private bool selected = false;

    private int sellUpgrades;


    //New
    private PlayerStatsUI statsUI;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.materials[1].color;
        buildManager = BuildManager.instance;
        statsUI = GameObject.FindGameObjectWithTag("StatsUI").GetComponent<PlayerStatsUI>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();

        mgPreview = (GameObject)Instantiate(mgPreviewPrefab, GetBuildPosition(), Quaternion.identity);
        rocketPreview = (GameObject)Instantiate(rocketPreviewPrefab, GetBuildPosition(), Quaternion.identity);
        laserPreview = (GameObject)Instantiate(laserPreviewPrefab, GetBuildPosition(), Quaternion.identity);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (tutorialNode)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void OnMouseOver()
    {
        if (tutorialNode)
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney && turret == null)
            rend.materials[1].color = hoverColourYes;
        else if (turret == null)
            rend.materials[1].color = hoverColourNo;

        showRange = true;
    }

    private void OnMouseExit()
    {
        if (tutorialNode)
            return;

        if (!selected)
        {
            ResetNode();
        }
    }

    private void ResetNode()
    {
        rend.materials[1].color = startColour;
        showRange = false;
    }

    private void BuildTurret(TurretBlueprint _turretBlueprint)
    {
        if (GameStats.currentMoney < _turretBlueprint.level1Cost)
        {
            Debug.Log("Not enough money to build turret");
            SoundManager.PlaySound("NoMoney");
            return;
        }

        SoundManager.PlaySound("Build");

        GameStats.currentMoney -= _turretBlueprint.level1Cost;
        GameStats.moneySpent += _turretBlueprint.level1Cost;
        statsUI.ChangeMoney(-_turretBlueprint.level1Cost);

        //Build Turret
        GameObject _turret = (GameObject)Instantiate(_turretBlueprint.level1Prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        //Build Effect
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        turretBlueprint = _turretBlueprint;

        DataRecorder.turretPostions.Add(GetBuildPosition());

        if (turretBlueprint.name == "Machine Gun")
        {
            GameStats.mgTurretLvl1Built++;
        }
        else if (turretBlueprint.name == "Rocket")
        {
            GameStats.rocketTurretLvl1Built++;
        }
        else if (turretBlueprint.name == "Laser")
        {
            GameStats.laserTurretLvl1Built++;
        }
    }

    public void UpgradeTurret()
    {
        if (upgradeLevel == 1)
        {
            sellUpgrades = Mathf.FloorToInt(turretBlueprint.level2Cost / 2);

            GameStats.currentUpgrades -= turretBlueprint.level2Cost;
            GameStats.upgradesSpent += turretBlueprint.level2Cost;
            statsUI.ChangeMoney(-turretBlueprint.level2Cost);

            //Delete old turret
            Destroy(turret);

            SoundManager.PlaySound("Build");

            //Create new upgraded turret
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.level2Prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            //Build effect
            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            //Update Stats
            if (turretBlueprint.name == "Machine Gun")
            {
                GameStats.mgTurretLvl1Built--;
                GameStats.mgTurretLvl2Built++;
            }
            else if (turretBlueprint.name == "Rocket")
            {
                GameStats.rocketTurretLvl1Built--;
                GameStats.rocketTurretLvl2Built++;
            }
            else if (turretBlueprint.name == "Laser")
            {
                GameStats.laserTurretLvl1Built--;
                GameStats.laserTurretLvl2Built++;
            }

            upgradeLevel = 2;
        }
        else if (upgradeLevel == 2)
        {
            sellUpgrades = Mathf.FloorToInt(turretBlueprint.level3Cost / 2);

            GameStats.currentUpgrades -= turretBlueprint.level3Cost;
            GameStats.upgradesSpent += turretBlueprint.level3Cost;
            statsUI.ChangeMoney(-turretBlueprint.level3Cost);

            //Delete old turret
            Destroy(turret);

            SoundManager.PlaySound("Build");

            //Create new upgraded turret
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.level3Prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            //Build effect
            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            //Update Stats
            if (turretBlueprint.name == "Machine Gun")
            {
                GameStats.mgTurretLvl2Built--;
                GameStats.mgTurretLvl3Built++;
            }
            else if (turretBlueprint.name == "Rocket")
            {
                GameStats.rocketTurretLvl2Built--;
                GameStats.rocketTurretLvl3Built++;
            }
            else if (turretBlueprint.name == "Laser")
            {
                GameStats.laserTurretLvl2Built--;
                GameStats.laserTurretLvl3Built++;
            }

            upgradeLevel = 3;
        }
    }

    public void SellTurret()
    {
        showRange = false;
        GameStats.currentUpgrades += sellUpgrades;
        sellUpgrades = 0;

        GameStats.currentMoney += turretBlueprint.sellCost;

        //Spawn Effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);

        //Update Stats
        if (turretBlueprint.name == "Machine Gun")
        {
            if (upgradeLevel == 1)
            {
                GameStats.mgTurretLvl1Sold++;
                GameStats.mgTurretLvl1Built--;
            }
            else if (upgradeLevel == 2)
            {
                GameStats.mgTurretLvl2Sold++;
                GameStats.mgTurretLvl2Built--;
            }
            else if (upgradeLevel == 3)
            {
                GameStats.mgTurretLvl3Sold++;
                GameStats.mgTurretLvl3Built--;
            }
            else if (upgradeLevel == 4)
            {
                GameStats.mgTurretBurstSold++;
                GameStats.mgTurretBurstBuilt--;
            }
            else if (upgradeLevel == 5)
            {
                GameStats.mgTurretSniperSold++;
                GameStats.mgTurretSniperBuilt--;
            }

        }
        else if (turretBlueprint.name == "Rocket")
        {
            if (upgradeLevel == 1)
            {
                GameStats.rocketTurretLvl1Sold++;
                GameStats.rocketTurretLvl1Built--;
            }
            else if (upgradeLevel == 2)
            {
                GameStats.rocketTurretLvl2Sold++;
                GameStats.rocketTurretLvl2Built--;
            }
            else if (upgradeLevel == 3)
            {
                GameStats.rocketTurretLvl3Sold++;
                GameStats.rocketTurretLvl3Built--;
            }
            else if (upgradeLevel == 4)
            {
                GameStats.rocketTurretRFSold++;
                GameStats.rocketTurretRFBuilt--;
            }
            else if (upgradeLevel == 5)
            {
                GameStats.rocketTurretNukeSold++;
                GameStats.rocketTurretNukeBuilt--;
            }
        }
        else if (turretBlueprint.name == "Laser")
        {
            if (upgradeLevel == 1)
            {
                GameStats.laserTurretLvl1Sold++;
                GameStats.laserTurretLvl1Built--;
            }
            else if (upgradeLevel == 2)
            {
                GameStats.laserTurretLvl2Sold++;
                GameStats.laserTurretLvl2Built--;
            }
            else if (upgradeLevel == 3)
            {
                GameStats.laserTurretLvl3Sold++;
                GameStats.laserTurretLvl3Built--;
            }
            else if (upgradeLevel == 4)
            {
                GameStats.laserTurretAOESold++;
                GameStats.laserTurretAOEBuilt--;
            }
            else if (upgradeLevel == 5)
            {
                GameStats.laserTurretDamageSold++;
                GameStats.laserTurretDamageBuilt--;
            }
        }

        turretBlueprint = null;
        upgradeLevel = 1;
    }

    public void UpgradeSpecial(int specialType)
    {
        sellUpgrades = Mathf.FloorToInt(turretBlueprint.specialCost / 2);

        GameStats.currentUpgrades -= turretBlueprint.specialCost;
        GameStats.upgradesSpent += turretBlueprint.specialCost;
        statsUI.ChangeUpgrades(-turretBlueprint.specialCost);

        Destroy(turret);

        SoundManager.PlaySound("Build");
        if (specialType == 1)
        {
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.special1Prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            //Update Stats
            if (turretBlueprint.name == "Machine Gun")
            {
                GameStats.mgTurretLvl3Built--;
                GameStats.mgTurretBurstBuilt++;
            }
            else if (turretBlueprint.name == "Rocket")
            {
                GameStats.rocketTurretLvl3Built--;
                GameStats.rocketTurretRFBuilt++;
            }
            else if (turretBlueprint.name == "Laser")
            {
                GameStats.laserTurretLvl3Built--;
                GameStats.laserTurretAOEBuilt++;
            }
            upgradeLevel = 4;
        }
        else
        {
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.special2Prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            //Update Stats
            if (turretBlueprint.name == "Machine Gun")
            {
                GameStats.mgTurretLvl3Built--;
                GameStats.mgTurretSniperBuilt++;
            }
            else if (turretBlueprint.name == "Rocket")
            {
                GameStats.rocketTurretLvl3Built--;
                GameStats.rocketTurretNukeBuilt++;
            }
            else if (turretBlueprint.name == "Laser")
            {
                GameStats.laserTurretLvl3Built--;
                GameStats.laserTurretDamageBuilt++;
            }
            upgradeLevel = 5;
        }
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    //Tutorial Functions
    public void TutorialOn()
    {
        rend.materials[1].color = hoverColourYes;
    }

    public void TutorialOff()
    {
        rend.materials[1].color = startColour;
    }

    private void Update()
    {
        currentRange = shop.GetCurrentRange();

        if (showRange && turret == null)
        {
            if (buildManager.GetTurretToBuild() != null)
            {
                string selectedTurret = buildManager.GetTurretToBuild().name;

                if (selectedTurret == "Machine Gun")
                {
                    mgPreview.SetActive(true);
                }
                else if (selectedTurret == "Rocket")
                {
                    rocketPreview.SetActive(true);
                }
                else if (selectedTurret == "Laser")
                { 
                    laserPreview.SetActive(true);
                }
            }

            currentRangeIMG.enabled = true;
            //Update Range (Image Canvas)
            currentRangeIMG.transform.localScale = new Vector3(currentRange * rangeMultiplier, currentRange * rangeMultiplier, currentRange * rangeMultiplier);
        }
        else
        {
            currentRangeIMG.enabled = false;

            mgPreview.SetActive(false);
            rocketPreview.SetActive(false);
            laserPreview.SetActive(false);
        }

        if (shop.GetUpdateNode())
        {
            mgPreview.SetActive(false);
            rocketPreview.SetActive(false);
            laserPreview.SetActive(false);

            shop.SetUpdateNode(false);
        }
    }

    public void SelectNode()
    {
        rend.materials[1].color = hoverColourYes;
        selected = true;
    }

    public void DeselectNode()
    {
        rend.materials[1].color = startColour;
        selected = false;
        currentRangeIMG.enabled = false;
    }

    public void HideRange()
    {
        currentRangeIMG.enabled = false;
    }
}
