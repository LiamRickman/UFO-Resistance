using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is a script of my own design to control the tutorial.

public class Tutorial : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform tutorialPrompts;
    private Transform[] prompts;
    [SerializeField] CameraController cam;
    private Vector3 defaultCamPos = new Vector3(28, 65, -60);
    public int promptIndex = 0;
    private Vector3 newCamPos;
    private bool canSkip = true;
    public static bool nodeUIDisabled = true;
    [SerializeField] Button nodeUISell;
    [SerializeField] Button nodeUIUpgrade;
    public static bool turretUpgraded = false;
    public static bool turretSold = false;
    [SerializeField] SceneFader sceneFader;

    public static bool enemyDied = false;
    public bool ignoreDeaths = false;
    private bool followEnemy = false;
    private Transform enemy;


    private bool roundSpawned = false;

    public static bool canPlay = false;

    private bool canKillEnemy = true;

    [Header("Node References")]
    [SerializeField] Node mgNode;
    private NodeUI nodeUI;
    private bool nodeActiveOnce = false;

    [Header("HUD References")]
    [SerializeField] GameObject shop;
    [SerializeField] Button mgTurret;
    [SerializeField] Button rocketTurret;
    [SerializeField] Button laserTurret;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject controlsAll;
    [SerializeField] Button play;
    [SerializeField] Button fast;
    [SerializeField] GameObject money;
    [SerializeField] GameObject upgrades;
    [SerializeField] GameObject health;
    [SerializeField] GameObject rounds;
    [SerializeField] GameObject roundInfo;
    private RoundControls rc;
    [SerializeField] TurretStats turretStats;
    [SerializeField] GameObject barTop;
    [SerializeField] GameObject barShop;
    private bool allowMoveCamera = false;


    private void Start()
    {
        Discord.AddToFile("Summary.txt", "LOADED: Tutorial");

        rc = controls.gameObject.GetComponent<RoundControls>();
        nodeUI = GameObject.Find("NodeUI").GetComponent<NodeUI>();

        nodeUIDisabled = true;

        play.interactable = false;
        fast.interactable = false;

        mgTurret.interactable = false;
        rocketTurret.interactable = false;
        laserTurret.interactable = false;

        nodeUISell.interactable = false;
        nodeUIUpgrade.interactable = false;

        shop.SetActive(false);
        money.SetActive(false);
        upgrades.SetActive(false);
        health.SetActive(false);
        rounds.SetActive(false);
        roundInfo.SetActive(false);
        controlsAll.SetActive(false);
        barTop.SetActive(false);
        barShop.SetActive(false);

        cam.SetCanMove(false);

        cam.transform.position = defaultCamPos;

        defaultCamPos = cam.transform.position;
        newCamPos = defaultCamPos;

        SelectPrompts(tutorialPrompts);
        ActivatePrompt();
    }

    private void Update()
    {
        int tempPromptIndex = promptIndex - 1;

        if (canSkip && prompts[tempPromptIndex].gameObject.GetComponent<TutorialPrompt>().GetTextFinished())
        {
            if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                ActivatePrompt();
            }
        }

        if (!followEnemy && !allowMoveCamera)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, newCamPos, Time.deltaTime / 0.5f);
            cam.transform.rotation = Quaternion.Euler(65, cam.transform.rotation.y, cam.transform.rotation.z);
        }

        UpdatePrompts();
    }

    private void CameraFollow(Transform target)
    {
        Vector3 followPos = new Vector3(target.position.x, 10, target.position.z - 7.5f);

        cam.transform.position = Vector3.Lerp(cam.transform.position, followPos, Time.deltaTime / 0.1f);
        cam.transform.rotation = Quaternion.Euler(45, cam.transform.rotation.y, cam.transform.rotation.z);
    }


    private void SelectPrompts(Transform _selected)
    {
        Transform[] tempPrompts = _selected.GetComponentsInChildren<Transform>();

        prompts = new Transform[tempPrompts.Length / 3];

        int tempPromptIndex = 0;

        for (int i = 0; i < tempPrompts.Length; i++)
        {
            if (tempPrompts[i].CompareTag("TutorialPrompt"))
            {
                prompts[tempPromptIndex] = tempPrompts[i];
                prompts[tempPromptIndex].gameObject.SetActive(false);
                tempPromptIndex++;
            }
        }
    }

    public void ActivatePrompt()
    {
        if (promptIndex > 0)
            prompts[promptIndex - 1].gameObject.SetActive(false);

        prompts[promptIndex].gameObject.SetActive(true);

        newCamPos = prompts[promptIndex].GetComponent<TutorialPrompt>().newCamPos;

        promptIndex++;

    }

    //Add hud updates when the correct prompt appears
    private void UpdatePrompts()
    {
        if (promptIndex == 5)
        {
            barTop.SetActive(true);
            rounds.SetActive(true);
            roundInfo.SetActive(true);
        }
        else if (promptIndex == 6)
        {
            health.SetActive(true);
        }
        else if (promptIndex == 7)
        {
            money.SetActive(true);
            upgrades.SetActive(true);
        }
        else if (promptIndex == 8)
        {
            controlsAll.SetActive(true);
        }
        else if (promptIndex == 9)
        {
            barShop.SetActive(true);
            shop.SetActive(true);
        }
        else if (promptIndex == 10)
        {
            cam.SetCanMove(true);
            allowMoveCamera = true;
        }
        else if (promptIndex == 11)
        {
            cam.SetCanMove(false);
            allowMoveCamera = false;
            canSkip = false;
            mgTurret.interactable = true;
            mgNode.TutorialOn();

            if (mgNode.turret != null)
            {
                ActivatePrompt();
                turretStats.StatsOff();
            }

        }
        else if (promptIndex == 12)
        {
            play.interactable = true;
            canPlay = true;
            mgTurret.interactable = false;
            mgNode.TutorialOff();
        }
        else if (promptIndex == 13)
        {
            canPlay = false;
            play.interactable = false;
            if (RoundSpawner.enemiesAlive == 0)
            {
                ActivatePrompt();
            }
        }
        else if (promptIndex == 14)
        {
            nodeUIDisabled = false;
            ActivatePrompt();
        }
        else if (promptIndex == 15)
        {
            canSkip = false;

            nodeUIUpgrade.interactable = true;

            if (turretUpgraded)
            {
                ActivatePrompt();
            }
        }
        else if (promptIndex == 16)
        {
            nodeUIUpgrade.interactable = false;

            if (!nodeActiveOnce)
            {
                nodeUI.SetTarget(mgNode);
                nodeActiveOnce = true;
            }

            canSkip = true;
        }
        else if (promptIndex == 17)
        {
            canSkip = false;


            nodeUISell.interactable = true;

            if (turretSold)
            {
                ActivatePrompt();
                nodeUISell.interactable = false;
            }
        }
        else if (promptIndex == 18)
        {
            canSkip = true;
        }
        else if (promptIndex == 19)
        {
            canSkip = false;

            followEnemy = true;

            if (!roundSpawned)
            {
                canPlay = false;
                rc.PressPlay();
                roundSpawned = true;
            }

            if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            }


            if (enemy != null)
            {
                CameraFollow(enemy);
                canKillEnemy = true;
            }

            int tempPromptIndex = promptIndex - 1;

            if (prompts[tempPromptIndex].gameObject.GetComponent<TutorialPrompt>().GetTextFinished() && (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && canKillEnemy))
            {
                enemy.gameObject.GetComponent<Enemy>().TakeDamage(1000);
                canKillEnemy = false;
            }

            if (RoundSpawner.enemiesAlive == 0)
            {
                roundSpawned = false;
                ActivatePrompt();
            }

        }
        else if (promptIndex == 20)
        {
            if (!roundSpawned)
            {
                canPlay = false;
                rc.PressPlay();
                roundSpawned = true;
            }


            if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            }

            if (enemy != null)
            {
                CameraFollow(enemy);
                canKillEnemy = true;
            }

            int tempPromptIndex = promptIndex - 1;

            if (prompts[tempPromptIndex].gameObject.GetComponent<TutorialPrompt>().GetTextFinished() && (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && canKillEnemy))
            {
                enemy.gameObject.GetComponent<Enemy>().TakeDamage(1000);
                canKillEnemy = false;
                roundSpawned = false;
            }

            if (RoundSpawner.enemiesAlive == 0)
            {
                ActivatePrompt();
                roundSpawned = false;
            }

        }
        else if (promptIndex == 21)
        {
            if (!roundSpawned)
            {
                canPlay = false;
                rc.PressPlay();
                roundSpawned = true;
            }


            if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            }

            if (enemy != null)
            {
                CameraFollow(enemy);
                canKillEnemy = true;
            }

            int tempPromptIndex = promptIndex - 1;

            if (prompts[tempPromptIndex].gameObject.GetComponent<TutorialPrompt>().GetTextFinished() && (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && canKillEnemy))
            {
                enemy.gameObject.GetComponent<Enemy>().TakeDamage(1000);
                canKillEnemy = false;
                roundSpawned = false;
            }

            if (RoundSpawner.enemiesAlive == 0)
            {
                ActivatePrompt();
                roundSpawned = false;
            }
        }
        else if (promptIndex == 22)
        {
            if (!roundSpawned)
            {
                canPlay = false;
                rc.PressPlay();
                roundSpawned = true;
            }


            if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            }

            if (enemy != null)
            {
                CameraFollow(enemy);
                canKillEnemy = true;
                enemy.gameObject.GetComponent<Enemy>().Slow(0.5f);
            }

            int tempPromptIndex = promptIndex - 1;

            if (prompts[tempPromptIndex].gameObject.GetComponent<TutorialPrompt>().GetTextFinished() && (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && canKillEnemy))
            {
                enemy.gameObject.GetComponent<Enemy>().TakeDamage(1000);
                canKillEnemy = false;
                roundSpawned = false;
            }

            if (RoundSpawner.enemiesAlive == 0)
            {
                ActivatePrompt();
                roundSpawned = false;
            }
        }
        else if (promptIndex == 23)
        {
            if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {
                Time.timeScale = 1f;
                //int levelReached = PlayerPrefs.GetInt(Discord.username + "_LevelReached") + 1;
                //PlayerPrefs.SetInt(Discord.username + "_LevelReached", levelReached);

                Discord.AddToFile("Summary.txt", "COMPLETED: Tutorial (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
                Discord.EditFile("Summary.txt", "Completed Tutorial: Yes", 3, false);
                GameStats.tutorialCompleted = true;
                PlayerPrefs.SetInt(Discord.username + "_TutorialComplete", 1);

                sceneFader.LoadScene("Menu_LevelSelect");
            }

        }
    }
}
