using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private Image p1CardImage;
    [SerializeField] private Image p2CardImage;
    [SerializeField] private Button dealButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Sprite defaultCard;
    [SerializeField] private TextMeshProUGUI p1CardName;
    [SerializeField] private TextMeshProUGUI p2CardName;
    [SerializeField] private TextMeshProUGUI resultText;

    private Deck deck;

    /// <summary>
    /// Unity initialization occuring before the first frame update.
    /// </summary>
    void Start()
    {
        if (p1CardImage == null || p2CardImage == null || dealButton == null)
            Debug.LogError("Missing value assignments in inspector.", this);

        dealButton.onClick.AddListener(Deal);
        restartButton.onClick.AddListener(Restart);

        deck = new Deck();
        deck.Shuffle();
    }

    /// <summary>
    /// Deal cards to both players and assign image and text values.
    /// </summary>
    private void Deal()
    {
        Card p1Card = deck.DrawCard();

        if (p1Card == null) // deck empty
        {
            deck = new Deck();
            deck.Shuffle();
            p1Card = deck.DrawCard();
        }

        p1CardImage.sprite = p1Card.GetImage();
        p1CardName.text = $"{p1Card.GetName()} of {p1Card.GetSuit()}";

        Card p2Card = deck.DrawCard();

        if (p2Card == null) // deck empty
        {
            deck = new Deck();
            deck.Shuffle();
            p2Card = deck.DrawCard();
        }

        p2CardImage.sprite = p2Card.GetImage();
        p2CardName.text = $"{p2Card.GetName()} of {p2Card.GetSuit()}";

        ResolveMatch(p1Card, p2Card);
    }

    /// <summary>
    /// Determine the winner and display results.
    /// </summary>
    /// <param name="p1Card">Player 1's card.</param>
    /// <param name="p2Card">Player 2's card.</param>
    private void ResolveMatch(Card p1Card, Card p2Card)
    {
        if (p1Card.GetValue() > p2Card.GetValue())
        {
            resultText.text = "Player 1\nWins!";
        }
        else if (p1Card.GetValue() < p2Card.GetValue())
        {
            resultText.text = "Player 2\nWins!";
        }
        else if (p1Card.GetValue() == p2Card.GetValue())
        {
            resultText.text = "Tie!";
        }
    }

    /// <summary>
    /// Restart the game with a fresh deck.
    /// </summary>
    private void Restart()
    {
        deck = new Deck();
        deck.Shuffle();
        p1CardImage.sprite = defaultCard;
        p2CardImage.sprite = defaultCard;
        p1CardName.text = string.Empty;
        p2CardName.text = string.Empty;
        resultText.text = string.Empty;
    }
}
