using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderInfo : MonoBehaviour
{
    [SerializeField]
    private Text orderIDText = null;
    [SerializeField]
    private Text customerNameText = null;
    [SerializeField]
    private Text priceText = null;

    public void Initialize(Order order, Action<Order> callback)
    {
        GetComponent<Button>().onClick.AddListener(() => callback(order));
        orderIDText.text = order.id.ToString();
        customerNameText.text = order.clientName;
        float price = 0;
        foreach (var item in order.items)
        {
            price += item.price * item.quantity;
        }
        priceText.text = price.ToString() + "$";
    }
}
