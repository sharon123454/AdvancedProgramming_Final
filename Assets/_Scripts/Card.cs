using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Card", menuName = "Cards")]
public class Card : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite image;

    public int cost;
    public int health;
    public int damage;
}