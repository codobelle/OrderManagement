using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StockItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text quantityText = null;
    private Action<Item> callbackDoubleClick;
    private Item stockItem;

    public void Initialize(Item item, Action<Item> callback)
    {
        stockItem = item;
        callbackDoubleClick = callback;
        nameText.text = item.name;
        priceText.text = item.price.ToString() + "$";
        quantityText.text = item.quantity.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 2)
            OnDoubleClick();
    }

    private void OnDoubleClick()
    {
        callbackDoubleClick.Invoke(stockItem);
    }
}
