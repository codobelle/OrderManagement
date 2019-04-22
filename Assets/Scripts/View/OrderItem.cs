using UnityEngine;
using UnityEngine.UI;

public class OrderItem : MonoBehaviour
{

    [SerializeField]
    private Text idText = null;
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text quantityText = null;

    public void Initialize(Item item)
    {
        idText.text = item.id.ToString();
        nameText.text = item.name;
        priceText.text = item.price.ToString() + "$";
        quantityText.text = item.quantity.ToString();
    }

}
