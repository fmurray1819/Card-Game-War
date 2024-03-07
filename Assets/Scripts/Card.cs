using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card_data data;

    public Sprite Number;
    public Sprite Suit;
    public int value;
    public Image NumberImage;
    public Image SuitImage;
        
    // Start is called before the first frame update
    void Start()
    {
        NumberImage.sprite = Number;
        SuitImage.sprite = Suit;
        value = data.value;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
