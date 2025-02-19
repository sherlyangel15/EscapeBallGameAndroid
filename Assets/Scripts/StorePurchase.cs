using UnityEngine;

public class StorePurchase : MonoBehaviour
{
    Coin coinScript;
    PlayerColor playerColor;
    int coinsAmount;

    [SerializeField] Sprite BlueSprite;
    [SerializeField] Sprite YellowSprite;
    [SerializeField] Sprite GreenSprite;
    [SerializeField] Sprite PinkSprite;
    [SerializeField] Sprite RedSprite;
    [SerializeField] Sprite PurpleSprite;

    public GameObject yellowButtonObj;
    public GameObject GreenButtonObj;
    public GameObject PinkButtonObj;
    public GameObject RedButtonObj;
    public GameObject PurpleButtonObj;

    void Start()
    {
        coinScript = FindObjectOfType<Coin>();
        playerColor = FindObjectOfType<PlayerColor>();
    }

    void Update()
    {
        coinsAmount = coinScript.GetCoinAmount();
    }
    
    void BalanceChecker(int Price, int heartAmount)
    {
        if(Price > coinsAmount)
        {
            return;
        }
        else if(Price <= coinsAmount)
        {
            coinScript.ReduceCoins(Price);
            coinScript.AddHearts(heartAmount);
        }
    }
    void BalanceReducer(int Price)
    {
        if(Price > coinsAmount)
        {
            return;
        }
        else if (Price <= coinsAmount)
        {
            coinScript.ReduceCoins(Price);
        }
    }

    public void BlueButton()
    {
        playerColor.SetPlayerSprite(BlueSprite);
    }
    public void YellowButton()
    {
        if(playerColor.yellowPurchased)
        {
            playerColor.SetPlayerSprite(YellowSprite);
            return;
        }
        else if(!playerColor.yellowPurchased)
        {
            if(coinsAmount < 50)
            {
                return;
            }
            playerColor.SetPlayerSprite(YellowSprite);
            BalanceReducer(50);
            playerColor.yellowPurchased = true;
        }
    }
    public void GreenButton()
    {
        if(playerColor.greenPurchased)
        {
            playerColor.SetPlayerSprite(GreenSprite);
            return;
        }
        else if(!playerColor.greenPurchased)
        {
            if(coinsAmount < 100)
            {
                return;
            }
            playerColor.SetPlayerSprite(GreenSprite);
            BalanceReducer(100);
            playerColor.greenPurchased = true;
        }
    }
    public void PinkButton()
    {
        if(playerColor.pinkPurchased)
        {
            playerColor.SetPlayerSprite(PinkSprite);
            return;
        }
        else if(!playerColor.pinkPurchased)
        {
            if(coinsAmount < 200)
            {
                return;
            }
            playerColor.SetPlayerSprite(PinkSprite);
            BalanceReducer(200);
            playerColor.pinkPurchased = true;
        }
    }
    public void RedButton()
    {
        if(playerColor.redPurchased)
        {
            playerColor.SetPlayerSprite(RedSprite);
            return;
        }
        else if(!playerColor.redPurchased)
        {
            if(coinsAmount < 250)
            {
                return;
            }
            playerColor.SetPlayerSprite(RedSprite);
            BalanceReducer(250);
            playerColor.redPurchased = true;
        }
    }
    public void PurpleButton()
    {
        if(playerColor.PurplePurchased)
        {
            playerColor.SetPlayerSprite(PurpleSprite);
            return;
        }
        else if(!playerColor.PurplePurchased)
        {
            if(coinsAmount < 400)
            {
                return;
            }
            playerColor.SetPlayerSprite(PurpleSprite);
            BalanceReducer(400);
            playerColor.PurplePurchased = true;
        }
    }

    public void OneHeart()
    {
        BalanceChecker(50, 1);
    }
    public void TwoHearts()
    {
        BalanceChecker(90, 2);
    }
    public void ThreeHearts()
    {
        BalanceChecker(130, 3);
    }
    public void FiveHearts()
    {
        BalanceChecker(225, 5);
    }
    public void TenHearts()
    {
        BalanceChecker(400, 10);
    }
    public void FiftyHearts()
    {
        BalanceChecker(1500, 50);
    }
}
 