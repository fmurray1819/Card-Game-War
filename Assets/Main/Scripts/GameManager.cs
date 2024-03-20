using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();
    public List<Card> player_discard = new List<Card>();
    public List<Card> ai_discard = new List<Card>();

    public Transform _canvas;

    private float offset;

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
    
    void Start()
    {
        offset = 250;
        // Shuffle the deck
        Shuffle();

        // Deal cards to players
        Deal();
    }
    
    void Update()
    {
        // Check for the spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
    }

    void Deal()
    {
        int handSize = deck.Count / 2;
        for (int i = 0; i < handSize; i++)
        {
            print(deck.Count + " " + i);
            int cardNumber = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[cardNumber], new Vector3(-750 + offset, 600, 0), quaternion.identity);
            current.transform.SetParent(_canvas);
            player_deck.Add(current);
            deck.RemoveAt(cardNumber);
            offset += 275;

        }

        Shuffle();
        for (int i = 0; i < handSize; i++)
        {
            print("handsize" + handSize + "  " + i);


            Card ai_current = deck[i];
            ai_deck.Add(ai_current);

        }

        deck.Clear();
    }

    void Shuffle()
    {
        // Shuffle the deck using Fisher-Yates algorithm
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Card value = deck[k];
            deck[k] = deck[n];
            deck[n] = value;
        }
    }

    void Play()
    {
        
        
        // Check if both players have at least one card in their deck
        if (player_deck.Count > 0 && ai_deck.Count > 0)
        {
            // Play the first card from the player's deck
            Card playerCard = player_deck[0];
            player_deck.RemoveAt(0);
            Debug.Log("Player plays: " + playerCard.name);

            // Play the first card from the AI's deck
            Card aiCard = ai_deck[0];
            
            
            //this will "flip" the card over by getting the back and turning it off
            //aiCard.transform.GetChild(4).gameObject.SetActive(false);
            
            
            
            
            ai_deck.RemoveAt(0);
            Debug.Log("AI plays: " + aiCard.name);

            // Compare the cards and determine the winner based on your game's rules
            DetermineWinner(playerCard, aiCard);
        }
        else
        {
            // Player ran out of cards and loses the game
            Debug.Log("You ran out of cards. You lose!");
        }
    }

    void DetermineWinner(Card playerCard, Card aiCard)
    {
        // Add your comparison logic here
        // Example: Compare card values and declare a winner

        if (playerCard.value > aiCard.value)
        {
            // Player wins, add both cards to player discard pile
            player_discard.Add(playerCard);
            player_discard.Add(aiCard);
            Debug.Log("Player wins the hand!");
        }
        else if (aiCard.value > playerCard.value)
        {
            // AI wins, add both cards to AI discard pile
            ai_discard.Add(playerCard);
            ai_discard.Add(aiCard);
            Debug.Log("AI wins the hand!");
        }
        else
        {
            // It's a tie, initiate a war
            War();
        }
    }

    void War()
    {
        // Check if both players have at least one card in their deck
        if (player_deck.Count >= 4 && ai_deck.Count >= 4)
        {
            // Play all cards from the player's deck
            List<Card> playerWarCards = player_deck.GetRange(0, 4);
            foreach (Card card in playerWarCards)
            {
                Debug.Log("Player's card in war: " + card.name);
            }

            // Play all cards from the AI's deck
            List<Card> aiWarCards = ai_deck.GetRange(0, 4);
            foreach (Card card in aiWarCards)
            {
                Debug.Log("AI's card in war: " + card.name);
            }

            // Determine the winner based on specific war rules
            DetermineWarWinner(playerWarCards, aiWarCards);

            // Remove played cards from the decks
            player_deck.RemoveRange(0, 4);
            ai_deck.RemoveRange(0, 4);
        }
        else
        {
            // Player ran out of cards and loses the game
            Debug.Log("You ran out of cards. You lose!");
        }
    }

    void DetermineWarWinner(List<Card> playerWarCards, List<Card> aiWarCards)
    {
        // Add your war comparison logic here
        // Example: Compare the fourth cards and declare a winner

        Card playerFourthCard = playerWarCards[3];
        Card aiFourthCard = aiWarCards[3];

        Debug.Log("Player's fourth card in war: " + playerFourthCard.name);
        Debug.Log("AI's fourth card in war: " + aiFourthCard.name);

        if (playerFourthCard.value > aiFourthCard.value)
        {
            // Player wins the war, add all cards to player discard pile
            player_discard.AddRange(playerWarCards);
            player_discard.AddRange(aiWarCards);
            Debug.Log("Player wins the war!");
        }
        else if (aiFourthCard.value > playerFourthCard.value)
        {
            // AI wins the war, add all cards to AI discard pile
            ai_discard.AddRange(playerWarCards);
            ai_discard.AddRange(aiWarCards);
            Debug.Log("AI wins the war!");
        }
        else
        {
            // War continues or specific rules apply
            Debug.Log("War results in another tie or specific rules apply.");
        }
    }
}
