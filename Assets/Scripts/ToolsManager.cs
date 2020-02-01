using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{
    private Tool selectedTool;
    public SpriteRenderer cursorTool;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        cursorTool.transform.localPosition = Vector2.Lerp(cursorTool.transform.position, mousePosition, moveSpeed);
    }

    public void SelectTool(Tool childTool)
    {
        selectedTool = childTool;
        cursorTool.sprite = selectedTool.toolSprite;
    }
 
}
