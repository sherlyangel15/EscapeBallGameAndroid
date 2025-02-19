using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    PlayerAttackMode attackMode;
    GameObject _face;
    public GameObject[] children;

    [Header("Effects")]
    [SerializeField] ParticleSystem DestroyEffect;
    [SerializeField] Sprite _deadFace;
    
    [SerializeField] GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        attackMode = FindObjectOfType<PlayerAttackMode>();

        _face = GameObject.FindWithTag("PlayerFace");
        DestroyEffect = GameObject.FindWithTag("PlayerDead").GetComponent<ParticleSystem>();

        if(gameManager != null)
        {
            _deadFace = gameManager.deadFace;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && !attackMode.GetAttackModeStatus())
        {
            _face.GetComponent<SpriteRenderer>().sprite = _deadFace;
            if(DestroyEffect != null)
            {
                DestroyEffect.Play();
            }
            if(gameManager != null)
            {
                gameManager.StartDelay(0.1f, gameManager.KillPlayer);
            }
        }
    }
}
