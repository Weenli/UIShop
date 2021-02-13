using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProductData : ScriptableObject {
    public int count;
    public int BuyCount;
    public Sprite image;
    public int price;
    public new string name;
    public string content;
    public ProductType type;
}