using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Shovel,
    Hand,
    Bucket,
    Trowel,
    FireExt,
    Brush
}
public class Tool : MonoBehaviour
{
    [SerializeField]private ToolType toolType;
    public Sprite toolSprite;

    public ToolType GetToolType()
    {
        return toolType;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
