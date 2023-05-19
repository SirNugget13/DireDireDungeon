using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private CanvasGroup GameOverGroup;
    
    public PlayerController pc;
    public float FadeTime = 1;
    public SceneLoader sl;
    //public GameObject MainMenuButton;

    public GameObject clickText;

    private bool waitToRegister;
    private bool GOver = false;
    private MusicManager mm;
    private bool unotimes = false;

    private void Start()
    {
        clickText.SetActive(false);
        GameOverGroup.alpha = 0;
        mm = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pc.isDead)
        {
            pc.state = PlayerController.State.Stopped;
            GOver = true;
            GameOverGroup.gameObject.SetActive(true);
            
            if(unotimes == false)
            {
                mm.SwitchTrack(mm.death);
                unotimes = true;
            }
            
        }

        if(waitToRegister)
        {
            if(!clickText.activeSelf)
            {
                clickText.SetActive(true);
            }
           
            if (Input.GetButtonDown("Attack"))
            {
                sl.LoadScenes(0);
            }
        }

        if(GOver)
        {  
            //Debug.Log("GameOver");
            GameOverGroup.alpha = Mathf.Lerp(GameOverGroup.alpha, 1, FadeTime * Time.deltaTime);
            //MainMenuButton.SetActive(true);

            this.Wait(0.75f, () =>
            {
                waitToRegister = true;
            });
        }
    }
}
