using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class RoundsUI : MonoBehaviour
{
    [SerializeField] Text roundsText;

    private void Update()
    {
        if (GameStats.wavesCompleted > 0)
        {
            roundsText.text = "ROUND: " + GameStats.wavesCompleted.ToString();
        }
        else
        {
            roundsText.text = "ROUND: 1";
        }
        
    }
}
