using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [SerializeField] int coinAmount;
    [SerializeField] int heartAmount;

    [Header("UI References")]
    [SerializeField] GameObject coinUI;
    [SerializeField] GameObject heartUI;

    PlayerHealth playerHealth;


    //Singleton Pattern instance
    static Coin instance;
    void Awake()
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;   //Subscribe to sceneLoaded event
        }
    }

    void Start()
    {
        coinUI = GameObject.FindWithTag("CoinUI");
        heartUI = GameObject.FindWithTag("HeartUI");
        playerHealth = FindObjectOfType<PlayerHealth>();

        //Debug: Initialize coinAmount for testing purpose.
        // coinAmount = 2000;
    }

    void Update()
    {
        if(coinUI != null)
            coinUI.GetComponent<TextMeshProUGUI>().text = coinAmount.ToString();
        if(heartUI != null)
            heartUI.GetComponent<TextMeshProUGUI>().text = heartAmount.ToString();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ReassignReferences();
    }
    public int GetCoinAmount() => coinAmount;
    public void AddCoins(int value)
    {
        coinAmount += value;
    }
    public void ReduceCoins(int value)
    {
        coinAmount  = Mathf.Max(coinAmount - value, 0);
    }
    public int GetHeartAmount() => heartAmount;
    public void AddHearts(int value)
    {
        heartAmount += value;
    }

    public void ReduceHearts(int value)
    {
        heartAmount = Mathf.Max(heartAmount - value, 0);
    }

    void ReassignReferences()
    {
        //Resolve references for new scenes (Singleton pattern limitation)
        coinUI = GameObject.FindWithTag("CoinUI");
        heartUI = GameObject.FindWithTag("HeartUI");
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; //Unsubscribe
    }
}
