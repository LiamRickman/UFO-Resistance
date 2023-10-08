using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is of my own making and shows upgrade stats when upgrading the turrets.

public class TurretUpgradeStats : MonoBehaviour
{
    [Header("Standard Upgrades")]
    public GameObject standardUI;
    [SerializeField] Text standardTurret;
    [SerializeField] Text standardDamage;
    [SerializeField] Text standardFirerate;
    [SerializeField] Text standardRange;
    [SerializeField] Text standardSlow;
    [SerializeField] Text standardExplosion;
    [SerializeField] Text standardSpecial;
    [SerializeField] Button standardUpgrade;
    [SerializeField] Text standardUpgradeCost;
    [SerializeField] Text standardSellCost;
    [SerializeField] Button standardMaxed;
    private bool hoverUpgrade;

    [Header("Special Upgrades")]
    public GameObject specialUI;
    [SerializeField] Text specialTurret;
    [SerializeField] Text specialDamage;
    [SerializeField] Text specialFirerate;
    [SerializeField] Text specialRange;
    [SerializeField] Text specialSlow;
    [SerializeField] Text specialExplosion;
    [SerializeField] Text specialSpecial;
    [SerializeField] Text specialUpgradeCost;
    [SerializeField] Text specialSellCost;
    [SerializeField] Text special1Text;
    [SerializeField] Text special2Text;
    private int hoverSpecial = 0;

    [Header("Misc.")]
    [SerializeField] NodeUI nodeUI;
    [SerializeField] Shop shop;
    private BuildManager buildManager;
    private Node target;
    private Turret currentTurret;
    private Turret upgradedTurret;
    private bool maxUpgraded;

    private void Start()
    {
        buildManager = BuildManager.instance;
        nodeUI = GameObject.FindGameObjectWithTag("NodeUI").GetComponent<NodeUI>();

        standardMaxed.interactable = false;

        HideUI();
    }

    private void Update()
    {
        if (target != null)
        {
            //Show Correct Upgrade Screen
            if (target.upgradeLevel == 1)
            {
                UpdateText();
                standardUpgradeCost.text = target.turretBlueprint.level2Cost.ToString();
                standardSellCost.text = target.turretBlueprint.sellCost.ToString();
                standardUpgrade.gameObject.SetActive(true);
                standardMaxed.gameObject.SetActive(false);
                maxUpgraded = false;
            }
            else if (target.upgradeLevel == 2)
            {
                UpdateText();
                standardUpgradeCost.text = target.turretBlueprint.level3Cost.ToString();
                standardSellCost.text = target.turretBlueprint.sellCost.ToString();
                standardUpgrade.gameObject.SetActive(true);
                standardMaxed.gameObject.SetActive(false);
                maxUpgraded = false;
            }
            else if (target.upgradeLevel == 3)
            {
                UpdateSpecialText();
                specialUpgradeCost.text = target.turretBlueprint.specialCost.ToString();
                specialSellCost.text = target.turretBlueprint.sellCost.ToString();
                standardUpgrade.gameObject.SetActive(true);
                standardMaxed.gameObject.SetActive(false);
                maxUpgraded = false;
            }
            else if (target.upgradeLevel == 4)
            {
                UpdateText();
                standardSellCost.text = target.turretBlueprint.sellCost.ToString();
                standardUpgrade.gameObject.SetActive(false);
                standardMaxed.gameObject.SetActive(true);
                maxUpgraded = true;

            }
            else if (target.upgradeLevel == 5)
            {
                UpdateText();
                standardSellCost.text = target.turretBlueprint.sellCost.ToString();
                standardUpgrade.gameObject.SetActive(false);
                standardMaxed.gameObject.SetActive(true);
                maxUpgraded = true;
            }

            if (Input.GetMouseButtonDown(1))
            {
                HideUI();
            }
        }
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        //Check what upgrade level the target is to show the correct UI.
        if (target.upgradeLevel == 1 || target.upgradeLevel == 2 || target.upgradeLevel == 4 || target.upgradeLevel == 5)
            ShowStandard();
        else if (target.upgradeLevel == 3)
            ShowSpecial();

        shop.DeselectShop();

        target.SelectNode();
    }

