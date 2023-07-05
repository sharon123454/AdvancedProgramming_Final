using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class Hand : MonoBehaviour
{
    [SerializeField] private CardVisual cardVisualPrefab;
    [SerializeField] private RectTransform[] cardPositions;
    [SerializeField] private int handSize;

    List<Card> cardList = new List<Card>();
    List<CardVisual> cardVisualList = new List<CardVisual>();

    public void FillHand()
    {
        for (int i = 0; i < handSize; i++)
        {
            Card card = Deck.instance.DrawCard();
            if (!card) return;

            if (cardVisualList.Count >= handSize)
            {
                CardVisual cardVisual = cardVisualList[i];
                cardVisualList.Remove(cardVisual);
                Destroy(cardVisual);
                cardList.RemoveAt(i);
            }

            cardList.Add(card);
            CardVisual newCard = Instantiate(cardVisualPrefab, cardPositions[i]);
            newCard.card = cardList[i];
            cardVisualList.Add(newCard);
        }
    }
}