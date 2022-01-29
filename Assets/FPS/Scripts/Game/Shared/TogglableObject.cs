using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

public class TogglableObject : MonoBehaviour
{
    public bool IsSolid { get; set; }
    public ActiveColor AssignedActiveColor { get; private set; }
    private Mesh Mesh;
    private void Awake()
    {
        
    }

	private void Init(Mesh mesh, ActiveColor activeColor, bool isSolid)
	{
        AssignedActiveColor = activeColor;
        Mesh = mesh;
        IsSolid = isSolid;
	}
}
