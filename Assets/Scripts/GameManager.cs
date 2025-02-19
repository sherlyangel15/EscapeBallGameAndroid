using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject Player;
    [SerializeField] Sprite StartFace;
    public Sprite deadFace;

    [Header("UI")]
    [SerializeField] Button pauseButton;
    [SerializeField] Sprite pauseImage;
    [SerializeField] Sprite playImage;

    [Header("Audio")]
    GameObject AudioPlayerObject;

    [Header("Respawn")]
    [ SerializeField] GameObject[] respawnObject;
    [SerializeField] List<Vector3> respawnObjectsPositionList;
    [SerializeField] List<Vector3> respawnObjectsVelocityList;
    [SerializeField] List<float> respawnObjectsGravityList;

    bool isPaused;
    LevelManager levelManager;
    AudioPlayer audioPlayer;
    PlayerAttackMode playerAttackMode;
    PlayerMove playerMove;
    PlayerHealth playerHealth;
    AudioSource audioSource;
    DefaultPlayerFace defaultPlayerFaceScript;
    Vector3 playerStartPosition;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioPlayer = FindAnyObjectByType<AudioPlayer>();
        playerMove = FindObjectOfType<PlayerMove>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerAttackMode = FindObjectOfType<PlayerAttackMode>();
        defaultPlayerFaceScript = FindObjectOfType<DefaultPlayerFace>();

        Player = GameObject.FindWithTag("Player");
        AudioPlayerObject = GameObject.FindWithTag("AudioPlayerObject");

        if(Player == null) return;

        audioSource = AudioPlayerObject.GetComponent<AudioSource>();
        playerStartPosition = Player.transform.position;

        isPaused = false;

        //store initial states in respawnObject
        foreach(var obj in respawnObject)
        {
            respawnObjectsPositionList.Add(obj.transform.position);
            respawnObjectsVelocityList.Add(obj.GetComponent<Rigidbody2D>().velocity);
            respawnObjectsGravityList.Add(obj.GetComponent<Rigidbody2D>().gravityScale);
        }
    }

    //Delay execution of a specified action by a given time
    public void StartDelay(float delayTime, Action startAction)
    {
        StartCoroutine(TimeDelay(delayTime, startAction));
    }

    private IEnumerator TimeDelay(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    public void KillPlayer()
    {
        audioPlayer.PlayDieSound();
        Player.gameObject.SetActive(false);
        StartDelay(2f, ActivatePlayer);

    }
    public void ActivatePlayer()
    {
        Player.gameObject.SetActive(true);
        RespawnObjects();
    }

    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }

    public void PauseGame()
    {
        if((pauseButton.image.sprite == pauseImage)&& (!isPaused))
        {
            PauseGameState();
        }
        else if((pauseButton.image.sprite == playImage)&&(isPaused))
        {
            ResumeGame();
        }
    }

    private void ResumeGame()
    {
        pauseButton.GetComponent<Image>().sprite = pauseImage;
        Time.timeScale = 1;
        isPaused = false;
    }

    private void PauseGameState()
    {
        pauseButton.GetComponent<Image>().sprite = playImage;
        Time.timeScale = 0;
        isPaused = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            audioSource.volume = 0;
            audioPlayer.PlayVictoryMusic();
            StartDelay(4f, levelManager.LoadLevelsScene);
        }
    }

    //Put all the respawning Items in the respawnObject array!!!
    public void RespawnObjects()
    {
        //Reset player state
        Player.transform.position = playerStartPosition;
        Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        playerMove.SetRawValueZero(); //Not Necessary for Android. 
        playerHealth.SetHealth(100);
        playerAttackMode.SetAttackMode(false);
        defaultPlayerFaceScript.SetDefaultSprite(StartFace);

        //Reset state of each respawn object
        for(int i = 0; i < respawnObject.Length; i++)
        {
            var rb = respawnObject[i].GetComponent<Rigidbody2D>();
            respawnObject[i].transform.position = respawnObjectsPositionList[i];
            rb.gravityScale = respawnObjectsGravityList[i];
            rb.velocity = respawnObjectsVelocityList[i];
        }
    }



}
