using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class OrderDataManager
{
    private static Orders orders;

    private static string projectFilePath = "/orders.json";

    public static Orders Orders
    {
        get => LoadData();
    }

    public static void UpdateOrders(List<Order> orderList)
    {
        orders.orders = new List<Order>();
        foreach (var order in orderList)
        {
            orders.orders.Add(new Order
            {
                id = order.id,
                clientName = order.clientName,
                items = order.items
            });
        }
        SaveData(orders);
    }

    //LoadData from json file
    private static Orders LoadData()
    {
        string filePath = Application.dataPath + projectFilePath;
        if (File.Exists(filePath))
        {
            try
            {
                string dataAsJson = File.ReadAllText(filePath);
                orders = JsonUtility.FromJson<Orders>(dataAsJson);
            }
            catch (Exception e)
            {
                Debug.Log("Exception caught" + e);
            }
        }
        else
        {
            orders = new Orders();
        }
        return orders;
    }

    //SaveData in json file
    private static void SaveData(Orders orders)
    {
        try
        {
            string filePath = Application.dataPath + projectFilePath;
            string dataAsJson = JsonUtility.ToJson(orders);
            File.WriteAllText(filePath, dataAsJson);
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught" + e);
        }
    }
}
