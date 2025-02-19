using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    Sprite playerSprite;
    [SerializeField] Sprite _defaultSprite;
    public bool yellowPurchased;
    public bool greenPurchased;
    public bool pinkPurchased;
    public bool redPurchased;
    public bool PurplePurchased;

    StorePurchase storePurchase;
    
    //Singleton Pattern instance
    static PlayerColor instance;
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        yellowPurchased = false;
        greenPurchased = false;
        pinkPurchased = false;
        redPurchased = false;
        PurplePurchased = false;
    }

    void Update()
    {
        if(storePurchase == null)
        {
            storePurchase = FindObjectOfType<StorePurchase>();
        }
        if(playerSprite == null)
        {
            playerSprite = _defaultSprite;
        }

        UpdateColorPurchasedData();
    }

    public Sprite GetPlayerSprite() => playerSprite;
    public void SetPlayerSprite(Sprite sprite) => this.playerSprite = sprite;

    void UpdateColorPurchasedData()
    {
        HideButtonIfPurchased(yellowPurchased, storePurchase.yellowButtonObj);
        HideButtonIfPurchased(greenPurchased, storePurchase.GreenButtonObj);
        HideButtonIfPurchased(pinkPurchased, storePurchase.PinkButtonObj);
        HideButtonIfPurchased(redPurchased, storePurchase.RedButtonObj);
        HideButtonIfPurchased(PurplePurchased, storePurchase.PurpleButtonObj);
    }

    void HideButtonIfPurchased(bool isPurchased, GameObject buttonObject)
    {
        if(isPurchased)
        {
            foreach(Transform child in buttonObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
