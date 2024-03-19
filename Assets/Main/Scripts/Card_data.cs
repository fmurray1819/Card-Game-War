using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_data", menuName = "Cards/Card_data", order = 1)]
public class Card_data : ScriptableObject
{
    
    public Sprite Number;
    public Sprite Number2;
    public Sprite Suit;
    public int value;
    public Sprite backside;

}
