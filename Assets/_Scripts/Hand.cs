using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class Hand : MonoBehaviour
{
    public static Hand instance;

    [SerializeField] private CardVisual _cardVisualPrefab;
    [SerializeField] private RectTransform[] _handPositions;

    private Queue<CardVisual> _cardVisualQueue = new Queue<CardVisual>();

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    public void FillHand()
    {
        for (int i = 0; i < _handPositions.Length; i++)
            DrawCard();
    }
    public void LoadHand(List<string> cardNamesInHand)
    {
        foreach (string cardName in cardNamesInHand)
        {
            Card card = Deck.instance.DrawCardByName(cardName);
            if (!card) return;

            // Creating new card
            CardVisual newCard = Instantiate(_cardVisualPrefab, _handPositions[_cardVisualQueue.Count]);
            newCard.card = card;
            _cardVisualQueue.Enqueue(newCard);
        }
    }

    public void DrawCard()
    {
        // Checking if Deck isn't empty
        if (Deck.instance.GetDeckAmount() > 0)
        {
            Card card = Deck.instance.DrawCard();
            if (!card) return;

            // if amount of existing cards bigger/equal then slots in hand
            if (_cardVisualQueue.Count >= _handPositions.Length)
            {
                // pulls first card from queue and destroying it
                CardVisual cardVisual = _cardVisualQueue.Dequeue();
                Destroy(cardVisual.gameObject);

                //// Move all card positions in hand (should find a solution im losing references)
                Queue<CardVisual> tempQueue = new Queue<CardVisual>();
                for (int i = 0; i < _handPositions.Length - 1; i++)
                {
                    cardVisual = _cardVisualQueue.Dequeue();
                    cardVisual.transform.SetParent(_handPositions[i]);
                    cardVisual.transform.localPosition = Vector3.zero;
                    tempQueue.Enqueue(cardVisual);
                }

                _cardVisualQueue = tempQueue;
            }

            // Creating new card
            CardVisual newCard = Instantiate(_cardVisualPrefab, _handPositions[_cardVisualQueue.Count]);
            newCard.card = card;
            _cardVisualQueue.Enqueue(newCard);
        }
    }

}