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

    public Card DrawCard()
    {
        if (availableCards.Count > 0)
        {
            int rand = Random.Range(0, availableCards.Count);

            Card card = availableCards[rand];
            availableCards.Remove(card);

            Debug.Log("Got card");
            return card;
        }
        else
        {
            Debug.Log("Deck is out of Cards");
            return null;
        }
    }

}