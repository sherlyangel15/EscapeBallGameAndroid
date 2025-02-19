using UnityEngine;

public class PlayerAttackModeButton : MonoBehaviour
{
    PlayerMove player;
    AndroidInputHandler androidInputHandlerScript;
    PlayerAttackMode playerAttackMode;
    AudioPlayer audioPlayer;
    Animator _myAnimator;

    [Header("Attack Mode")]
    [SerializeField] GameObject attackModeTick;
    [SerializeField] GameObject attackModeButton;
    bool playerIsMoving = false;
    bool AttackIsReady = false;
    float count = 0f;

    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        playerAttackMode = FindObjectOfType<PlayerAttackMode>();
        androidInputHandlerScript = FindObjectOfType<AndroidInputHandler>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        _myAnimator = GetComponent<Animator>();
        attackModeTick = GameObject.FindWithTag("AMTick");
        attackModeButton = GameObject.FindWithTag("AMButton");
        attackModeTick.SetActive(false);
    }

    void Update()
    {
        playerIsMoving = androidInputHandlerScript.GetIsMovingStatus();

        if (playerIsMoving)
        {
            count += Time.deltaTime;
        }

        if(count >= 25 )
        {
            attackModeTick.SetActive(true);
            _myAnimator.SetBool("isReady",true);
            AttackIsReady = true;
            count = 0f;
        }
    }

    public void ButtonClicked()
    {
        if(!AttackIsReady)
        {
            return;
        }
        else if(AttackIsReady)
        {
            audioPlayer.PlaySurvivalMode();
            attackModeTick.SetActive(false);
            _myAnimator.SetBool("isReady", false);
            playerAttackMode.SetAttackMode(true);
            AttackIsReady = false;
        }
    }
}
