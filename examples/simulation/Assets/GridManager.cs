using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    GameObject cellPrefab;

    [SerializeField]
    TMP_Text indeceseText;

    [SerializeField]
    int gridW = 10;

    [SerializeField]
    int gridH = 5;

    [SerializeField]
    Material hoverMaterial;
    [SerializeField]
    Material defaultMaterial;

    float cellWidth = 3;
    float cellHeight = 3;

    float spacing = 0.1f;

    CellScript[,] grid;
    CellScript currentHoverCell;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenereateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, LayerMask.GetMask("cell"))) {
            Vector2Int gridPosition = WorldPointToGridIndeces(hit.point);
            indeceseText.text = gridPosition.ToString();
            if (currentHoverCell != null && currentHoverCell != grid[gridPosition.x, gridPosition.y]) {
                currentHoverCell.gameObject.GetComponentInChildren<Renderer>().material = defaultMaterial;
            }
            currentHoverCell = grid[gridPosition.x, gridPosition.y];
            currentHoverCell.gameObject.GetComponentInChildren<Renderer>().material = hoverMaterial;
        }
    }

    Vector2Int WorldPointToGridIndeces(Vector3 worldPoint) {
        Vector2Int gridPosition = new Vector2Int();
        gridPosition.x = Mathf.FloorToInt(worldPoint.x / (cellWidth + spacing));
        gridPosition.y = Mathf.FloorToInt(worldPoint.z / (cellHeight + spacing));
        return gridPosition;
    }


    public void GenereateGrid() {
        for (int i = transform.childCount-1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        grid = new CellScript[gridW, gridH];
        for (int x = 0; x < gridW; x++) {
            for (int y = 0; y < gridH; y++) {
                Vector3 pos = new Vector3((cellWidth+spacing) * x, 0, (cellHeight+spacing) * y);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                cell.transform.localScale = new Vector3(cellWidth, 1, cellHeight);
                cell.transform.SetParent(transform);
                grid[x,y] = cell.GetComponent<CellScript>();
            }
        }
    }
}
