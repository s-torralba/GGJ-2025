using UnityEngine;

public class DragManager : MonoBehaviour
{
    private static DragManager instance;
    public static DragManager Instance => instance;

    private int activeDrags = 0;

    public bool IsAnythingBeingDragged => activeDrags > 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterDragStart()
    {
        activeDrags++;
        Debug.Log($"Dragging started. Active drags: {activeDrags}");
    }

    public void RegisterDragEnd()
    {
        activeDrags = Mathf.Max(0, activeDrags - 1);
        Debug.Log($"Dragging ended. Active drags: {activeDrags}");
    }
}
