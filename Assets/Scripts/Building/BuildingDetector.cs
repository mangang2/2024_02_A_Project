using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDetector : MonoBehaviour
{
    public float checkRaius = 3.0f;                 //아이템 감지 범위
    public Vector3 lastPostion;                     //플레이어의 마지막 위치 (플레이어 이동이 감지 될 경우 주변을 찾기 위한 변수)
    public float moveThreshold = 0.1f;              //이동 감지 임계값
    public ConstructibleBuilding currentNearbyBuilding;
    public BuildingCrafter currentBuildingCrafter;

  
    private void CheckForBuilding()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRaius);      //감지 범위 내의 모든 콜라이더를 찾아옴

        float closestDistance = float.MaxValue;          //가장 가까운 거리의 초기값
        ConstructibleBuilding closestBuilding = null;          //가장 가까운 아이템 초기값
        BuildingCrafter closesCrafter = null;

        foreach (Collider collider in hitColliders)
        {
            ConstructibleBuilding building = collider.GetComponent<ConstructibleBuilding>();
            if (building != null)
            {
                float distance = Vector3.Distance(transform.position, building.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBuilding = building;
                    closesCrafter = building.GetComponent<BuildingCrafter>();
                }
            }
        }
        if (closestBuilding != currentNearbyBuilding)
        {
            currentNearbyBuilding = closestBuilding;
            currentBuildingCrafter = closesCrafter;

            if (currentNearbyBuilding != null && !currentNearbyBuilding.isConstructed)
            {
                if(FloatingTextManager.instance != null)
                {
                    Vector3 textPostion = transform.position + Vector3.up * 0.5f;
                    FloatingTextManager.instance.Show(
                        $"[F] 키로 {currentNearbyBuilding.buildingName} 건설 (나무 {currentNearbyBuilding.requiredTree} 개 필요"
                        , currentNearbyBuilding.transform.position + Vector3.up);
                }
                
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lastPostion = transform.position;
        CheckForBuilding();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(lastPostion, transform.position) > moveThreshold)
        {
            CheckForBuilding();
            lastPostion = transform.position;
        }

        if(currentNearbyBuilding != null && Input.GetKeyDown(KeyCode.F))
        {
            if (!currentNearbyBuilding.isConstructed)
            {
                currentNearbyBuilding.StartConsturction(GetComponent<PlayerInventory>());
            }
            else if(currentBuildingCrafter != null)
            {
                Debug.Log($"{currentNearbyBuilding.buildingName} 의 제작 메뉴 열기");
                CraftingUIManager.Instance?.ShowUI(currentBuildingCrafter);
            }
        }
    }
}
