using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

namespace Unity.FPS.Game
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
		[SerializeField] private ActiveColor CurrentActiveColor;
		[SerializeField] private List<TogglableObject> TogglableObjectList;
		public TogglableObjectSO togglableObjectSO;
		private void Awake()
		{
			EventManager.AddListener<ColorSwitchEvent>(OnColorSwitchEvent);
			SceneManager.sceneUnloaded += OnNewSceneUnloaded();
			SceneManager.sceneLoaded += OnNewSceneLoaded();
		}

		private void Start()
		{
			InitializeLevelState();
			GetAllTogglableObjects();
		}

		private UnityAction<Scene> OnNewSceneUnloaded()
		{
			ClearTogglableObjectList();
			return null;
		}
		private UnityAction<Scene, LoadSceneMode> OnNewSceneLoaded()
		{
			InitializeLevelState();
			return null;
		}
		private void InitializeLevelState()
		{
			SetActiveColor(_initialActiveColor);
			TriggerColorSwitchEvent();
		}

		private void GetAllTogglableObjects()
		{
			if(TogglableObjectList.Count <= 0)
			{
				Debug.Log("List was empty");
				TogglableObject[] togglableObjects = FindObjectsOfType<TogglableObject>();
				TogglableObjectList = togglableObjects.ToList();
			}
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
			if(TogglableObjectList.Count > 0)
			{
				TogglableObjectList.ForEach(obj =>
				{
					obj?.ToggleSolidState(CurrentActiveColor, togglableObjectSO);
				});
			}
		}

		private void TriggerColorSwitchEvent()
		{
			ColorSwitchEvent evt = Events.ColorSwitchEvent;
			EventManager.Broadcast(evt);
		}
		private void OnGameEndEvent(GameOverEvent evt)
		{
			ClearTogglableObjectList();
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

		void OnDestroy()
		{
			EventManager.RemoveListener<ColorSwitchEvent>(OnColorSwitchEvent);
			SceneManager.sceneUnloaded -= OnNewSceneUnloaded();
			SceneManager.sceneLoaded -= OnNewSceneLoaded();
		}
	}
}
