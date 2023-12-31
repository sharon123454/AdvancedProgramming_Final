using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck instance;

    [SerializeField] private List<Card> availableCards;

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    public int GetDeckAmount() { return availableCards.Count; }
    public Card DrawCard()
    {
        if (availableCards.Count > 0)
        {
            int rand = Random.Range(0, availableCards.Count);

            Card card = availableCards[rand];
            availableCards.Remove(card);

            //Debug.Log($"Card {card.name} was drawn");
            return card;
        }
        else
        {
            Debug.Log("Deck is out of Cards");
            return null;
        }
    }

    public Card DrawCardByName(string name)
    {
        foreach (Card card in availableCards)
        {
            if (card.name == name)
            {
                //delaying card removal as I cant remove an object in the list which it exists
                StartCoroutine(RemoveCardDelayed(card));
                return card;
            }
        }

        return null;
    }

    IEnumerator RemoveCardDelayed(Card cardDrawn)
    {
        yield return null;
        availableCards.Remove(cardDrawn);
    }

}