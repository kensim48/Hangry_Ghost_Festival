using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform boardPosition1;
    public Transform boardPosition2;
    public Transform boardPosition3;
    public Transform Board;
    // public string[] WeaponList;
    public Image[] WeaponImageList;
    public Dictionary<int, int> PriceDictionary;

    public int boosterCost;
    public int chargeCost;
    public int bombCost;
    public int bubbleteaCost;
    enum DisplayWeapons
    {
        whipcream, // Whip cream
        noodle, //noodle
        popcorn, //popcorn
        bubbletea // Shooter
    }
    void Start()
    {
        // WeaponList = new string[] {"whipcream", "noodle", "bomb", "bubbletea" };
        // create random gen to get three values
        // PriceDictionary = new Dictionary<int, int>(){(int) DisplayWeapons.booster:boosterCost, (int)DisplayWeapons.charge: char  }

        // int position1 = Random.Range(0, WeaponList.Length);
        // int position2 = Random.Range(0, WeaponList.Length);
        // int position3 = Random.Range(0, WeaponList.Length);



    }

    // Update is called once per frame
    void Update()
    {

    }
}
