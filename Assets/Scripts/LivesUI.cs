using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0

public class LivesUI : MonoBehaviour
{
    [SerializeField] Text livesText;

    private void Update()
    {
        livesText.text = GameStats.healthRemaining.ToString();
    }
}
