using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TogglableObjectAsset")]
public class TogglableObjectSO : ScriptableObject
{
	[Header("Solid Floor")]
	public Mesh SolidFloorMesh;
	public Material SolidFloorBlueMaterial;
	public Material SolidFloorRedMaterial;
	[Header("Non-Solid Floor")]
	public Mesh NonSolidFloorMesh;
	public Material NonSolidFloorBlueMaterial;
	public Material NonSolidFloorRedMaterial;
	[Header("Solid Wall")]
	public Mesh SolidWallMesh;
	public Material SolidWallBlueMaterial;
	public Material SolidWallRedMaterial;
	[Header("Non-Solid Wall")]
	public Mesh NonSolidWallMesh;
	public Material NonSolidWallBlueMaterial;
	public Material NonSolidWallRedMaterial;
}
