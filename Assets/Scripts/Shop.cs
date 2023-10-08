using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class Shop : MonoBehaviour
{
    public TurretBlueprint mgTurret;
    [SerializeField] Text standardText;
    [SerializeField] Button mgButton;

    public TurretBlueprint laserTurret;
    [SerializeField] Text laserText;
    [SerializeField] Button laserButton;

    public TurretBlueprint rocketTurret;
    [SerializeField] Text rocketText;
    [SerializeField] Button rocketButton;

    [Header("Misc References")]
    public float currentRange = 1f;
    BuildManager buildManager;
    [SerializeField] TurretStats turretStats;
    [SerializeField] TurretUpgradeStats turretUI;
    [SerializeField] Color selectedColour;
    private bool updateNode = false;
    private string selectedTurret = null;
    [SerializeField] NodeUI nodeUI;


    private void Start()
    {
        buildManager = BuildManager.instance;

        nodeUI = GameObject.FindGameObjectWithTag("NodeUI").GetComponent<NodeUI>();

        //Update Shop Costs
        standardText.text = mgTurret.level1Cost.ToString();
        rocketText.text = rocketTurret.level1Cost.ToString();
        laserText.text = laserTurret.level1Cost.ToString();


    }
    public void SelectMGTurret()
    {
        if (selectedTurret == "MG")
        {
            DeselectShop();
            return;
        }
            
        selectedTurret = "MG";

        buildManager.SelectTurretToBuild(mgTurret);
        currentRange = mgTurret.level1Range;
        SelectButton(mgButton);
        DeselectButton(laserButton);
        DeselectButton(rocketButton);

        turretUI.HideUI();
        nodeUI.Hide();
    }

    public void SelectRocketTurret()
    {
        if (selectedTurret == "Rocket")
        {
            DeselectShop();
            return;
        }

        selectedTurret = "Rocket";

        buildManager.SelectTurretToBuild(rocketTurret);
        currentRange = rocketTurret.level1Range;
        SelectButton(rocketButton);
        DeselectButton(mgButton);
        DeselectButton(laserButton);

        turretUI.HideUI();
        nodeUI.Hide();
    }

    public void SelectLaserTurret()
    {
        if (selectedTurret == "Laser")
        {
            DeselectShop();
            return;
        }

        selectedTurret = "Laser";

        buildManager.SelectTurretToBuild(laserTurret);
        currentRange = laserTurret.level1Range;
        SelectButton(laserButton);
        DeselectButton(mgButton);
        DeselectButton(rocketButton);

        turretUI.HideUI();
        nodeUI.Hide();
    }

    public float GetCurrentRange()
    {
        return currentRange;
    }

    private void Update()
    {
        if (buildManager.GetTurretToBuild() != null)
        {
            turretStats.StatsOn();
        }


        if (Input.GetMouseButtonDown(1))
        {
            DeselectShop();
        }
    }
    private void SelectButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = selectedColour;
        button.colors = colors;
    }

    private void DeselectButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;
    }

    public void DeselectShop()
    {
        updateNode = true;

        selectedTurret = null;

        buildManager.SelectTurretToBuild(null);
        turretStats.StatsOff();
        DeselectButton(mgButton);
        DeselectButton(laserButton);
        DeselectButton(rocketButton);
    }

    public bool GetUpdateNode()
    {
        return updateNode;
    }

    public void SetUpdateNode(bool update)
    {
        updateNode = update;
    }
}
