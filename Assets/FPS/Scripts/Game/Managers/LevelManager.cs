using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using System;

namespace Assets.FPS.Scripts.Game.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public ActiveColor _initialActiveColor;
		private ActiveColor CurrentActiveColor;
		private List<TogglableObject> TogglableObjects;
		private void Awake()
		{
			SetActiveColor(_initialActiveColor);
		}

		private void SetActiveColor(ActiveColor activeColor)
		{
			CurrentActiveColor = activeColor;
		}

		private void ToggleAllColors()
		{
			TogglableObjects.ForEach(obj =>
			{
				obj.IsSolid = CurrentActiveColor == obj.AssignedActiveColor;
				obj.gameObject.GetComponent<MeshFilter>().mesh = new Mesh();
			});
		}

		private void ToggleActiveColor()
		{
			switch (CurrentActiveColor)
			{
				case ActiveColor.RED:
					SetActiveColor(ActiveColor.BLUE);
					break;
				case ActiveColor.BLUE:
					SetActiveColor(ActiveColor.RED);
					break;
				default:
					throw new Exception("Out of range");
			}
		}
	}
}
