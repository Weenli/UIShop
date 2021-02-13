using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    [SerializeField]
    private InventorySlot InventoryPrefab;
    [SerializeField]
    private Transform InventoryContent;
    [SerializeField]
    private Text InfoText;
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private void Awake() {
        ShopManager.onButtonBuy += AddSlot;
    }
    private void AddSlot(string name, int count, Sprite image, string content) {
        if (inventorySlots.Find(x => x.GetName().Equals(name)) == null) {
            var Clone = Instantiate(InventoryPrefab, Vector3.zero, Quaternion.identity, InventoryContent) as InventorySlot;
            Clone.Instantiate(name, count, image);
            Clone.InfoButton.onClick.AddListener(delegate { Info(content); });
            inventorySlots.Add(Clone);

        }
        else {
            inventorySlots.Find(x => x.GetName().Equals(name)).AddCount(count);

        }
    }
    private void Info(string info) {
        InfoText.text = info;
        InfoText.gameObject.SetActive(true);
        InventoryContent.gameObject.SetActive(false);
    }
}
