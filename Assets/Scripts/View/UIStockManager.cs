using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStockManager : MonoBehaviour
{
    [Header ("InventoryTab")]
    [SerializeField]
    private StockController controller = null;
    [SerializeField]
    private InputField itemNameInputField = null;
    [SerializeField]
    private InputField itemPriceInputField = null;
    [SerializeField]
    private InputField itemQuantityInputField = null;
    [SerializeField]
    private Text itemSalePercentText = null;

    [Header("OrderTab")]
    [SerializeField]
    private Transform stockItemStockContent = null;
    [SerializeField]
    private Transform stockItemOrderContent = null;
    [SerializeField]
    private GameObject stockItem = null;
    [SerializeField]
    private GameObject quantityPanel = null;

    private List<GameObject> itemGOList = new List<GameObject>();
    private List<GameObject> itemGOOrderList = new List<GameObject>();
    private bool wasOrderCreated;
    private bool isOnSale;
    private float salePercent;

    public void SetItemOnSale(bool value)
    {
        isOnSale = value;
    }

    public void SetItemSalePercent(float percent)
    {
        salePercent = percent;
        itemSalePercentText.text = (salePercent * 100).ToString() + "%";
    }

    public void SaveStockItem()
    {
        string itemName = itemNameInputField.text;
        int.TryParse(itemQuantityInputField.text, out int itemQuantity);
        itemQuantity = Mathf.Abs(itemQuantity);
        float.TryParse(itemPriceInputField.text, out float itemPrice);
        itemPrice = Mathf.Abs(itemPrice);

        if (isOnSale)
        {
            itemPrice = itemPrice - salePercent * itemPrice;
        }

        if (!string.IsNullOrEmpty(itemName) && itemQuantity > 0)
        {
            controller.SetItemBST(itemName, itemQuantity, itemPrice, isOnSale, salePercent);
            UpdateUI();
        }
    }

    public void SetOrderState(bool value)
    {
        wasOrderCreated = value;
    }

    public void UpdateUI()
    {
        ClearUI();
        foreach (Item item in StockDataManager.GetStock().items)
        {
            AddItemUI(item);
        }
    }
    
    public void AddItemUI(Item item)
    {
        GameObject itemGameObject = Instantiate(stockItem, stockItemStockContent);
        itemGOList.Add(itemGameObject);
        itemGameObject.GetComponent<StockItem>().Initialize(item, OpenQuantityPanel);
    }

    public void UpdateStockContentOrder()
    {
        foreach (var item in itemGOOrderList)
        {
            Destroy(item);
        }
        foreach (Item item in StockDataManager.GetStock().items)
        {
            GameObject itemGameObject = Instantiate(stockItem, stockItemOrderContent);
            itemGOOrderList.Add(itemGameObject);
            itemGameObject.GetComponent<StockItem>().Initialize(item, OpenQuantityPanel);
        }
    }

    private void ClearUI()
    {
        itemNameInputField.text = "";
        itemPriceInputField.text = "";
        itemQuantityInputField.text = "";
        foreach (var item in itemGOList)
        {
            Destroy(item);
        }
    }

    private void OpenQuantityPanel(Item item)
    {
        if (wasOrderCreated)
        {
            quantityPanel.SetActive(true);
            quantityPanel.GetComponent<QuantityPanel>().SetItem(item);
        }
    }

}
