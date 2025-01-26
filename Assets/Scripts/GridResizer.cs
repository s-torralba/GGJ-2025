using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class OptimizedDynamicGridResizer : MonoBehaviour
{
    public RectTransform container;
    public int maxColumns = 4;
    public int minColumns = 1;
    public Vector2 minCellSize = new Vector2(50, 50);
    public Vector2 maxCellSize = new Vector2(200, 200);
    public int padding = 10;

    private GridLayoutGroup gridLayout;

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        AdjustGridCellSize();
    }

    private void Update()
    {
        AdjustGridCellSize();
    }

    private void AdjustGridCellSize()
    {
        if (container == null || gridLayout == null || container.rect.width <= 0 || container.rect.height <= 0)
            return;

        float containerWidth = container.rect.width - gridLayout.padding.left - gridLayout.padding.right - (padding * 2);
        float containerHeight = container.rect.height - gridLayout.padding.top - gridLayout.padding.bottom - (padding * 2);

        int columns = Mathf.Clamp(Mathf.FloorToInt(containerWidth / minCellSize.x), minColumns, maxColumns);
        int rows = Mathf.CeilToInt((float)transform.childCount / columns);

        float cellWidth = Mathf.Clamp((containerWidth - (gridLayout.spacing.x * (columns - 1))) / columns, minCellSize.x, maxCellSize.x);
        float cellHeight = Mathf.Clamp((containerHeight - (gridLayout.spacing.y * (rows - 1))) / rows, minCellSize.y, maxCellSize.y);

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;
    }
}
