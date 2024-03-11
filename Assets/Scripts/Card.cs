using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card_data data;

    public Image Number;
    public Image NumberTwo;
    public Image Suit;
    public int value;
   
    // Start is called before the first frame update
    void Start()
    {
        Number.sprite = data.Number;
        NumberTwo.sprite = data.Number2;
        Suit.sprite = data.Suit;
        value = data.value;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
