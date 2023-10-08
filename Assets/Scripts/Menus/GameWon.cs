using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class GameWon : MonoBehaviour
{

    [SerializeField] SceneFader sceneFader;

    [SerializeField] string nextLevel = "Level 02";
    //[SerializeField] int nextLevelIndex = 2;

    [SerializeField] Text roundsText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
        //PlayerPrefs.SetInt("levelReached", nextLevelIndex);
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

    public void PressContinue()
    {
        
        sceneFader.LoadScene(nextLevel);
    }

    public void PressMenu()
    {
        sceneFader.LoadScene("Menu_Main");
    }
}
