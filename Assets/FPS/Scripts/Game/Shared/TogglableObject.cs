using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using Assets.FPS.Scripts.Game.Managers;
using System;

public class TogglableObject : MonoBehaviour
{
    public bool IsSolid;
    public bool IsFloor;
    public ActiveColor AssignedActiveColor;
    private Mesh Mesh;
    private Collider collider;
	private MeshRenderer meshRenderer;
	private void Awake()
	{
		collider = GetComponentInChildren<MeshCollider>();
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}
	private void Start()
    {
        AssignSelfToManager();
    }

	private void Init(Mesh mesh, ActiveColor activeColor, bool isSolid)
	{
        AssignedActiveColor = activeColor;
        Mesh = mesh;
        IsSolid = isSolid;
	}

	private Material SelectMaterial(TogglableObjectSO materialReference)
	{
		Material newMaterial;
		if (IsSolid)
		{
			if (IsFloor)
			{
				switch (AssignedActiveColor)
				{
					case ActiveColor.RED:
						newMaterial = materialReference.SolidFloorRedMaterial;
					break;
					case ActiveColor.BLUE:
						newMaterial = materialReference.SolidFloorBlueMaterial;
						break;
					default:
						throw new Exception("Out of range");
				}
			}
			else
			{
				switch (AssignedActiveColor)
				{
					case ActiveColor.RED:
						newMaterial = materialReference.SolidWallRedMaterial;
						break;
					case ActiveColor.BLUE:
						newMaterial = materialReference.SolidWallBlueMaterial;
						break;
					default:
						throw new Exception("Out of range");
				}
			}
			
		} else
		{
			if (IsFloor)
			{
				switch (AssignedActiveColor)
				{
					case ActiveColor.RED:
						newMaterial = materialReference.NonSolidFloorRedMaterial;
						break;
					case ActiveColor.BLUE:
						newMaterial = materialReference.NonSolidFloorBlueMaterial;
						break;
					default:
						throw new Exception("Out of range");
				}
			}
			else
			{
				switch (AssignedActiveColor)
				{
					case ActiveColor.RED:
						newMaterial = materialReference.NonSolidWallRedMaterial;
						break;
					case ActiveColor.BLUE:
						newMaterial = materialReference.NonSolidWallBlueMaterial;
						break;
					default:
						throw new Exception("Out of range");
				}
			}
		}
		return newMaterial;
	}

    public void ToggleSolidState(ActiveColor currentActiveColor, TogglableObjectSO materialReference)
	{
        if(currentActiveColor == AssignedActiveColor)
		{
            IsSolid = true;
			collider.enabled = true;
			meshRenderer.enabled = true;
			
			if (IsFloor)
			{
                this.gameObject.layer = (int)TogglableObjectState.SolidFloor;

			} else
			{
                this.gameObject.layer = (int)TogglableObjectState.SolidWall;
			}
		} else
		{
            IsSolid = false;
			collider.enabled = false;
			meshRenderer.enabled = false;
			if (IsFloor)
			{
                this.gameObject.layer = (int)TogglableObjectState.NonSolidFloor;
			} else
			{
                this.gameObject.layer = (int)TogglableObjectState.NonSolidWall;
			}
		}
		meshRenderer.material = SelectMaterial(materialReference);
	}

    private void AssignSelfToManager()
	{
        LevelManager.I.AssignToList(this);
    }
}
