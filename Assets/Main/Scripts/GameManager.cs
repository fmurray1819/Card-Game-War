using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
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
    private Vector3 playerDeckSpawn = new Vector3(-500, 0, 0);
    private Vector3 aiDeckSpawn = new Vector3(900, 1080, 0);
    private float time;
    private float ogtime;
    private Card playerCard;
    private Card aiCard;
    private bool timeIsStopped = true;
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
        //offset = 0;
        // Shuffle the deck
        Shuffle();

        // Deal cards to players
        Deal();
        
        //Timer
        ogtime = 3f;
        time = ogtime;
    }
    
    void Update()
    {
        // Check for the spacebar input
        if (time == 3 && timeIsStopped)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Play();
                timeIsStopped = false;
            }
        }
        
        // Compare the cards and determine the winner based on your game's rules
        if (!timeIsStopped)
        {
            time -= Time.deltaTime;
            print(time);
            if (time < 0)
            {
                time = ogtime;
                timeIsStopped = true;
                DetermineWinner(playerCard, aiCard);
            }
        }
    }

    void Deal()
    {
        int handSize = deck.Count / 2;
        for (int i = 0; i < handSize; i++)
        {
            int cardNumber = Random.Range(0, deck.Count);
            Card current = Instantiate(deck[cardNumber], playerDeckSpawn, quaternion.identity);
            current.transform.SetParent(_canvas);
            player_deck.Add(current);
            deck.RemoveAt(cardNumber);
            //offset += 20;
        }

        //offset = 0;
        Shuffle();
        
        for (int i = 0; i < handSize; i++)
        {
            Card ai_current = Instantiate(deck[i], aiDeckSpawn, Quaternion.Euler(180,0,0));
            ai_deck.Add(ai_current);
            ai_current.transform.SetParent(_canvas);
            //offset += 20;
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
            playerCard = player_deck[0];
            playerCard.transform.position = playerCardSpawn;
            playerCard.transform.GetChild(4).gameObject.SetActive(false);
            player_deck.RemoveAt(0);
            print("Player plays: " + playerCard.name);

            // Play the first card from the AI's deck
            aiCard = ai_deck[0];
            aiCard.transform.position = aiCardSpawn;
            aiCard.transform.GetChild(4).gameObject.SetActive(false);
            ai_deck.RemoveAt(0);
            print("AI plays: " + aiCard.name);
            
            
      
        }
        // Player Lost
        if (player_deck.Count < 0)
        {
            print("You ran out of cards. You lose!");
        }
        // AI Lost
        if (ai_deck.Count < 0)
        {
            print("Your opponent ran out of cards. You win!");
        }
    }

    void DetermineWinner(Card playerCard, Card aiCard)
    {
        if (playerCard.value > aiCard.value)
        {
            // Player wins, add both cards to player discard pile
            playerCard.transform.GetChild(4).gameObject.SetActive(true);
            aiCard.transform.GetChild(4).gameObject.SetActive(true);
            player_deck.Add(playerCard);
            player_deck.Add(aiCard);
            print("Player wins the hand!");
        }
        else if (aiCard.value > playerCard.value)
        {
            // AI wins, add both cards to AI discard pile
            playerCard.transform.GetChild(4).gameObject.SetActive(true);
            aiCard.transform.GetChild(4).gameObject.SetActive(true);
            ai_deck.Add(playerCard);
            ai_deck.Add(aiCard);
            print("AI wins the hand!");
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
                print("Player's card in war: " + card.name);
            }

            // Play all cards from the AI's deck
            List<Card> aiWarCards = ai_deck.GetRange(0, 4);
            foreach (Card card in aiWarCards)
            {
                print("AI's card in war: " + card.name);
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
            print("You ran out of cards. You lose!");
        }
        // AI Lost
        if (ai_deck.Count < 4)
        {
            print("Your opponent ran out of cards. You win!");
        }
    }

    void DetermineWarWinner(List<Card> playerWarCards, List<Card> aiWarCards)
    {
        Card playerFourthCard = playerWarCards[3];
        Card aiFourthCard = aiWarCards[3];

        print("Player's fourth card in war: " + playerFourthCard.name);
        print("AI's fourth card in war: " + aiFourthCard.name);

        if (playerFourthCard.value > aiFourthCard.value)
        {
            // Player wins the war, add all cards to player discard pile
            player_deck.AddRange(playerWarCards);
            player_deck.AddRange(aiWarCards);
            print("Player wins the war!");
        }
        else if (aiFourthCard.value > playerFourthCard.value)
        {
            // AI wins the war, add all cards to AI discard pile
            ai_deck.AddRange(playerWarCards);
            ai_deck.AddRange(aiWarCards);
            print("AI wins the war!");
        }
        else
        {
            War();
        }
    }
    
}
