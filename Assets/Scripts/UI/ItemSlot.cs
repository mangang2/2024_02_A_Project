using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI countText;
    public Button useButton;

    private ItemType itemType;
    private int itemCount;

    public void Setup(ItemType type, int Count)
    {
        itemType = type;
        itemCount = Count;

        itemNameText.text = GetItemDisplayName(type);
        countText.text = Count.ToString();

        useButton.onClick.AddListener(UseItem);
    }

    private string GetItemDisplayName(ItemType type)
    {
        switch (type)
        {
            case ItemType.VeagetableStew: return "��ä ��Ʃ";
            case ItemType.fruitSalad: return "���� ������";
            case ItemType.RepairKit: return "���� ŰƮ";
            default: return type.ToString();
        }
    }

    private void UseItem()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        SurvivalStats stats = FindObjectOfType<SurvivalStats>();

        switch (itemType)
        {
            case ItemType.VeagetableStew:
                if(inventory.RemoveItem(itemType, 1))
                {
                    stats.EatFood(40f);
                    InventoryUIManager.Instance.RefreshInventory();
                }
                break;
            case ItemType.fruitSalad:
                if (inventory.RemoveItem(itemType, 1))
                {
                    stats.EatFood(50f);
                    InventoryUIManager.Instance.RefreshInventory();
                }
                break;
            case ItemType.RepairKit:
                if (inventory.RemoveItem(itemType, 1))
                {
                    stats.EatFood(25f);
                    InventoryUIManager.Instance.RefreshInventory();
                }
                break;
        }
    }

}
