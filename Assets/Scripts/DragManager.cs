using UnityEngine;

public class DragManager : MonoBehaviour
{
    private static DragManager instance; // Singleton instance
    public static DragManager Instance => instance;

    private int activeDrags = 0; // Tracks how many objects are currently being dragged

    // Property to check if anything is being dragged
    public bool IsAnythingBeingDragged => activeDrags > 0;

    private void Awake()
    {
        // Ensure only one instance of DragManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Called when dragging starts
    public void RegisterDragStart()
    {
        activeDrags++;
        Debug.Log($"Dragging started. Active drags: {activeDrags}");
    }

    // Called when dragging ends
    public void RegisterDragEnd()
    {
        activeDrags = Mathf.Max(0, activeDrags - 1); // Prevent negative values
        Debug.Log($"Dragging ended. Active drags: {activeDrags}");
    }
}