    private void UpdateText()
    {
        currentTurret = target.turret.GetComponent<Turret>();

        if (target.upgradeLevel == 1)
            upgradedTurret = target.turretBlueprint.level2Prefab.GetComponent<Turret>();
        else if (target.upgradeLevel == 2)
            upgradedTurret = target.turretBlueprint.level3Prefab.GetComponent<Turret>();

        if (hoverUpgrade && !maxUpgraded)
        {
            standardTurret.text = "  " + target.turretBlueprint.name.ToUpper() + " (Lvl " + target.upgradeLevel + ")";
            if (upgradedTurret.GetDamage() > currentTurret.GetDamage())
                standardDamage.text = "  - Damage: " + currentTurret.GetDamage() + " > <b><color=lime>" + upgradedTurret.GetDamage() + "</color></b>";
            else if (upgradedTurret.GetDamage() < currentTurret.GetDamage())
                standardDamage.text = "  - Damage: " + currentTurret.GetDamage() + " > <b><color=red>" + upgradedTurret.GetDamage() + "</color></b>";
            else
                standardDamage.text = "  - Damage: " + currentTurret.GetDamage();

            if (upgradedTurret.GetFirerate() > currentTurret.GetFirerate())
                standardFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s" + " > <b><color=lime>" + upgradedTurret.GetFirerate() + "s</color></b>";
            else if (upgradedTurret.GetFirerate() < currentTurret.GetFirerate())
                standardFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s" + " > <b><color=red>" + upgradedTurret.GetFirerate() + "s</color></b>";
            else
                standardFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s";

            if (upgradedTurret.GetRange() > currentTurret.GetRange())
                standardRange.text = "  - Range: " + currentTurret.GetRange() + "m" + " > <b><color=lime>" + upgradedTurret.GetRange() + "m</color></b>";
            else if (upgradedTurret.GetRange() < currentTurret.GetRange())
                standardRange.text = "  - Range: " + currentTurret.GetRange() + "m" + " > <b><color=red>" + upgradedTurret.GetRange() + "m</color></b>";
            else
                standardRange.text = "  - Range: " + currentTurret.GetRange() + "m";

            if (upgradedTurret.GetSlow() > currentTurret.GetSlow())
                standardSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%" + " > <b><color=lime>" + upgradedTurret.GetSlow() * 100f + "%</color></b>";
            else if (upgradedTurret.GetSlow() < currentTurret.GetSlow())
                standardSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%" + " > <b><color=red>" + upgradedTurret.GetSlow() * 100f + "%</color></b>";
            else
                standardSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%";

            if (upgradedTurret.GetExplosionRadius() > currentTurret.GetExplosionRadius())
                standardExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m" + " > <b><color=lime>" + upgradedTurret.GetExplosionRadius() + "m</color></b>";
            else if (upgradedTurret.GetExplosionRadius() < currentTurret.GetExplosionRadius())
                standardExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m" + " > <b><color=red>" + upgradedTurret.GetExplosionRadius() + "m</color></b>";
            else
                standardExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m";

            if (upgradedTurret.GetSpecial() != currentTurret.GetSpecial())
                standardSpecial.text = "  - Special: " + currentTurret.GetSpecial() + " > <b><color=lime>" + upgradedTurret.GetSpecial() + "</color></b>";
            else
                standardSpecial.text = "  - Special: " + currentTurret.GetSpecial() + " > <b><color=white>" + upgradedTurret.GetSpecial() + "</color></b>";
        }
        else if (maxUpgraded)
        {
            standardTurret.text = "  " + target.turretBlueprint.name.ToUpper() + " (Lvl " + target.upgradeLevel + ")";

            standardDamage.text = "  - Damage: " + currentTurret.GetDamage();
            standardFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s";
            standardRange.text = "  - Range: " + currentTurret.GetRange() + "m";
            standardSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%";
            standardExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m";
            standardSpecial.text = "  - Special: " + currentTurret.GetSpecial();
        }
        else
        {
            standardTurret.text = "  " + target.turretBlueprint.name.ToUpper() + " (Lvl " + target.upgradeLevel + ")";

            standardDamage.text = "  - Damage: " + currentTurret.GetDamage();
            standardFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s";
            standardRange.text = "  - Range: " + currentTurret.GetRange() + "m";
            standardSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%";
            standardExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m";
            standardSpecial.text = "  - Special: " + currentTurret.GetSpecial();
        }
    }

