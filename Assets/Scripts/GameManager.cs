using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();
    public List<Card> player_discard = new List<Card>();
    public List<Card> ai_discard = new List<Card>();

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
<<<<<<< HEAD
        // Shuffle the deck
        Shuffle();

        // Deal cards to players
=======
        CreateDeck();
        Shuffle(deck);
>>>>>>> 9d83a41175257c2e42b54522e2f1976e53ea4b44
        Deal();
    }

    void CreateDeck()
    {
<<<<<<< HEAD
        // Check for the spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
=======
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
>>>>>>> 9d83a41175257c2e42b54522e2f1976e53ea4b44
        }
    }

    void Deal()
    {
<<<<<<< HEAD
        int totalCards = deck.Count;
        int cardsPerPlayer = totalCards / 2;

        for (int i = 0; i < cardsPerPlayer; i++)
        {
            // Deal to the player
            player_deck.Add(deck[0]);
            deck.RemoveAt(0);

            // Deal to the AI
            ai_deck.Add(deck[0]);
            deck.RemoveAt(0);
        }
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
=======
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
>>>>>>> 9d83a41175257c2e42b54522e2f1976e53ea4b44
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
<<<<<<< HEAD
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
=======
        // Implement AI's turn logic here
    }
}
>>>>>>> 9d83a41175257c2e42b54522e2f1976e53ea4b44
