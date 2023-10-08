using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a script of my own making to display the turret stats when buying a new turret.
public class TurretStats : MonoBehaviour
{
    private BuildManager buildManager;

    [SerializeField] Text turret;
    [SerializeField] Text damage;
    [SerializeField] Text firerate;
    [SerializeField] Text range;
    [SerializeField] Text slowAmount;
    [SerializeField] Text explosionRadius;
    [SerializeField] Text special;

    private Turret currentTurretStats;

    private int turretLevel = 1;

    [SerializeField] GameObject statsBox;

    private void Start()
    {
        buildManager = BuildManager.instance;

        statsBox.SetActive(false);
    }

    private void Update()
    {
        if (buildManager.GetTurretToBuild() != null)
        {      
            UpdateStats();
        }
    }

    private void UpdateStats()
    {
        string turretName = buildManager.GetTurretToBuild().name.ToUpper();

        currentTurretStats = buildManager.GetTurretToBuild().level1Prefab.gameObject.GetComponent<Turret>();

        turret.text = "  " + turretName;

        if (turretName == "MACHINE GUN")
        {
            turret.text = "  MACHINE GUN (Lvl " + turretLevel + ")";
            damage.text = "  - Damage: " + currentTurretStats.GetDamage();
            firerate.text = "  - Firerate: " + currentTurretStats.GetFirerate() + "s";
            range.text = "  - Range: " + currentTurretStats.GetRange() + "m";
            slowAmount.text = "  - Slow Amount: " + currentTurretStats.GetSlow() * 100f + "%";
            explosionRadius.text = "  - Explosion Range: " + currentTurretStats.GetExplosionRadius() + "m";
            special.text = "  - Special: " + currentTurretStats.GetSpecial();
        }
        else if (turretName == "LASER")
        {
            turret.text = "  LASER (Lvl " + turretLevel + ")";
            damage.text = "  - Damage: " + currentTurretStats.GetDamage();
            firerate.text = "  - Firerate: " + currentTurretStats.GetFirerate() + "s";
            range.text = "  - Range: " + currentTurretStats.GetRange() + "m";
            slowAmount.text = "  - Slow Amount: " + currentTurretStats.GetSlow() * 100f + "%";
            explosionRadius.text = "  - Explosion Range: " + currentTurretStats.GetExplosionRadius() + "m";
            special.text = "  - Special: " + currentTurretStats.GetSpecial();
        }
        else if (turretName == "ROCKET")
        {
            turret.text = "  ROCKET (Lvl " + turretLevel + ")";
            damage.text = "  - Damage: " + currentTurretStats.GetDamage();
            firerate.text = "  - Firerate: " + currentTurretStats.GetFirerate() + "s";
            range.text = "  - Range: " + currentTurretStats.GetRange() + "m";
            slowAmount.text = "  - Slow Amount: " + currentTurretStats.GetSlow() * 100f + "%";
            explosionRadius.text = "  - Explosion Range: " + currentTurretStats.GetExplosionRadius() + "m";
            special.text = "  - Special: " + currentTurretStats.GetSpecial();
        }
    }

    public void StatsOn()
    {
        statsBox.SetActive(true);
    }
    public void StatsOff()
    {
        statsBox.SetActive(false);
    }

}
