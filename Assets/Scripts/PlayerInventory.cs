using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //각각 아이템 개수를 저장하는 변수
    public int crystalCount = 0;            //크리스탈 개수
    public int plantCount = 0;              //식물 개수
    public int bushCount = 0;               //수풀 개수
    public int treeCount = 0;               //나무 개수

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
                crystalCount++;    //식물 개수 증가
                Debug.Log($"식물 획득 ! 현재 개수 : {plantCount}");                      //현재 식물 개수 출력
                break;
            case ItemType.Bush:
                crystalCount++;    //수풀 개수 증가
                Debug.Log($"수풀 획득 ! 현재 개수 : {bushCount}");                       //현재 수풀 개수 출력
                break;
            case ItemType.Tree:
                crystalCount++;    //나무 개수 증가
                Debug.Log($"나무 획득 ! 현재 개수 : {treeCount}");                       //현재 나무 개수 출력
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
                    Debug.Log($"크리스탈 {amount} tkdyd ! 현재 개수 : {plantCount}");               
                    return true;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;    
                    Debug.Log($"크리스탈 {amount} tkdyd ! 현재 개수 : {bushCount}");                
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;   
                    Debug.Log($"크리스탈 {amount} tkdyd ! 현재 개수 : {treeCount}");                
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
