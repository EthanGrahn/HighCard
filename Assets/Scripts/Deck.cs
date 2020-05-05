using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private int value;
    private string name;
    private string suit;
    private Sprite image;

    /// <summary>
    /// Create a new playing card.
    /// </summary>
    /// <param name="value">Integer value of the card.</param>
    /// <param name="name">Name of card. (e.g. 5, 10, Jack, King)</param>
    /// <param name="suit">Clubs, Spades, Hearts, or Diamonds.</param>
    /// <param name="image">Face image.</param>
    public Card(int value, string name, string suit, Sprite image)
    {
        this.value = value;
        this.name = name;
        this.suit = suit;
        this.image = image;
    }

    /// <summary>
    /// Get the integer value of the playing card.
    /// </summary>
    /// <returns>Integer value.</returns>
    public int GetValue()
    {
        return value;
    }

    /// <summary>
    /// Get the face name of the playing card.
    /// </summary>
    /// <returns>Face name of the playing card.</returns>
    public string GetName()
    {
        return name;
    }

    /// <summary>
    /// Get the suit of the playing card.
    /// </summary>
    /// <returns>Suit of the playing card.</returns>
    public string GetSuit()
    {
        return suit;
    }

    /// <summary>
    /// Get the sprite of the playing card.
    /// </summary>
    /// <returns>Sprite of the playing card.</returns>
    public Sprite GetImage()
    {
        return image;
    }
}

public class Deck
{
    private List<Card> cards;

    /// <summary>
    /// Initialize a new 52 card deck.
    /// </summary>
    public Deck()
    {
        cards = new List<Card>();
        List<Sprite> images = new List<Sprite>();
        images.AddRange(Resources.LoadAll<Sprite>("Cards")); // load all card images
        images.RemoveAll(x => x.name == "Blank"); // remove unneeded blank card

        int value = -1;
        string name = string.Empty;
        string suit = string.Empty;
        foreach(var image in images) // parse images to construct deck
        {
            string cardSuit = image.name.Substring(0, 1);
            switch (cardSuit) // first character indicates suit
            {
                case "C":
                    suit = "Clubs";
                    break;
                case "S":
                    suit = "Spades";
                    break;
                case "H":
                    suit = "Hearts";
                    break;
                case "D":
                    suit = "Diamonds";
                    break;
                default:
                    suit = "Invalid Suit";
                    Debug.LogWarning($"Invalid suit '{image.name.Substring(0, 1)}' found with image name '{image.name}'");
                    break;
            }

            string cardName = image.name.Substring(1);
            switch (cardName) // the rest of the string indicates the card name
            {
                case "J":
                    value = 11;
                    name = "Jack";
                    break;
                case "Q":
                    value = 12;
                    name = "Queen";
                    break;
                case "K":
                    value = 13;
                    name = "King";
                    break;
                case "A":
                    value = 14;
                    name = "Ace";
                    break;
                default:
                    if (int.TryParse(cardName, out int result))
                    {
                        value = result;
                        name = cardName;
                    }
                    else
                    {
                        value = -1;
                        name = "Invalid Card";
                        Debug.LogError($"Unable to parse '{cardName}' as integer.");
                    }
                    break;
            }

            cards.Add(new Card(value, name, suit, image));
        }
    }

    /// <summary>
    /// Get number of remaining cards in deck.
    /// </summary>
    /// <returns>Number of remaining cards in the deck.</returns>
    public int GetCount()
    {
        return cards.Count;
    }

    /// <summary>
    /// Shuffle the deck of cards into new arrangement.
    /// </summary>
    public void Shuffle()
    {
        if (cards.Count == 0)
        {
            Debug.LogWarning("Attempted shuffle on empty deck.");
            return;
        }

        int count = cards.Count;
        int final = count - 1;

        int j;
        Card swap;

        for (int i = 0; i < final; i++) // perform in-place shuffle
        {
            j = Random.Range(i, count);
            swap = cards[i];
            cards[i] = cards[j];
            cards[j] = swap;
        }
    }

    /// <summary>
    /// Draw a card from the deck.
    /// </summary>
    /// <returns>Top card on deck. Null if deck is empty.</returns>
    public Card DrawCard()
    {
        if (cards.Count == 0)
            return null;

        Card card = cards[0];
        cards.RemoveAt(0);

        return card;
    }
}
