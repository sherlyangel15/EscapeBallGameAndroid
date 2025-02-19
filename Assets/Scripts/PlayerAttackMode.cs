using System.Collections;
using UnityEngine;

public class PlayerAttackMode : MonoBehaviour
{
    AudioPlayer audioPlayer;
    PlayerHealth playerHealth;
    DefaultPlayerFace defaultPlayerFace;

    [Header("Player")]
    [SerializeField] bool inAttackMode;
    [SerializeField] GameObject Player;
    CapsuleCollider2D _playerCapsuleCollider;
    Color _defaultPlayerColor;
    [SerializeField] Vector3 _defaultScale;

    [Header("Face")]
    [SerializeField] GameObject face;
    [SerializeField] Sprite _attackFace;
    [SerializeField] Sprite _defaultFace;

    [Header("Border Glow")]
    [SerializeField] GameObject playerBorder;
    [SerializeField] int intensity = 10;
    Material borderMaterial;
    Color _defaultColor;

    [Header("Attack Mode Timer")]
    float duration = 1f;
    float timer = 0f;
    
    [Header("Attack Mode")]
    GameObject AMCollider;

    Coroutine attackModeCoroutine;

    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        defaultPlayerFace = FindObjectOfType<DefaultPlayerFace>();

        Player = GameObject.FindWithTag("Player");
        face = GameObject.FindWithTag("PlayerFace");
        _defaultPlayerColor = Player.GetComponent<SpriteRenderer>().color;
        
        borderMaterial = playerBorder.GetComponent<SpriteRenderer>().material;
        _defaultColor = borderMaterial.GetColor("_GlowColor");

        AMCollider = GameObject.FindWithTag("AMCollider");
        _playerCapsuleCollider = AMCollider.GetComponent<CapsuleCollider2D>();
        _defaultScale = new Vector3(2, 2, 0);
        inAttackMode = false;
    }

    void Update()
    {
        _defaultFace = defaultPlayerFace.GetDefaultSprite();

        if(inAttackMode)
        {
            timer += Time.deltaTime;
            if(timer >= 12f)
            {
                inAttackMode = false;
                timer = 0f;
            }

            ActivateAttackMode();
        }
        else if(!inAttackMode)
        {
            DeactivateAttackMode();
        }
    }

    public void SetAttackMode(bool value)
    {
        inAttackMode = value;
        timer = 0f;
    }

    public bool GetAttackModeStatus()
    {
        return inAttackMode;
    }

    void ActivateAttackMode()
    {
        _playerCapsuleCollider.enabled = true;

        face.GetComponent<SpriteRenderer>().sprite = _attackFace;
        face.transform.localScale = new Vector3(2, 2, 0);
        borderMaterial.SetColor("_GlowColor", Color.red * intensity);
        Player.GetComponent<SpriteRenderer>().color = Color.black;
        if(playerHealth.Gethealth() <= 5)
        {
            playerHealth.SetHealth(15);
        }
    }
    
    void DeactivateAttackMode()
    {
        _playerCapsuleCollider.enabled = false;

        face.GetComponent<SpriteRenderer>().sprite = _defaultFace;
        face.transform.localScale = _defaultScale;
        borderMaterial.SetColor("_GlowColor", _defaultColor);
        Player.GetComponent<SpriteRenderer>().color = _defaultPlayerColor;

    }

    IEnumerator ChangePropertyOverTime(GameObject enemyObject)
    {
        Material material = enemyObject.GetComponent<SpriteRenderer>().material;
        GameObject[] childrenArray = enemyObject.GetComponent<DestroyPlayer>().children;

        foreach(var child in childrenArray)
        {
            Destroy(child, 0.5f);
        }

        audioPlayer.PlayExplosionCrunchSound();

        float timeElapsed = 0f;
        while(timeElapsed < duration)
        {
            float currentValue = Mathf.Lerp(1, 0, timeElapsed/duration);
            material.SetFloat("_Fade", currentValue);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_Fade", 0);
        Destroy(enemyObject);
    }

    public void StartCorou(GameObject enemyObject)
    {
        attackModeCoroutine = StartCoroutine(ChangePropertyOverTime(enemyObject));
        StopCoroutine(ChangePropertyOverTime(enemyObject));
    }
}