    private void UpdateSpecialText()
    {
        currentTurret = target.turret.GetComponent<Turret>();

        special1Text.text = target.turretBlueprint.special1Prefab.GetComponent<Turret>().special;
        special2Text.text = target.turretBlueprint.special2Prefab.GetComponent<Turret>().special;

        if (hoverSpecial != 0)
        {
            if (hoverSpecial == 1)
                upgradedTurret = target.turretBlueprint.special1Prefab.GetComponent<Turret>();
            else if (hoverSpecial == 2)
                upgradedTurret = target.turretBlueprint.special2Prefab.GetComponent<Turret>();

            specialTurret.text = "  " + target.turretBlueprint.name.ToUpper() + " (Lvl " + target.upgradeLevel + ")";
            if (upgradedTurret.GetDamage() > currentTurret.GetDamage())
                specialDamage.text = "  - Damage: " + currentTurret.GetDamage() + " > <b><color=lime>" + upgradedTurret.GetDamage() + "</color></b>";
            else if (upgradedTurret.GetDamage() < currentTurret.GetDamage())
                specialDamage.text = "  - Damage: " + currentTurret.GetDamage() + " > <b><color=red>" + upgradedTurret.GetDamage() + "</color></b>";
            else
                specialDamage.text = "  - Damage: " + currentTurret.GetDamage();

            if (upgradedTurret.GetFirerate() > currentTurret.GetFirerate())
                specialFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s" + " > <b><color=lime>" + upgradedTurret.GetFirerate() + "s</color></b>";
            else if (upgradedTurret.GetFirerate() < currentTurret.GetFirerate())
                specialFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s" + " > <b><color=red>" + upgradedTurret.GetFirerate() + "s</color></b>";
            else
                specialFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s";

            if (upgradedTurret.GetRange() > currentTurret.GetRange())
                specialRange.text = "  - Range: " + currentTurret.GetRange() + "m" + " > <b><color=lime>" + upgradedTurret.GetRange() + "m</color></b>";
            else if (upgradedTurret.GetRange() < currentTurret.GetRange())
                specialRange.text = "  - Range: " + currentTurret.GetRange() + "m" + " > <b><color=red>" + upgradedTurret.GetRange() + "m</color></b>";
            else
                specialRange.text = "  - Range: " + currentTurret.GetRange() + "m";

            if (upgradedTurret.GetSlow() > currentTurret.GetSlow())
                specialSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%" + " > <b><color=lime>" + upgradedTurret.GetSlow() * 100f + "%</color></b>";
            else if (upgradedTurret.GetSlow() < currentTurret.GetSlow())
                specialSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%" + " > <b><color=red>" + upgradedTurret.GetSlow() * 100f + "%</color></b>";
            else
                specialSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%";

            if (upgradedTurret.GetExplosionRadius() > currentTurret.GetExplosionRadius())
                specialExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m" + " > <b><color=lime>" + upgradedTurret.GetExplosionRadius() + "m</color></b>";
            else if (upgradedTurret.GetExplosionRadius() < currentTurret.GetExplosionRadius())
                specialExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m" + " > <b><color=red>" + upgradedTurret.GetExplosionRadius() + "m</color></b>";
            else
                specialExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m";

            if (upgradedTurret.GetSpecial() != currentTurret.GetSpecial())
                specialSpecial.text = "  - Special: " + currentTurret.GetSpecial() + " > <b><color=lime>" + upgradedTurret.GetSpecial() + "</color></b>";
            else
                specialSpecial.text = "  - Special: " + currentTurret.GetSpecial() + " > <b><color=white>" + upgradedTurret.GetSpecial() + "</color></b>";
        }
        else
        {
            specialTurret.text = "  " + target.turretBlueprint.name.ToUpper() + " (Lvl " + target.upgradeLevel + ")";

            specialDamage.text = "  - Damage: " + currentTurret.GetDamage();
            specialFirerate.text = "  - Firerate: " + currentTurret.GetFirerate() + "s";
            specialRange.text = "  - Range: " + currentTurret.GetRange() + "m";
            specialSlow.text = "  - Slow Amount: " + currentTurret.GetSlow() * 100f + "%";
            specialExplosion.text = "  - Explosion Range: " + currentTurret.GetExplosionRadius() + "m";
            specialSpecial.text = "  - Special: " + currentTurret.GetSpecial();
        }
    }


