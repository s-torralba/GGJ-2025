using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class OptimizedDynamicGridResizer : MonoBehaviour
{
    public RectTransform container;     // The RectTransform of the grid's parent container
    public int maxColumns = 4;          // Maximum number of columns allowed
    public int minColumns = 1;          // Minimum number of columns allowed
    public Vector2 minCellSize = new Vector2(50, 50); // Minimum size of each cell
    public Vector2 maxCellSize = new Vector2(200, 200); // Maximum size of each cell
    public int padding = 10;            // Padding to leave around the edges

    private GridLayoutGroup gridLayout;

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        AdjustGridCellSize(); // Initial adjustment
    }

    private void Update()
    {
        AdjustGridCellSize(); // Keep adjusting in case of changes
    }

    private void AdjustGridCellSize()
    {
        if (container == null || gridLayout == null || container.rect.width <= 0 || container.rect.height <= 0)
            return;

        // Calculate available container space
        float containerWidth = container.rect.width - gridLayout.padding.left - gridLayout.padding.right - (padding * 2);
        float containerHeight = container.rect.height - gridLayout.padding.top - gridLayout.padding.bottom - (padding * 2);

        // Determine the number of columns dynamically based on the max width
        int columns = Mathf.Clamp(Mathf.FloorToInt(containerWidth / minCellSize.x), minColumns, maxColumns);
        int rows = Mathf.CeilToInt((float)transform.childCount / columns);

        // Calculate cell size while respecting minimum/maximum bounds
        float cellWidth = Mathf.Clamp((containerWidth - (gridLayout.spacing.x * (columns - 1))) / columns, minCellSize.x, maxCellSize.x);
        float cellHeight = Mathf.Clamp((containerHeight - (gridLayout.spacing.y * (rows - 1))) / rows, minCellSize.y, maxCellSize.y);

        // Apply the new cell size
        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);

        // Ensure the constraint count matches the column count
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;
    }
}
