using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOrdersManager : MonoBehaviour
{
    [SerializeField]
    private UIStockManager uiStockManager = null;
    [SerializeField]
    private OrderController controller = null;
    [SerializeField]
    private InputField customerNameInputField = null;
    [SerializeField]
    private GameObject orderItem = null;
    [SerializeField]
    private Transform orderItemStockContent = null;
    [SerializeField]
    private GameObject orderInfoButtonPrefab = null;
    [SerializeField]
    private Transform allOrdersContent = null;
    [SerializeField]
    private Transform allHistoryOrdersContent = null;

    [SerializeField]
    private GameObject finalOrderItemsPanel = null;
    [SerializeField]
    private Text finalOrderClientNamePanel = null;
    [SerializeField]
    private Transform allFinalOrderItemsContent = null;
    [SerializeField]
    private GameObject finalItemOrderPrefab = null;

    private Order currentOrder;
    private string customerName;

    private List<GameObject> itemGOList = new List<GameObject>();
    private List<GameObject> orderGOList = new List<GameObject>();
    private List<GameObject> historyOrderGOList = new List<GameObject>();
    private List<GameObject> finalItemOrderGOList = new List<GameObject>();

    public void AddItemUI(Item item)
    {
        GameObject orderItemGameObject = Instantiate(orderItem, orderItemStockContent);
        itemGOList.Add(orderItemGameObject);
        orderItemGameObject.GetComponent<OrderItem>().Initialize(item);
    }

    public void CreateOrder()
    {
        customerName = customerNameInputField.text;
        if (!string.IsNullOrEmpty(customerName))
        {
            currentOrder = controller.GetOrderBST(customerName);
            if (currentOrder != null)
            {
                UpdateUI();
            }
            else
            {
                controller.SetOrderBST(customerName, new List<Item>());
            }
            uiStockManager.UpdateStockContentOrder();
            uiStockManager.SetOrderState(true);
        }
    }

    public void Done()
    {
        customerNameInputField.text = "";
        ClearUI();
        uiStockManager.UpdateStockContentOrder();
        uiStockManager.SetOrderState(false);
    }

    public void AddItemOrder(Item item)
    {
        currentOrder = controller.GetOrderBST(customerName);
        if (currentOrder != null)
        {
            Item itemToUpdate = currentOrder.items.Find(i => i.id == item.id);
            if (itemToUpdate != null)
            {
                itemToUpdate.quantity += item.quantity;
            }
            else
            {
                currentOrder.items.Add(item);
            }
            controller.SetOrderBST(customerName, currentOrder.items);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        ClearUI();
        foreach (Item item in currentOrder.items)
        {
            AddItemUI(item);
        }
    }

    public void ShowAllOrders()
    {
        foreach(var order in orderGOList)
        {
            Destroy(order);
        }
        foreach (var order in OrderDataManager.Orders.orders)
        {
            GameObject orderGameObject = Instantiate(orderInfoButtonPrefab, allOrdersContent);
            orderGOList.Add(orderGameObject);
            orderGameObject.GetComponent<OrderInfo>().Initialize(order, ShowItemsOrder);
        }
    }

    public void Proceed()
    {
        List<Order> ordersToHistory = OrderHistoryDataManager.Orders.orders;
        ordersToHistory.AddRange(OrderDataManager.Orders.orders);

        OrderHistoryDataManager.UpdateOrders(ordersToHistory);
        OrderDataManager.UpdateOrders(new List<Order>());
        controller.ClearTree();
        foreach (var order in orderGOList)
        {
            Destroy(order);
        }
    }

    public void ShowAllHistoryOrders()
    {
        foreach (var order in historyOrderGOList)
        {
            Destroy(order);
        }
        foreach (var order in OrderHistoryDataManager.Orders.orders)
        {
            GameObject orderGameObject = Instantiate(orderInfoButtonPrefab, allHistoryOrdersContent);
            historyOrderGOList.Add(orderGameObject);
            orderGameObject.GetComponent<OrderInfo>().Initialize(order, ShowItemsOrder);
        }
    }

    private void ShowItemsOrder(Order order)
    {
        finalOrderItemsPanel.SetActive(true);
        finalOrderClientNamePanel.text = order.clientName;
        foreach (var item in finalItemOrderGOList)
        {
            Destroy(item);
        }
        foreach (var item in order.items)
        {
            GameObject finalOrderItemGameObject = Instantiate(finalItemOrderPrefab, allFinalOrderItemsContent);
            finalItemOrderGOList.Add(finalOrderItemGameObject);
            finalOrderItemGameObject.GetComponent<OrderItem>().Initialize(item);
        }
    }

    private void ClearUI()
    {
        foreach (var item in itemGOList)
        {
            Destroy(item);
        }
    }
}
