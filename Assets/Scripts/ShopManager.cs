using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ProductType {
    Weapon = 0,
    Armor,
    Magick,
    Utility
}
public class ShopManager : MonoBehaviour {

    public static Action<string, int, Sprite, string> onButtonBuy;

    private int gold;
    [SerializeField]
    private Text Gold;

    [SerializeField]
    private Product ProductPrefab;
    [SerializeField]
    private RectTransform ContentShop;
    [SerializeField]
    private RectTransform ContentCategory;
    [SerializeField]
    private ScrollRect _scrollrect;
    [SerializeField]
    private List<ProductData> _ProductData = new List<ProductData>();
    [SerializeField]
    private List<ProductFields> _weapons = new List<ProductFields>();
    [SerializeField]
    private List<ProductFields> _armor = new List<ProductFields>();
    [SerializeField]
    private List<ProductFields> _magick = new List<ProductFields>();
    [SerializeField]
    private List<ProductFields> _utility = new List<ProductFields>();
    [SerializeField]
    private List<Product> ProductToBuy = new List<Product>();
    void Start() {
        gold = 100000000;
        Gold.text = gold.ToString();
        for (int i = 0; i < _ProductData.Count; i++) {
            InitializeShopCategories(_ProductData[i], i);
        }
        InitializeInventory();
    }
    private void ShopInitialize(int type) {
        _scrollrect.content = ContentShop;
        ContentCategory.gameObject.SetActive(false);
        ContentShop.gameObject.SetActive(true);
        switch ((ProductType)type) {
            case ProductType.Weapon:
                for (int i = 0; i < _weapons.Count; i++) {
                    InitializeShopComponent(_weapons[i], i);
                }
                break;
            case ProductType.Armor:
                for (int i = 0; i < _armor.Count; i++) {    
                    InitializeShopComponent(_armor[i], i);
                }
                break;
            case ProductType.Magick:
                for (int i = 0; i < _magick.Count; i++) {
                    InitializeShopComponent(_magick[i], i);
                }
                break;
            case ProductType.Utility:
                for (int i = 0; i < _utility.Count; i++) {
                    InitializeShopComponent(_utility[i], i);
                }
                break;
        }
    }
    private void InitializeShopComponent(ProductFields product, int i) {
        var clone = Instantiate(ProductPrefab, Vector3.zero, Quaternion.identity,ContentShop) as Product;
        clone.Instantiate(product.name, product.count, product.price, product.image);
        ProductToBuy.Add(clone);
        clone._buy_button.onClick.AddListener(delegate { Buy(product, i); });
    }
    private void Buy(ProductFields product, int i) {
        if (gold >= product.price && product.count > 0) {
            product.count--;
            product.BuyCount++;
            gold -= product.price;
            Gold.text = gold.ToString();
            ProductToBuy[i].Instantiate(product.name, product.count, product.price, product.image);
            onButtonBuy(product.name, product.BuyCount, product.image, product.content);
            _ProductData[product.index].BuyCount++;
            _ProductData[product.index].count--;
        }
    }
    private void Close() {
        _scrollrect.content = ContentCategory;
        ContentCategory.gameObject.SetActive(true);
        ContentShop.gameObject.SetActive(false);
        foreach (Product s in ProductToBuy) {
            Destroy(s.gameObject);
        }
        ProductToBuy.Clear();
    }
    private void InitializeShopCategories(ProductData data, int i) {
        ProductType type = data.type;
        switch (type) {
            case ProductType.Armor:
                _armor.Add(ProductFields.Initialize(data, i));
                break;
            case ProductType.Magick:
                _magick.Add(ProductFields.Initialize(data, i));
                break;
            case ProductType.Utility:
                _utility.Add(ProductFields.Initialize(data, i));
                break;
            case ProductType.Weapon:
                _weapons.Add(ProductFields.Initialize(data, i));
                break;
        }
    }
    private void InitializeInventory() {
        foreach (ProductData data in _ProductData) {
            if (data.BuyCount > 0) {
                onButtonBuy(data.name, data.BuyCount, data.image, data.content);
            }
        }
    }
}

public class ProductFields {
    public int count;
    public int BuyCount;
    public Sprite image;
    public int price;
    public string name;
    public int index;
    public string content = "";
    public static ProductFields Initialize(ProductData data, int i) {
        ProductFields product = new ProductFields();
        product.content = data.content;
        product.BuyCount = data.BuyCount;
        product.image = data.image;
        product.price = data.price;
        product.name = data.name;
        product.count = data.count;
        product.index = i;
        return product;
    }
}