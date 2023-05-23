using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen instance { get; private set; }

    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public GameObject placeHolder;
    //public GameObject falshes;
    public List<ParticleSystem> flashes;

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

        foreach (ParticleSystem particleSystem in flashes)
        {
            particleSystem.Stop();
        }

        // TODO: - ganjiaqi temp logic! fix it later!!
        Invoke("TempStopParticle", 3f);
    }


    public void FadeIn(float fadeDuration = 2f)
    {
        Debug.Log("begin to fade in!!!");
        this.fadeDuration = fadeDuration;
        Fade(1, 0);
    }

    public void FadeOut(float fadeDuration = 2f)
    {
        Debug.Log("begin to fade out!!!");
        this.fadeDuration = fadeDuration;
        Fade(0, 1, true);
    }

    private void Fade(float alphaIn, float alphaOut, bool fadeout = false)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, fadeout));
    }

    private IEnumerator FadeRoutine(float alphaIn, float alphaOut, bool fadeout = false)
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

        if(fadeout)
        {
            // TODO: - ganjiaiq arrange them!!
            placeHolder.SetActive(true);
            foreach (ParticleSystem particleSystem in flashes)
            {
                particleSystem.Play();
            }
        }
    }

    public void EndingScreen(float fadeDuration = 2f)
    {
        FadeOut(fadeDuration);
    }

    private void TempStopParticle()
    {
        foreach (ParticleSystem particleSystem in flashes)
        {
            particleSystem.Stop();
        }
    }
}