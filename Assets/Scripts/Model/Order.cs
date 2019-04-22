using System;
using System.Collections.Generic;

[Serializable]
public class Orders
{
    public List<Order> orders = new List<Order>();
}

[Serializable]
public class Order
{
    public int id;
    public string clientName;
    public List<Item> items = new List<Item>();
}

[Serializable]
public class Item
{
    public int id;
    public string name;
    public float price;
    public int quantity;
    public bool isOnSale;
    public float salePercent;
}
