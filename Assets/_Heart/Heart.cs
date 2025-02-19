using UnityEngine;

public class Heart : MonoBehaviour
{

    PlayerHealth playerHealth;
    AudioPlayer audioPlayer;
    Coin coin;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        coin = FindObjectOfType<Coin>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            audioPlayer.PlayHeartSound();
            coin.AddHearts(1);
            Destroy(gameObject);
        }
    }
}
