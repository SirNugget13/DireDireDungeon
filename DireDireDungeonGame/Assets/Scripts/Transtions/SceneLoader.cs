using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] RectTransform fader;

    public AudioMixer GameAudio;
    public AudioSource Woosh;
    public AudioSource ReWoosh;

    public PlayerController player;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.state = PlayerController.State.Stopped;
        }    
        
        fader.gameObject.SetActive(true);

        if(GameAudio != null)
        {
            StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "MusicVolume", 0.01f, 0));
            StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "MusicVolume", 1.5f, 0.8f));

            StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "EffectsVolume", 0.01f, 0));
            StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "EffectsVolume", 1.5f, 0.8f));
        }

        this.Wait(1f, () =>
        {
            if (ReWoosh != null){ ReWoosh.PlayDelayed(0); }
            
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            LeanTween.scale(fader, Vector3.zero, 1.2f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                fader.gameObject.SetActive(false);
                if (SceneManager.GetActiveScene().buildIndex == 1) { } //player.state = PlayerController.State.Normal; }
            });
        });
    }

    public void LoadScenes(int index)
    {
        Debug.Log("LoadCalled");

        Time.timeScale = 1;

        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(0, 0, 0), 0);

        StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "MusicVolume", 1.5f, 0));
        StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "EffectsVolume", 1.5f, 0));

        Woosh.PlayDelayed(0.75f);
        this.Wait(1, () => 
        {
            LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                this.Wait(0.2f, () =>
                {
                    SceneManager.LoadScene(index);
                });
            });
        });
    }

    public void NextLevel()
    {
        Debug.Log("NextCalled");

        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(0, 0, 0), 0);

        StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "MusicVolume", 1.5f, 0));
        StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "EffectsVolume", 1.5f, 0));

        Woosh.PlayDelayed(0.75f);
        this.Wait(1, () =>
        {
            LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                this.Wait(0.2f, () =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                });
            });
        });
    }

    public void ReloadScene()
    {
        Debug.Log("ReloadCalled");

        Time.timeScale = 1;

        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(0, 0, 0), 0);

        StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "MusicVolume", 1.5f, 0));
        StartCoroutine(FadeMixerGroup.StartFade(GameAudio, "EffectsVolume", 1.5f, 0));

        Woosh.PlayDelayed(0.75f);
        this.Wait(1, () =>
        {
            LeanTween.scale(fader, Vector3.one, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
            {
                this.Wait(0.2f, () =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });
            });
        });
    }

    public void LoadScenes(string scene)
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            SceneManager.LoadScene(scene);
        });
    }

    public void LoadMainMenu()
    {
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
