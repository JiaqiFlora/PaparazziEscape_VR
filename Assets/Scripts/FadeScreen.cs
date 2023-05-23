using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance { get; private set; }

    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;

    private Renderer rend;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        if (fadeOnStart)
        {
            FadeIn();
        }

        //foreach (ParticleSystem particleSystem in flashes)
        //{
        //    particleSystem.Stop();
        //}

        //// TODO: - ganjiaqi temp logic! fix it later!!
        //Invoke("TempStopParticle", 3f);
    }


    public void FadeIn(float fadeDuration = 2f)
    {
        Debug.Log("begin to fade in!!!");
        this.fadeDuration = fadeDuration;
        Fade(1, 0);
    }

    public void FadeOut(float fadeDuration = 2f, bool isEnd = false)
    {
        Debug.Log("begin to fade out!!!");
        this.fadeDuration = fadeDuration;
        Fade(0, 1, isEnd);
    }

    private void Fade(float alphaIn, float alphaOut, bool isEnd = false)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, isEnd));
    }

    private IEnumerator FadeRoutine(float alphaIn, float alphaOut, bool isEnd = false)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        rend.material.color = newColor2;

        if(isEnd)
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndex = currentIndex + 1;
            Debug.Log($"current index: {currentIndex}");
            Debug.Log($"next index: {nextIndex}");
            Debug.Log($"is it in range? {nextIndex < SceneManager.sceneCountInBuildSettings}");
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("load to next scene!!!");
                SceneManager.LoadScene(nextIndex);

            }
        }
    }

    public void EndingScreen(float fadeDuration = 2f)
    {
        FadeOut(fadeDuration, true);
    }
}