    public void PressUpgrade()
    {
        //Lets tutorial progress
        Tutorial.turretUpgraded = true;
        //Main
        if (target.upgradeLevel == 1)
        {
            if (GameStats.currentUpgrades < target.turretBlueprint.level2Cost)
            {
                Debug.Log("Not enough tokens to upgrade turret");
                SoundManager.PlaySound("NoMoney");
                return;
            }
            else
            {
                target.UpgradeTurret();
                BuildManager.instance.DeselectNode();
            }

            SetTarget(target);
        }
        else if (target.upgradeLevel == 2)
        {
            if (GameStats.currentUpgrades < target.turretBlueprint.level3Cost)
            {
                Debug.Log("Not enough tokens to upgrade turret");
                SoundManager.PlaySound("NoMoney");
                return;
            }
            else
            {
                target.UpgradeTurret();
                BuildManager.instance.DeselectNode();
            }

            SetTarget(target);
        }
    }

    public void PressSell()
    {
        Tutorial.turretSold = true;

        HideUI();
        nodeUI.Hide();
        target.SellTurret();
        target = null;
        BuildManager.instance.DeselectNode();
    }

    public void PressSpecial()
    {
        if (GameStats.currentUpgrades < target.turretBlueprint.specialCost)
        {
            Debug.Log("Not enough tokens to upgrade turret");
            SoundManager.PlaySound("NoMoney");
            return;
        }
        else
        {
            target.UpgradeSpecial(hoverSpecial);
            SetTarget(target);

            BuildManager.instance.DeselectNode();
        }
    }

    public void HoverSpecial1()
    {
        hoverSpecial = 1;
        nodeUI.SetSpecial(hoverSpecial);

        nodeUI.ShowUpgradedRange(true);
    }

    public void HoverSpecial2()
    {
        hoverSpecial = 2;
        nodeUI.SetSpecial(hoverSpecial);

        nodeUI.ShowUpgradedRange(true);
    }
    public void UnhoverSpecial()
    {
        hoverSpecial = 0;
        nodeUI.SetSpecial(hoverSpecial);

        nodeUI.ShowUpgradedRange(false);
    }

    public void HoverUpgrade()
    {
        hoverUpgrade = true;
        nodeUI.ShowUpgradedRange(true);
    }

    public void UnhoverUpgrade()
    {
        hoverUpgrade = false;
        nodeUI.ShowUpgradedRange(false);
    }
    public void ShowStandard()
    {
        standardUI.SetActive(true);
        specialUI.SetActive(false);
    }

    public void ShowSpecial()
    {
        standardUI.SetActive(false);
        specialUI.SetActive(true);
    }

    public void HideUI()
    {
        standardUI.SetActive(false);
        specialUI.SetActive(false);

        hoverUpgrade = false;
        hoverSpecial = 0;
        nodeUI.SetSpecial(hoverSpecial);
        nodeUI.ShowUpgradedRange(false);

        nodeUI.Hide();

        if (target != null)
        {
            target.DeselectNode();
        }
        
    }
}
