using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //���� ������ ������ �����ϴ� ����
    public int crystalCount = 0;            //ũ����Ż ����
    public int plantCount = 0;              //�Ĺ� ����
    public int bushCount = 0;               //��Ǯ ����
    public int treeCount = 0;               //���� ����

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
                crystalCount++;    //�Ĺ� ���� ����
                Debug.Log($"�Ĺ� ȹ�� ! ���� ���� : {plantCount}");                      //���� �Ĺ� ���� ���
                break;
            case ItemType.Bush:
                crystalCount++;    //��Ǯ ���� ����
                Debug.Log($"��Ǯ ȹ�� ! ���� ���� : {bushCount}");                       //���� ��Ǯ ���� ���
                break;
            case ItemType.Tree:
                crystalCount++;    //���� ���� ����
                Debug.Log($"���� ȹ�� ! ���� ���� : {treeCount}");                       //���� ���� ���� ���
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
                    Debug.Log($"ũ����Ż {amount} tkdyd ! ���� ���� : {plantCount}");               
                    return true;
                }
                break;
            case ItemType.Bush:
                if (bushCount >= amount)
                {
                    bushCount -= amount;    
                    Debug.Log($"ũ����Ż {amount} tkdyd ! ���� ���� : {bushCount}");                
                    return true;
                }
                break;
            case ItemType.Tree:
                if (treeCount >= amount)
                {
                    treeCount -= amount;   
                    Debug.Log($"ũ����Ż {amount} tkdyd ! ���� ���� : {treeCount}");                
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
        Debug.Log("=======�κ��丮=======");
        Debug.Log($"ũ����Ż:{crystalCount}��");
        Debug.Log($"�Ĺ�:{plantCount}��");
        Debug.Log($"��Ǯ:{bushCount}��");
        Debug.Log($"����:{treeCount}��");
        Debug.Log("======================");
    }
}
