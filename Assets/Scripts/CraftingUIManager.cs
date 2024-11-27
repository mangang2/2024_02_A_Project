using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject crafringPanel;
    public TextMeshProUGUI buildingNameText;
    public Transform recipeContainer;
    public Button closeButton;
    public GameObject recipeButtonPrefabs;

    private BuildingCrafter currentCrafter;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        crafringPanel.SetActive(false);
    }

    private void RefreshRecipeList()
    {
        foreach(Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        if(currentCrafter != null && currentCrafter.recipes != null)
        {
            foreach(CraftingRecipe recipe in currentCrafter.recipes)
            {
                GameObject buttonObj = Instantiate(recipeButtonPrefabs, recipeContainer);
                RecipeButton recipeBotton = buttonObj.GetComponent<RecipeButton>();
                recipeBotton.Setup(recipe, currentCrafter);

            }
        }
    }

    public void ShowUI(BuildingCrafter crafter)
    {
        currentCrafter = crafter;
        crafringPanel.SetActive (true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if(crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
            RefreshRecipeList();
        }
    }

    public void HideUI()
    {
        crafringPanel.SetActive(false);
        currentCrafter = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => HideUI());
    }
}
