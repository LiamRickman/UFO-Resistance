using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class NodeUI : MonoBehaviour
{
    public GameObject rangeUI;
    public GameObject upgradedRangeUI;

    private Node target;
    [SerializeField] Shop shop;

    [SerializeField] Image rangeImage;
    [SerializeField] Image upgradeRangeImage;
    [SerializeField] float rangeMultiplier;

    private float range;
    private float upgradeRange;

    private int special = 0;

    public void SetTarget(Node _target)
    {
        if (target != null)
        {
            target.DeselectNode();
        }

        target = _target;

        transform.position = target.GetBuildPosition();

        rangeUI.SetActive(true);
    }

    private void Update()
    {
        if (target != null)
        {
            if (target.turret != null)
            {
                if (target.upgradeLevel == 1)
                {
                    range = target.turretBlueprint.level1Prefab.GetComponent<Turret>().range;
                    upgradeRange = target.turretBlueprint.level2Prefab.GetComponent<Turret>().range;
                }
                else if (target.upgradeLevel == 2)
                {
                    range = target.turretBlueprint.level2Prefab.GetComponent<Turret>().range;
                    upgradeRange = target.turretBlueprint.level3Prefab.GetComponent<Turret>().range;
                }
                else if (target.upgradeLevel == 3)
                {
                    range = target.turretBlueprint.level3Prefab.GetComponent<Turret>().range;

                    if (special == 1)
                        upgradeRange = target.turretBlueprint.special1Prefab.GetComponent<Turret>().range;
                    else if (special == 2)
                        upgradeRange = target.turretBlueprint.special2Prefab.GetComponent<Turret>().range;
                    else
                        upgradeRange = 0;
                }
                else if (target.upgradeLevel == 4)
                {
                    range = target.turretBlueprint.special1Prefab.GetComponent<Turret>().range;
                    upgradeRange = 0;
                }
                else if (target.upgradeLevel == 5)
                {
                    range = target.turretBlueprint.special2Prefab.GetComponent<Turret>().range;
                    upgradeRange = 0;
                }

                //Update Range (Image Canvas)
                rangeImage.transform.localScale = new Vector3(range * rangeMultiplier, range * rangeMultiplier, range * rangeMultiplier);
                upgradeRangeImage.transform.localScale = new Vector3(upgradeRange * rangeMultiplier, upgradeRange * rangeMultiplier, upgradeRange * rangeMultiplier);
            }
        }
        else
        {
            range = 0;
            upgradeRange = 0;
        }

        if (Tutorial.nodeUIDisabled)
        {
            rangeUI.SetActive(false);
        }
    }


    public void Hide()
    {
        rangeUI.SetActive(false);
        target = null;
    }

    public void SetSpecial(int _new)
    {
        special = _new;
    }

    public void ShowUpgradedRange(bool show)
    {
        upgradedRangeUI.SetActive(show);
    }
}
