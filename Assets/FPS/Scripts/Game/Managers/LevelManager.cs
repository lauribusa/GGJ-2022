using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using System;

namespace Assets.FPS.Scripts.Game.Managers
{
	public enum TogglableObjectState { 
		SolidWall = 12,
		NonSolidWall = 13,
		SolidFloor = 14,
		NonSolidFloor = 15
	}
	public class LevelManager : MonoBehaviour
	{
		public ActiveColor _initialActiveColor;
		private ActiveColor CurrentActiveColor;
		[SerializeField] private List<TogglableObject> TogglableObjectList;
		public TogglableObjectSO togglableObjectSO;
		private void Awake()
		{
			SetActiveColor(_initialActiveColor);
			EventManager.AddListener<ColorSwitchEvent>(OnColorSwitchEvent);
		}

		private void SetActiveColor(ActiveColor activeColor)
		{
			CurrentActiveColor = activeColor;
		}

		public void AssignToList(TogglableObject togglableObject)
		{
			TogglableObjectList.Add(togglableObject);
		}

		public void RemoveFromList(TogglableObject togglableObject)
		{
			TogglableObjectList.Remove(togglableObject);
		}

		public void ClearTogglableObjectList()
		{
			TogglableObjectList.Clear();
		}

		private void ToggleAllTogglableObjects()
		{
			TogglableObjectList.ForEach(obj =>
			{
				obj.ToggleSolidState(CurrentActiveColor);
			});
		}

		private void TriggerColorSwitchEvent()
		{
			ColorSwitchEvent evt = Events.ColorSwitchEvent;
			EventManager.Broadcast(evt);
		}

		private void OnColorSwitchEvent(ColorSwitchEvent evt)
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
			ToggleAllTogglableObjects();
		}

		#region Singleton
		private static LevelManager _I;
		public static LevelManager I => _I;
		public LevelManager()
		{
			_I = this;
		}
		#endregion
	}
}
