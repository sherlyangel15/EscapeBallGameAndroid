using UnityEngine;

public class DestroyCoin : MonoBehaviour
{
    Coin coinScript;
    AudioPlayer audioPlayer;
    void Start()
    {
        coinScript = FindObjectOfType<Coin>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioPlayer.PlayCoinSound();
            coinScript.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
