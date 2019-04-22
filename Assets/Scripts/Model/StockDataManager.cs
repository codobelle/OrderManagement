using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StockDataManager
{
    private static Stock stock;

    private static string projectFilePath = "/stock.json";

    public static Stock GetStock()
    {
        return LoadData();
    }

    public static void UpdateStock(List<Item> stockItems)
    {
        stock.items = new List<Item>();
        foreach (var item in stockItems)
        {
            stock.items.Add(new Item
            {
                id = item.id,
                name = item.name,
                price = item.price,
                quantity = item.quantity
            });
        }
        SaveData(stock);
    }

    //LoadData from json file
    private static Stock LoadData()
    {
        string filePath = Application.dataPath + projectFilePath;
        if (File.Exists(filePath))
        {
            try
            {
                string dataAsJson = File.ReadAllText(filePath);
                stock = JsonUtility.FromJson<Stock>(dataAsJson);
            }
            catch (Exception e)
            {
                Debug.Log("Exception caught" + e);
            }
        }
        else
        {
            stock = new Stock();
        }
        return stock;
    }

    //SaveData in json file
    private static void SaveData(Stock stock)
    {
        try
        {
            string filePath = Application.dataPath + projectFilePath;
            string dataAsJson = JsonUtility.ToJson(stock);
            File.WriteAllText(filePath, dataAsJson);
        }
        catch (Exception e)
        {
            Debug.Log("Exception caught" + e);
        }
    }

    [Serializable]
    public class Stock
    {
        public List<Item> items = new List<Item>();
    }
}
