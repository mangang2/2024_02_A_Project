using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class RecipeButton : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI recipeName;
    public TextMeshProUGUI materialsText;
    public Button craftButton;

    private CraftingRecipe recipe;
    private BuildingCrafter crafter;
    private PlayerInventory playerInventory;

    public void Setup(CraftingRecipe recipe , BuildingCrafter crafter)
    {
        this.recipe = recipe;
        this.crafter = crafter;
        playerInventory = FindObjectOfType<PlayerInventory>();

        recipeName.text = recipe.itemName;
        UpdateMaterialsText();

        craftButton.onClick.AddListener(OnCraftButtonClicked);
    }

    private void UpdateMaterialsText()
    {
        string materials = " �ʿ� ��� : \n";
        for (int i = 0; i < recipe.requiredItems.Length; i++)
        {
            ItemType item = recipe.requiredItems[i];
            int requirted = recipe.requiredAmounts[i];
            int has = playerInventory.GetItemCount(item);
            materials += $"{item} : {has}/{requirted}\n";
        }
        materialsText.text = materials;
    }

    private void OnCraftButtonClicked()
    {
        crafter.TryCraft(recipe, playerInventory);
        UpdateMaterialsText();
    }
}
