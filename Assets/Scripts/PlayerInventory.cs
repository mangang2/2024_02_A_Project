using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;

    //각각 아이템 개수를 저장하는 변수
    public int crystalCount = 0;            //크리스탈 개수
    public int plantCount = 0;              //식물 개수
    public int bushCount = 0;               //수풀 개수
    public int treeCount = 0;               //나무 개수

    public int veagetableStewCount = 0;
    public int fruitSaladCount = 0;
    public int repairKitCount = 0;

    public void Start()
    {
        survivalStats = GetComponent<SurvivalStats>();
    }

    public void UseItem(ItemType itemType)
    {
        if (GetItemCount(itemType) <= 0)
        {
            return;
        }

        switch (itemType)
        {
            case ItemType.VeagetableStew:
                RemoveItem(ItemType.VeagetableStew, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].hungerRestoreAmount);
                break;
            case ItemType.fruitSalad:
                RemoveItem(ItemType.fruitSalad, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[1].hungerRestoreAmount);
                break;
            case ItemType.RepairKit:
                RemoveItem(ItemType.RepairKit, 1);
                survivalStats.EatFood(RecipeList.KitchenRecipes[0].repairAmount);
                break;
        }
    }
    public void AddItem(ItemType itemType, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            AddItem(itemType);
        }
    }

    //아이템을 추가하는 함수, 아이템 종류에 따라서 해당 아이템의 개수를 증가 시킴
    public void AddItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;    //크리스탈 개수 증가
                Debug.Log($"크리스탈 획득 ! 현재 개수 : {crystalCount}");                //현재 크리스탈 개수 출력
                break; 
            case ItemType.Plant:
                plantCount++;    //식물 개수 증가
                Debug.Log($"식물 획득 ! 현재 개수 : {plantCount}");                      //현재 식물 개수 출력
                break;
            case ItemType.Bush:
                bushCount++;    //수풀 개수 증가
                Debug.Log($"수풀 획득 ! 현재 개수 : {bushCount}");                       //현재 수풀 개수 출력
                break;
            case ItemType.Tree:
                treeCount++;    //나무 개수 증가
                Debug.Log($"나무 획득 ! 현재 개수 : {treeCount}");                       //현재 나무 개수 출력
                break;
            case ItemType.VeagetableStew:
                crystalCount++;    //나무 개수 증가
                Debug.Log($"나무 획득 ! 현재 개수 : {veagetableStewCount}");                       //현재 나무 개수 출력
                break;
            case ItemType.fruitSalad:
                fruitSaladCount++;    //나무 개수 증가
                Debug.Log($"나무 획득 ! 현재 개수 : {fruitSaladCount}");                       //현재 나무 개수 출력
                break;
            case ItemType.RepairKit:
                repairKitCount++;    //나무 개수 증가
                Debug.Log($"나무 획득 ! 현재 개수 : {repairKitCount}");                       //현재 나무 개수 출력
                break;
        }
    }

    public bool RemoveItem(ItemType itemType , int amount = 1)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                if (crystalCount >= amount)
                {
                    crystalCount -= amount;    //크리스탈 개수 증가
                    Debug.Log($"크리스탈 {amount} tkdyd ! 현재 개수 : {crystalCount}");                //현재 크리스탈 개수 출력
                    return true;
                }
                break;
            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;    
                    Debug.Log($"식물 {amount} tkdyd ! 현재 개수 : {plantCount}");               
                    return true;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;    
                    Debug.Log($"덤불 {amount} tkdyd ! 현재 개수 : {bushCount}");                
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;   
                    Debug.Log($"나무 {amount} tkdyd ! 현재 개수 : {treeCount}");                
                    return true;
                }
                break;
            case ItemType.VeagetableStew:
                if (veagetableStewCount >= amount)
                {
                    veagetableStewCount -= amount;
                    Debug.Log($"야채 스튜 {amount} tkdyd ! 현재 개수 : {veagetableStewCount}");
                    return true;
                }
                break;
            case ItemType.fruitSalad:
                if (fruitSaladCount >= amount)
                {
                    fruitSaladCount -= amount;
                    Debug.Log($"과일 샐러드 {amount} tkdyd ! 현재 개수 : {fruitSaladCount}");
                    return true;
                }
                break;
            case ItemType.RepairKit:
                if (repairKitCount >= amount)
                {
                    repairKitCount -= amount;
                    Debug.Log($"수리 키드 {amount} tkdyd ! 현재 개수 : {repairKitCount}");
                    return true;
                }
                break;

        }
        return false;

    }
    
    public int GetItemCount(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                return crystalCount;
            case ItemType.Plant:
                return plantCount;
            case ItemType.Bush:
                return bushCount;
            case ItemType.Tree:
                return treeCount;
            case ItemType.VeagetableStew:
                return veagetableStewCount;
            case ItemType.fruitSalad:
                return fruitSaladCount;
            case ItemType.RepairKit:
                return repairKitCount;

            default:
                return 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void ShowInventory()
    {
        Debug.Log("=======인벤토리=======");
        Debug.Log($"크리스탈:{crystalCount}개");
        Debug.Log($"식물:{plantCount}개");
        Debug.Log($"수풀:{bushCount}개");
        Debug.Log($"나무:{treeCount}개");
        Debug.Log("======================");
    }
}
