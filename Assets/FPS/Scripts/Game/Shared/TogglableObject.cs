using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using System;

public class TogglableObject : MonoBehaviour
{
    public bool IsSolid;
    public bool IsFloor;
    public ActiveColor AssignedActiveColor;
    private Mesh Mesh;
	[SerializeField]
    private Collider meshCollider;
	private MeshRenderer meshRenderer;
	void Awake()
    {
		Init();
        AssignSelfToManager();
    }

	private void Init()
	{
		meshCollider = GetComponentInChildren<MeshCollider>();
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	private void Start()
	{
		AssignSelfToManager();
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
		if(meshCollider == null || meshRenderer == null)
		{
			Init();
		}
        if(currentActiveColor == AssignedActiveColor)
		{
			IsSolid = true;
			meshCollider.enabled = true;
			//meshRenderer.enabled = true;
			
			if (IsFloor)
			{
                gameObject.layer = (int)TogglableObjectState.SolidFloor;

			} else
			{
                gameObject.layer = (int)TogglableObjectState.SolidWall;
			}
		} else
		{
            IsSolid = false;
			meshCollider.enabled = false;
			//meshRenderer.enabled = false;
			if (IsFloor)
			{
                gameObject.layer = (int)TogglableObjectState.NonSolidFloor;
			} else
			{
                gameObject.layer = (int)TogglableObjectState.NonSolidWall;
			}
		}
		meshRenderer.material = SelectMaterial(materialReference);
	}

    private void AssignSelfToManager()
	{

		LevelManager.instance.AssignToList(this);
    }
}
