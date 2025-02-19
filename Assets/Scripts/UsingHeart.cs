using UnityEngine;

public class UsingHeart : MonoBehaviour
{
    PlayerHealth healthScript;
    Coin coinScript;
    int heartAmount;

    void Start()
    {
        healthScript = FindObjectOfType<PlayerHealth>();
        coinScript = FindObjectOfType<Coin>();
    }
    
    public void UseHeart()
    {
        heartAmount = coinScript.GetHeartAmount();
        
        if(heartAmount <= 0)
        {
            return;
        }
        else if(heartAmount > 0)
        {
            healthScript.SetHealth(100);
            coinScript.ReduceHearts(1);
        }
    }
}
