using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using Assets.FPS.Scripts.Game.Managers;

public class TogglableObject : MonoBehaviour
{
    public bool IsSolid;
    public bool IsFloor;
    public ActiveColor AssignedActiveColor;
    private Mesh Mesh;
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

    public void ToggleSolidState(ActiveColor currentActiveColor)
	{
        if(currentActiveColor == AssignedActiveColor)
		{
            IsSolid = true;
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
            if (IsFloor)
			{
                this.gameObject.layer = (int)TogglableObjectState.NonSolidFloor;
			} else
			{
                this.gameObject.layer = (int)TogglableObjectState.NonSolidWall;
			}
		}
	}

    private void AssignSelfToManager()
	{
        LevelManager.I.AssignToList(this);
    }
}
