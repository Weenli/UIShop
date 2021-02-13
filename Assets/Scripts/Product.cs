using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product : MonoBehaviour
{
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _count;
    [SerializeField]
    private Text _price ;
    [SerializeField]
    private Image icon;
    [SerializeField]
    public Button _buy_button;
    public void Instantiate(string name,int count, int price, Sprite image) {
        this._name.text = name;
        this._price.text = price.ToString();
        this._count.text = count.ToString();
        this.icon.sprite = image;
        if(count == 0) {
            _buy_button.interactable = false;
        }
    }


}
