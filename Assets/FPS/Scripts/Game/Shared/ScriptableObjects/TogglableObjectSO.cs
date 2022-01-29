using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TogglableObjectAsset")]
public class TogglableObjectSO : ScriptableObject
{
	[Header("Solid Floor")]
	public Mesh SolidFloorMesh;
	public Material SolidFloorMaterial;
	[Header("Non-Solid Floor")]
	public Mesh NonSolidFloorMesh;
	public Material NonSolidFloorMaterial;
	[Header("Solid Wall")]
	public Mesh SolidWallMesh;
	public Material SolidWallMaterial;
	[Header("Non-Solid Wall")]
	public Mesh NonSolidWallMesh;
	public Material NonSolidWallMaterial;
}
