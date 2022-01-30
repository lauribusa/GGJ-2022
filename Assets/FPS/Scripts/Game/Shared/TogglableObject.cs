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
	[SerializeField]
    private BoxCollider boxCollider;
	public MeshRenderer meshRenderer;
	public MeshFilter meshFilter;
	void Awake()
    {
		Init();
        AssignSelfToManager();
    }

	private void Init()
	{
		if(boxCollider == null || meshRenderer == null || meshFilter == null)
		{
			boxCollider = GetComponentInChildren<BoxCollider>();
			meshRenderer = GetComponentInChildren<MeshRenderer>();
			meshFilter = GetComponentInChildren<MeshFilter>();
		}
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

	private Mesh SelectMesh(TogglableObjectSO meshReference)
	{
		Mesh newMesh;
		if (IsSolid)
		{
			if (IsFloor)
			{
				newMesh = meshReference.SolidFloorMesh;
			} else
			{
				newMesh = meshReference.SolidWallMesh;
			}
		}
		else
		{
			if (IsFloor)
			{
				newMesh = meshReference.NonSolidFloorMesh;
			}
			else
			{
				newMesh = meshReference.NonSolidWallMesh;
			}
		}

		return newMesh;
	}
    public void ToggleSolidState(ActiveColor currentActiveColor, TogglableObjectSO materialReference)
	{
		if(boxCollider == null || meshRenderer == null || meshFilter == null)
		{
			Init();
		}
        if(currentActiveColor == AssignedActiveColor)
		{
			IsSolid = true;
			boxCollider.enabled = true;
			
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
			boxCollider.enabled = false;

			if (IsFloor)
			{
                gameObject.layer = (int)TogglableObjectState.NonSolidFloor;
			} else
			{
                gameObject.layer = (int)TogglableObjectState.NonSolidWall;
			}
		}
		meshRenderer.material = SelectMaterial(materialReference);
		meshFilter.mesh = SelectMesh(materialReference);
	}

    private void AssignSelfToManager()
	{
		FindObjectOfType<LevelManager>().AssignToList(this);
    }
}
