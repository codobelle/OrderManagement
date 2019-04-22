using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BST;

public class OrderController : MonoBehaviour
{
    private BSTTree tree = new BSTTree();

    private void Start()
    {
        foreach (Order order in OrderDataManager.Orders.orders)
        {
            tree.Insert(order.id, order);
        }
    }

    public void SetOrderBST(string customerName, List<Item> orderItems)
    {
        int orderId = customerName.GetHashCode();
        BSTNode node = tree.Find(orderId);
        if (node == null)
        {
            Order order = new Order()
            {
                id = orderId,
                clientName = customerName,
                items = new List<Item>()
            };

            tree.Insert(orderId, order);
        }
        else
        {
            orderId = node.GetData<Order>().clientName.GetHashCode();
            node.SetData(new Order
            {
                id = orderId,
                clientName = customerName,
                items = orderItems
            }, orderId);
        }
        List<Order> nodes = new List<Order>();
        tree.PreorderTraversal(nodes);
        OrderDataManager.UpdateOrders(nodes);
    }

    public Order GetOrderBST(string customerName)
    {
        int orderID = customerName.GetHashCode();
        BSTNode node = tree.Find(orderID);
        if (node != null)
        {
            return node.GetData<Order>();
        }
        return null;
    }

    public void ClearTree()
    {
        tree = new BSTTree();
    }
}
