using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _count;
    [SerializeField]
    private Image icon;
    [SerializeField]
    public Button InfoButton;


    public void Instantiate(string name, int count, Sprite image) {
        this._name.text = name;
        this._count.text = count.ToString();
        this.icon.sprite = image;
    }
    public void AddCount(int count) {
        _count.text = count.ToString();
    }
    public string GetName() {
        return this._name.text;
    }
}
