using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private SurvivalStats survivalStats;

    //���� ������ ������ �����ϴ� ����
    public int crystalCount = 0;            //ũ����Ż ����
    public int plantCount = 0;              //�Ĺ� ����
    public int bushCount = 0;               //��Ǯ ����
    public int treeCount = 0;               //���� ����

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

    //�������� �߰��ϴ� �Լ�, ������ ������ ���� �ش� �������� ������ ���� ��Ŵ
    public void AddItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Crystal:
                crystalCount++;    //ũ����Ż ���� ����
                Debug.Log($"ũ����Ż ȹ�� ! ���� ���� : {crystalCount}");                //���� ũ����Ż ���� ���
                break; 
            case ItemType.Plant:
                plantCount++;    //�Ĺ� ���� ����
                Debug.Log($"�Ĺ� ȹ�� ! ���� ���� : {plantCount}");                      //���� �Ĺ� ���� ���
                break;
            case ItemType.Bush:
                bushCount++;    //��Ǯ ���� ����
                Debug.Log($"��Ǯ ȹ�� ! ���� ���� : {bushCount}");                       //���� ��Ǯ ���� ���
                break;
            case ItemType.Tree:
                treeCount++;    //���� ���� ����
                Debug.Log($"���� ȹ�� ! ���� ���� : {treeCount}");                       //���� ���� ���� ���
                break;
            case ItemType.VeagetableStew:
                crystalCount++;    //���� ���� ����
                Debug.Log($"���� ȹ�� ! ���� ���� : {veagetableStewCount}");                       //���� ���� ���� ���
                break;
            case ItemType.fruitSalad:
                fruitSaladCount++;    //���� ���� ����
                Debug.Log($"���� ȹ�� ! ���� ���� : {fruitSaladCount}");                       //���� ���� ���� ���
                break;
            case ItemType.RepairKit:
                repairKitCount++;    //���� ���� ����
                Debug.Log($"���� ȹ�� ! ���� ���� : {repairKitCount}");                       //���� ���� ���� ���
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
                    crystalCount -= amount;    //ũ����Ż ���� ����
                    Debug.Log($"ũ����Ż {amount} tkdyd ! ���� ���� : {crystalCount}");                //���� ũ����Ż ���� ���
                    return true;
                }
                break;
            case ItemType.Plant:
                if (plantCount >= amount)
                {
                    plantCount -= amount;    
                    Debug.Log($"�Ĺ� {amount} tkdyd ! ���� ���� : {plantCount}");               
                    return true;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;    
                    Debug.Log($"���� {amount} tkdyd ! ���� ���� : {bushCount}");                
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;   
                    Debug.Log($"���� {amount} tkdyd ! ���� ���� : {treeCount}");                
                    return true;
                }
                break;
            case ItemType.VeagetableStew:
                if (veagetableStewCount >= amount)
                {
                    veagetableStewCount -= amount;
                    Debug.Log($"��ä ��Ʃ {amount} tkdyd ! ���� ���� : {veagetableStewCount}");
                    return true;
                }
                break;
            case ItemType.fruitSalad:
                if (fruitSaladCount >= amount)
                {
                    fruitSaladCount -= amount;
                    Debug.Log($"���� ������ {amount} tkdyd ! ���� ���� : {fruitSaladCount}");
                    return true;
                }
                break;
            case ItemType.RepairKit:
                if (repairKitCount >= amount)
                {
                    repairKitCount -= amount;
                    Debug.Log($"���� Ű�� {amount} tkdyd ! ���� ���� : {repairKitCount}");
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
        Debug.Log("=======�κ��丮=======");
        Debug.Log($"ũ����Ż:{crystalCount}��");
        Debug.Log($"�Ĺ�:{plantCount}��");
        Debug.Log($"��Ǯ:{bushCount}��");
        Debug.Log($"����:{treeCount}��");
        Debug.Log("======================");
    }
}
