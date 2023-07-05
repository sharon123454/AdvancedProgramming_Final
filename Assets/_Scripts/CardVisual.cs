using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CardVisual : MonoBehaviour
{
    public Card card;
    [Space]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI numberText;
    public Image artworkImage;

    private void Start()
    {
        numberText.text = card.number.ToString();
        nameText.text = card.name;

        if (card.image)
        {
            artworkImage.sprite = card.image;
            //artworkImage.SetNativeSize();
        }
    }
}