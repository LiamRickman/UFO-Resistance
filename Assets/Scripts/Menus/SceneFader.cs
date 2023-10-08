using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class SceneFader : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    [SerializeField] AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime * 0.5f;
            float a = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t -= Time.deltaTime * 0.5f;
            float a = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, a);
            yield return 0;

            SceneManager.LoadScene(scene);
        }      
    }

    private void Update()
    {
        //Sets discord username to default if no username set (Only needed for in editor)
        //Put this here as it is active in every scene
        if (Discord.username == null)
        {
            Discord.username = "Default";
        }
    }
}
