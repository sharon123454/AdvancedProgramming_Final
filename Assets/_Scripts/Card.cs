using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_CardName", menuName = "New Card")]
public class Card : ScriptableObject
{
    public new string name;
    public Sprite image;
    public int number;
}