using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private float offset;
    // Lists
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();
    public Transform _canvas;
    // Define variables for card instantiation locations
    public Vector3 playerCardSpawn = new Vector3(-500, 0, 0);
    public Vector3 aiCardSpawn = new Vector3(50, 800, 0);
    
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
        offset = 0;
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
            int cardNumber = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[cardNumber], new Vector3(-500 + offset, 0, 0), quaternion.identity);
            current.transform.SetParent(_canvas);
            player_deck.Add(current);
            deck.RemoveAt(cardNumber);
            offset += 20;
        }

        offset = 0;
        Shuffle();
        
        for (int i = 0; i < handSize; i++)
        {
            Card ai_current = Instantiate(deck[i], new Vector3(50 + offset, 800, 0), Quaternion.Euler(180,0,0));
            ai_deck.Add(ai_current);
            ai_current.transform.SetParent(_canvas);
            offset += 20;
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
            playerCard.transform.position = playerCardSpawn;
            playerCard.transform.GetChild(4).gameObject.SetActive(false);
            player_deck.RemoveAt(0);
            Debug.Log("Player plays: " + playerCard.name);

            // Play the first card from the AI's deck
            Card aiCard = ai_deck[0];
            aiCard.transform.position = aiCardSpawn;
            aiCard.transform.GetChild(4).gameObject.SetActive(false);
            ai_deck.RemoveAt(0);
            Debug.Log("AI plays: " + aiCard.name);
            
            // Compare the cards and determine the winner based on your game's rules
            DetermineWinner(playerCard, aiCard);
        }
        // Player Lost
        if (player_deck.Count < 0)
        {
            Debug.Log("You ran out of cards. You lose!");
        }
        // AI Lost
        if (ai_deck.Count < 0)
        {
            Debug.Log("Your opponent ran out of cards. You win!");
        }
    }

    void DetermineWinner(Card playerCard, Card aiCard)
    {
        if (playerCard.value > aiCard.value)
        {
            // Player wins, add both cards to player discard pile
            player_deck.Add(playerCard);
            player_deck.Add(aiCard);
            Debug.Log("Player wins the hand!");
        }
        else if (aiCard.value > playerCard.value)
        {
            // AI wins, add both cards to AI discard pile
            ai_deck.Add(playerCard);
            ai_deck.Add(aiCard);
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
        // Player Lost
        if (player_deck.Count < 4)
        {
            Debug.Log("You ran out of cards. You lose!");
        }
        // AI Lost
        if (ai_deck.Count < 4)
        {
            Debug.Log("Your opponent ran out of cards. You win!");
        }
    }

    void DetermineWarWinner(List<Card> playerWarCards, List<Card> aiWarCards)
    {
        Card playerFourthCard = playerWarCards[3];
        Card aiFourthCard = aiWarCards[3];

        Debug.Log("Player's fourth card in war: " + playerFourthCard.name);
        Debug.Log("AI's fourth card in war: " + aiFourthCard.name);

        if (playerFourthCard.value > aiFourthCard.value)
        {
            // Player wins the war, add all cards to player discard pile
            player_deck.AddRange(playerWarCards);
            player_deck.AddRange(aiWarCards);
            Debug.Log("Player wins the war!");
        }
        else if (aiFourthCard.value > playerFourthCard.value)
        {
            // AI wins the war, add all cards to AI discard pile
            ai_deck.AddRange(playerWarCards);
            ai_deck.AddRange(aiWarCards);
            Debug.Log("AI wins the war!");
        }
        else
        {
            War();
        }
    }
    
}
