using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ButtonController : MonoBehaviour
{
    private PlayerController playerControllerRef;
    public PlayerController PlayerControllerRef { set { playerControllerRef = value; } get { return playerControllerRef; } }
    [SerializeField]
    private AudioClip buttonClip;
    private AudioSource buttonAudioSource;
    private float lastButtonPressTime;
    [SerializeField]
    private float inputDelay;
    [SerializeField]
    private GameObject meteorUi;
    [SerializeField]
    private  GameController gameContRef;
    // Start is called before the first frame update
    void Start()
    {
        buttonAudioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartApp()
    {
        if (gameContRef.BIsGameActive == true) return;
        gameContRef.BIsGameActive = true;
        meteorUi.SetActive(false);
        gameContRef.Initialized();
    }
    public void QuitApp()
    {
        playSound();
        Application.Quit();
        print("Quitting!");
    }
    public void ButtonPress(int buttonNumber)
    {
        
        if(lastButtonPressTime + inputDelay <= Time.time)
        {
            lastButtonPressTime = Time.time;
            if (playerControllerRef != null)
            {
                playerControllerRef.PassIntent(buttonNumber);
            }
            playSound();
        }
        else
        {
            return;
        }
   
    }
    private void playSound()
    {
        if (buttonAudioSource != null)
        {
            buttonAudioSource.PlayOneShot(buttonClip);
        }
    }
}
