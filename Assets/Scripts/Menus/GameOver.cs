using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class GameOver : MonoBehaviour
{
    [SerializeField] Text roundsText;
    [SerializeField] SceneFader sceneFader;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < GameStats.wavesCompleted)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }


    public void PressRetry()
    {
        sceneFader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PressMenu()
    {
        SceneManager.LoadScene("Menu_Main");
    }
}
