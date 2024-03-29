﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HeurekaGames.AssetHunterPRO
{
	public class AH_SceneReferenceWindow : EditorWindow
	{
		private static AH_SceneReferenceWindow m_window;
		private Vector2 scrollPos;

		[SerializeField]
		private float btnMinWidthSmall = 50;

		private List<String> m_allScenesInProject;
		private List<String> m_allScenesInBuildSettings;
		private List<String> m_allEnabledScenesInBuildSettings;
		private List<String> m_allUnreferencedScenes;
		private List<String> m_allDisabledScenesInBuildSettings;

		private static readonly string WINDOWNAME = "AH Scenes";

		[MenuItem("Window/Heureka/Asset Hunter PRO/Open scene overview")]
		public static void Init()
		{
			m_window = AH_SceneReferenceWindow.GetWindow<AH_SceneReferenceWindow>(WINDOWNAME, true, typeof(AH_Window));
			m_window.titleContent.image = AH_EditorData.Instance.SceneIcon.Icon;
			m_window.GetSceneInfo();
		}

		private void GetSceneInfo()
		{
			m_allScenesInProject = AH_Utils.GetAllSceneNames().ToList<string>();
			m_allScenesInBuildSettings = AH_Utils.GetAllSceneNamesInBuild().ToList<string>();
			m_allEnabledScenesInBuildSettings = AH_Utils.GetEnabledSceneNamesInBuild().ToList<string>();
			m_allDisabledScenesInBuildSettings = SubtractSceneArrays(m_allScenesInBuildSettings, m_allEnabledScenesInBuildSettings);
			m_allUnreferencedScenes = SubtractSceneArrays(m_allScenesInProject, m_allScenesInBuildSettings);
		}

		//Get the subset of scenes where we subtract "secondary" from "main"
		private List<String> SubtractSceneArrays(List<String> main, List<String> secondary)
		{
			return main.Except<string>(secondary).ToList<string>();
		}

		private void OnFocus()
		{
			GetSceneInfo();
		}

		private void OnGUI()
		{
			if (!m_window)
				Init();

			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
			AH_WindowStyler.DrawGlobalHeader(m_window, AH_WindowStyler.clr_Dark, "SCENE REFERENCES", false);

			//Show all used types
			EditorGUILayout.BeginVertical();

			//Make sure this window has focus to update contents
			Repaint();

			if (m_allEnabledScenesInBuildSettings.Count == 0)
				AH_WindowStyler.DrawCenteredMessage(m_window, 310f, 110f, "There are no enabled scenes in build settings");

			drawScenes("These scenes are added and enabled in build settings", m_allEnabledScenesInBuildSettings);
			drawScenes("These scenes are added to build settings but disabled", m_allDisabledScenesInBuildSettings);
			drawScenes("These scenes are not referenced anywhere in build settings", m_allUnreferencedScenes);

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndScrollView();
		}

		private void drawScenes(string headerMsg, List<string> scenes)
		{
			if (scenes.Count > 0)
			{
				EditorGUILayout.HelpBox(headerMsg, MessageType.Info);
				foreach (string scenePath in scenes)
				{
					EditorGUILayout.BeginHorizontal();
					if (GUILayout.Button("Ping", GUILayout.Width(btnMinWidthSmall)))
					{
						Selection.activeObject = AssetDatabase.LoadAssetAtPath(scenePath, typeof(UnityEngine.Object));
						EditorGUIUtility.PingObject(Selection.activeObject);
					}
					EditorGUILayout.LabelField(scenePath);
					EditorGUILayout.EndHorizontal();
				}
				EditorGUILayout.Separator();
			}
		}
	}
}