using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script is of my own design and shows what enemies are coming up next.

public class RoundInfo : MonoBehaviour
{
    [SerializeField] GameObject e_standard;
    [SerializeField] GameObject e_light;
    [SerializeField] GameObject e_tank;
    [SerializeField] GameObject e_flying;

    [SerializeField] GameObject x1;
    [SerializeField] GameObject x2;
    [SerializeField] GameObject x3;
    [SerializeField] GameObject x4;

    public bool isStandard, isLight, isTank, isFlying;

    private int types = 0;

    public void SetEnemies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            types = 0;
        }

        if (isStandard)
        {
            GameObject enemy = Instantiate(e_standard, transform.position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.transform.localScale = new Vector3(1, 1, 1);
            types++;
        }

        if (isLight)
        {
            GameObject enemy = Instantiate(e_light, transform.position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.transform.localScale = new Vector3(1, 1, 1);
            types++;
        }

        if (isTank)
        {
            GameObject enemy = Instantiate(e_tank, transform.position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.transform.localScale = new Vector3(1, 1, 1);
            types++;
        }

        if (isFlying)
        {
            GameObject enemy = Instantiate(e_flying, transform.position, Quaternion.identity);
            enemy.transform.SetParent(transform);
            enemy.transform.localScale = new Vector3(1, 1, 1);
            types++;
        }

        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            if (types == 1)
            {
                x1.SetActive(true);
                x2.SetActive(false);
                x3.SetActive(false);
                x4.SetActive(false);
            }
            else if (types == 2)
            {
                x1.SetActive(false);
                x2.SetActive(true);
                x3.SetActive(false);
                x4.SetActive(false);
            }
            else if (types == 3)
            {
                x1.SetActive(false);
                x2.SetActive(false);
                x3.SetActive(true);
                x4.SetActive(false);
            }
            else if (types == 4)
            {
                x1.SetActive(false);
                x2.SetActive(false);
                x3.SetActive(false);
                x4.SetActive(true);
            }
            else if (types == 0)
            {
                x1.SetActive(false);
                x2.SetActive(false);
                x3.SetActive(false);
                x4.SetActive(false);
            }
        }
        else
        {
            x1.SetActive(true);
            x2.SetActive(false);
            x3.SetActive(false);
            x4.SetActive(false);
        }
    }
}
