using System.Collections.Generic;
using BST;
using UnityEngine;

public class StockController : MonoBehaviour
{
    [SerializeField]
    private UIStockManager uiStockManager = null;
    private BSTTree tree = new BSTTree();

    private void Start()
    {
        foreach (Item item in StockDataManager.GetStock().items)
        {
            tree.Insert(item.id, item);
            uiStockManager.AddItemUI(item);
        }
    }

    public void SetItemBST(string itemName, int itemQuantity, float itemPrice, bool isOnSale, float salePercent)
    {
        int itemID = itemName.GetHashCode();
        BSTNode node = tree.Find(itemID);
        if (node == null)
        {
            Item item = new Item()
            {
                id = itemID,
                name = itemName,
                price = itemPrice,
                quantity = itemQuantity,
                isOnSale = isOnSale,
                salePercent = salePercent
            };
            tree.Insert(itemID, item);
        }
        else
        {
            itemID = node.GetData<Item>().name.GetHashCode();
            node.SetData(new Item
            {
                id = itemID,
                name = node.GetData<Item>().name,
                price = itemPrice,
                quantity = itemQuantity,
                isOnSale = isOnSale,
                salePercent = salePercent
            }, itemID);
        }
        List<Item> nodes = new List<Item>();
        tree.PreorderTraversal(nodes);
        StockDataManager.UpdateStock(nodes);
    }

    public Item GetItemBST(string itemName)
    {
        int itemID = itemName.GetHashCode();
        BSTNode node = tree.Find(itemID);
        if (node != null)
        {
            return node.GetData<Item>();
        }
        return null;
    }
}
