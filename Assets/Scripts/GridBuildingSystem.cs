using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridCell
{
    public Vector3Int Position;      //셀의 그리드 내 위치
    public bool IsOccupied;          //셀이 건물로 차있지 여부
    public GameObject Building;      //셀에 배치된 건물 객체

    public GridCell(Vector3Int position)         //클래스 이름과 동일한 함수 (생성자) 클래스가 생성될때 호출
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{

    [SerializeField] private int width = 10;     //그리드의 가로 크기
    [SerializeField] private int height = 10;    //그리드의 새로 크기
    [SerializeField] private float cellSize = 1.0f;      //각 셀의 크기
    [SerializeField] private GameObject cellPrefabs;    //셀 프리팹
    [SerializeField] private GameObject buildingPrefabs;  //빌딩 프리팹

    [SerializeField] private PlayerController playerController;     //플레이어 컨트롤러 참조

    [SerializeField] private Grid grid;
    private GridCell[,] cells;                     //GridCell 클래스를 2차원 배열로 선언
    private Camera firstPersonCamera;

    void Start()
    {
        firstPersonCamera = playerController.firstPersonCamera;
        CreateGrid();
    }
    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize);

        cells = new GridCell[width, height];
        Vector3 gridCenter = playerController.transform.position;
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2.0f);

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height ; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition);
                GameObject cellObject = Instantiate(cellPrefabs, worldPosition, cellPrefabs.transform.rotation);
                cellObject.transform.SetParent(transform);

                cells[x, z] = new GridCell(cellPosition);
            }
        }
    }

    void Update()
    {
        Vector3 lookPosition = GetLookPosition();
        if(lookPosition != Vector3.zero)                     //보고 있는 좌표가 있는지 검사
        {
            Vector3Int gridPosition = grid.WorldToCell(lookPosition);    //그리드 월드 포지션 전환
            if (isValidGridPosition(gridPosition))                        //위치가 유효한지 확인
            {
                HighlightCell(gridPosition);
                if (Input.GetMouseButton(0))
                {
                    PlaceBuilding(gridPosition);
                }
                if (Input.GetMouseButton(1))
                {
                    RemoveBuilding(gridPosition);
                }
            }
        }
    }

    //그리드 셀에 건물을 배치하는 메서드
    private void PlaceBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];
        if (!cell.IsOccupied)
        {
            Vector3 worldPosition = grid.GetCellCenterWorld(gridPosition);
            GameObject building = Instantiate(buildingPrefabs, worldPosition, Quaternion.identity);
            cell.IsOccupied = true;
            cell.Building = building;

        }
    }

    private void RemoveBuilding(Vector3Int gridPosition)
    {
        GridCell cell = cells[gridPosition.x, gridPosition.z];     //위치 기반으로 cell을 받아온다.
        if (cell.IsOccupied)                                       //해당 위치에 건물이 있는지 확인한다.
        {
            Destroy(cell.Building);                        //Cell 건물을 제거한다.
            cell.IsOccupied = false;                       //건물 확인 값
            cell.Building = null;            
        }
    }


    private void HighlightCell(Vector3Int gridPosition)
    {
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GameObject cellObject = cells[x, z].Building != null ? cells[x, z].Building : transform.GetChild(x * height + z).gameObject;
                cellObject.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        //특정 샐에 건물이 있으면 빨간색 아니면 초록색
        GridCell cell = cells[gridPosition.x, gridPosition.z ];
        GameObject highlightObject = cell.Building != null ? cell.Building : transform.GetChild(gridPosition.x * height + gridPosition.z).gameObject;
        highlightObject.GetComponent<Renderer>().material.color = cell.IsOccupied ? Color.red : Color.green;
    }


    //그리드 포지션이 유효한지 확인하는 메서드
    private bool isValidGridPosition(Vector3Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height;
    }
    private Vector3 GetLookPosition()
    {
        if(playerController.isFirstperson)
        {
            Ray ray = new Ray(firstPersonCamera.transform.position , firstPersonCamera.transform.forward);
            if(Physics.Raycast(ray, out RaycastHit hitlnfo, 5.0f))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitlnfo.distance, Color.red);
                return hitlnfo.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.white);
            }

            
        }

        else
        {
            Vector3 characterPosition = playerController.transform.position;
            Vector3 characterFoward = playerController.transform.forward;
            Vector3 ratOrigin = characterPosition + Vector3.up * 1.5f + characterFoward * 0.5f;
            Vector3 rayDirection = (characterFoward - Vector3.up).normalized;

            Ray ray = new Ray(ratOrigin, rayDirection);

            if(Physics.Raycast(ray, out RaycastHit hitlnfo, 5.0f))
            {
                Debug.DrawRay(ray.origin, ray.direction * hitlnfo.distance, Color.blue);
                return hitlnfo.point;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.white);
            }
        }
        return Vector3.zero;
    }
    //그리드 셀을 Glzmo로 표기하는 메서드
    private void OnDrawGizmos()      //유니티 Scene창에 보이는 Debug 그림
    {
        Gizmos.color = Color.blue;
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z);
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition);              
                cells[x, z] = new GridCell(cellPosition);
            }

        }
    }

    
}
