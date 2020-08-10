using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BoardPrefab;
    public GameObject shopManager;
    public GameObject Mat1;
    public GameObject Mat2;
    public GameObject Mat3;
    public Sprite[] WeaponImageList;
    public Dictionary<int, int> PriceDictionary;
    public List<ShopItem> ShopList = new List<ShopItem>();
    public int whipcreamCost;
    public int noodleCost;
    public int popcornCost;
    public int bubbleteaCost;

    public delegate void NotifyShopItemSelected(ShopItem selectedItem);
    public static event NotifyShopItemSelected notifyitemselected;

    enum DisplayWeapons
    {
        whipcream, // booster
        noodle, //charge
        popcorn, //bomb
        bubbletea // Shooter
    }
    void Start()
    {
        // WeaponList = new string[] {"whipcream", "noodle", "bomb", "bubbletea" };
        // create random gen to get three values
        PriceDictionary = new Dictionary<int, int>(){
            {(int) DisplayWeapons.whipcream, whipcreamCost},
            {(int) DisplayWeapons.noodle,noodleCost},
            {(int) DisplayWeapons.popcorn, popcornCost},
            {(int) DisplayWeapons.bubbletea,bubbleteaCost}
            };

        for (int i = 0; i < 3; i++)
        {
            int randPos = Random.Range(0, PriceDictionary.Count);
            GameObject board = Instantiate(BoardPrefab, transform.position, transform.rotation) as GameObject;
            board.transform.SetParent(shopManager.transform, true);
            board.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = WeaponImageList[randPos];
            board.transform.GetChild(0).gameObject.GetComponent<Text>().text = PriceDictionary[randPos].ToString();
            ShopList.Add(new ShopItem(randPos, PriceDictionary[randPos]));
            // print("shoplist length: " + ShopList.Count);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Mat1.GetComponent<ShopMat>().isSelected){
            RaiseShopItemSelcetd(ShopList[0]);
        }else if(Mat2.GetComponent<ShopMat>().isSelected){
            RaiseShopItemSelcetd(ShopList[1]);
        }else if( Mat3.GetComponent<ShopMat>().isSelected){
            RaiseShopItemSelcetd(ShopList[2]);
        }

    }

    public void RaiseShopItemSelcetd(ShopItem selectedItem){ 
        if (notifyitemselected != null)
            {
                notifyitemselected(selectedItem);
            }

    }
}

public class ShopItem
{
    public ShopItem(int WeaponIdx, int Cost)
    {
        weaponidx = WeaponIdx;
        cost = Cost;
    }
    public int weaponidx { get; set; }
    public int cost { get; set; }
}

