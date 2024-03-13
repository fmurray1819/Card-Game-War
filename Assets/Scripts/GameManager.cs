using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();

    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateDeck();
        Shuffle(deck);
        Deal();
    }

    void CreateDeck()
    {
        // Populate the deck with cards
        // You should implement your logic to create the deck here
    }

    void Shuffle(List<Card> deckToShuffle)
    {
        for (int i = 0; i < deckToShuffle.Count; i++)
        {
            Card temp = deckToShuffle[i];
            int randomIndex = Random.Range(i, deckToShuffle.Count);
            deckToShuffle[i] = deckToShuffle[randomIndex];
            deckToShuffle[randomIndex] = temp;
        }
    }

    void Deal()
    {
        for (int i = 0; i < 5; i++) // Deal 5 cards to each player
        {
            if (deck.Count > 0)
            {
                player_hand.Add(deck[0]);
                deck.RemoveAt(0);
            }
            if (deck.Count > 0)
            {
                ai_hand.Add(deck[0]);
                deck.RemoveAt(0);
            }
        }
    }

    void AI_Turn()
    {
        // Implement AI's turn logic here
    }
}