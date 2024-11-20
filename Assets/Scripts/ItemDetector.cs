using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ���� ����

public enum ItemType
{
    Crystal,     //ũ����Ż
    Plant,       //�Ĺ�
    Bush,        //��Ǯ
    Tree,        //����
    VeagetableStew,//��ä ��Ʃ
    fruitSalad,//���� ������
    RepairKit//���� ŰƮ
}
public class ItemDetector : MonoBehaviour
{
    public float checkRaius = 3.0f;                 //������ ���� ����
    public Vector3 lastPostion;                     //�÷��̾��� ������ ��ġ (�÷��̾� �̵��� ���� �� ��� �ֺ��� ã�� ���� ����)
    public float moveThreshold = 0.1f;              //�̵� ���� �Ӱ谪
    public CollectibleItem currentNearbyItem;         //���� ������ �ִ� ���� ������ ������
    
    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;          //���۽� ���� ��ġ�� ������ ��ġ�� ����
        CheckForItems();                           //�ʱ� ������ üũ ����
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        if(Vector3.Distance(lastPostion, transform.position) > moveThreshold)    //�÷��̾ ���� �Ÿ� �̻� �̵��ߴ��� üũ
        {
            CheckForItems();                                                     //�̵��� ������ üũ
            lastPostion = transform.position;                                    //���� ��ġ�� ������ ��ġ�� ������Ʈ
        }
       
        //����� �������� �ְ� EŰ�� ������ �� ������ ����
        if (currentNearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNearbyItem.CollectItem(GetComponent<PlayerInventory>());             //PlayerInventory �����Ͽ� ������ ����
        }
    }

    //�ֺ��� ���� ������ �������� �����ϴ� �Լ�

    private void CheckForItems()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRaius);      //���� ���� ���� ��� �ݶ��̴��� ã�ƿ�

        float closestDistance = float.MaxValue;          //���� ����� �Ÿ��� �ʱⰪ
        CollectibleItem closestItem = null;          //���� ����� ������ �ʱⰪ

        foreach (Collider collider in hitColliders)
        {
            CollectibleItem item = collider.GetComponent<CollectibleItem>();
            if(item != null && item.canCollect)
            {
                float distance = Vector3.Distance(transform.position, item.transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            } 
        }
        if(closestItem != currentNearbyItem)
        {
            currentNearbyItem = closestItem;
            if(currentNearbyItem != null)
            {
                Debug.Log($" [E] Ű�� ���� {currentNearbyItem.itemName} ����");
            }
        }
    }

    private void OnDrawGizmos()      //��Ƽ�� Sceneâ�� ���̴� Debug �׸�
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRaius);
    }
}
