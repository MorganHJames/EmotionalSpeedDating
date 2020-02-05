////////////////////////////////////////////////////////////
// File: GetEmotionData.cs
// Author: Morgan Henry James
// Date Created: 31-10-2019
// Brief: Changes text and cubes to show how the user is feeling.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

/// <summary>
/// Changes text and cubes to show how the user is feeling.
/// </summary>
public class EmotionDetectionDemo : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The loading screen.
	/// </summary>
	[Tooltip("The loading screen.")]
	[SerializeField] private GameObject loadingScreen;

	/// <summary>
	/// The process of the python executable.
	/// </summary>
	private Process p = new Process();

	/// <summary>
	/// All of the cubes within the scene.
	/// </summary>
	[Tooltip("All of the cubes within the scene.")]
	[SerializeField] private Transform[] allCubes;

	/// <summary>
	/// The cube pertaining to the emotion of neutral.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of neutral.")]
	[SerializeField] private Transform neutral;

	/// <summary>
	/// The cube pertaining to the emotion of surprised.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of surprised.")]
	[SerializeField] private Transform surprised;

	/// <summary>
	/// The cube pertaining to the emotion of sad.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of sad.")]
	[SerializeField] private Transform sad;

	/// <summary>
	/// The cube pertaining to the emotion of happy.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of happy.")]
	[SerializeField] private Transform happy;

	/// <summary>
	/// The cube pertaining to the emotion of scared.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of scared.")]
	[SerializeField] private Transform scared;

	/// <summary>
	/// The cube pertaining to the emotion of disgust.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of disgust.")]
	[SerializeField] private Transform disgust;

	/// <summary>
	/// The cube pertaining to the emotion of angry.
	/// </summary>
	[Tooltip("The cube pertaining to the emotion of angry.")]
	[SerializeField] private Transform angry;

	/// <summary>
	/// How much the neutral percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much the neutral percentage should be multiplied by.")]
	[SerializeField] private float neutralMultiplier = 1.0f;

	/// <summary>
	/// How much the surprised percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much the surprised percentage should be multiplied by.")]
	[SerializeField] private float surprisedMultiplier = 1.0f;

	/// <summary>
	/// How much the sad percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much the sad percentage should be multiplied by.")]
	[SerializeField] private float sadMultiplier = 1.0f;

	/// <summary>
	/// How much the happy percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much the happy percentage should be multiplied by.")]
	[SerializeField] private float happyMultiplier = 1.0f;

	/// <summary>
	/// How much the scared percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much scared neutral percentage should be multiplied by.")]
	[SerializeField] private float scaredMultiplier = 1.0f;

	/// <summary>
	/// How much the disgust percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much the disgust percentage should be multiplied by.")]
	[SerializeField] private float disgustMultiplier = 1.0f;

	/// <summary>
	/// How much the angry percentage should be multiplied by.
	/// </summary>
	[Tooltip("How much the angry percentage should be multiplied by.")]
	[SerializeField] private float angryMultiplier = 1.0f;

	/// <summary>
	/// The text to indicate the users current emotion.
	/// </summary>
	[Tooltip("The text to indicate the users current emotion.")]
	[SerializeField] private Text currentEmotion;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Starts up the emotion detection executable made in python that writes to a file named finnish.txt.
	/// </summary>
	private void Start()
	{
		p.StartInfo.FileName = Application.dataPath + "\\StreamingAssets\\EmotionDetect\\EmotionDetect.exe";
		p.Start();
		loadingScreen.SetActive(false);
	}

	/// <summary>
	/// Sets the emotion profile each frame to be in line with the finnish.txt.
	/// </summary>
	private void Update()
	{
		// Below is what the finnish.txt file should contain looks like.
		// neutral angry: 17.02 % disgust: 0.20 % scared: 16.01 % happy: 24.75 % sad: 9.21 % surprised: 2.99 % neutral: 29.81 %

		// Steams the file.
		using (var fs = new FileStream(Application.dataPath + "\\StreamingAssets\\EmotionDetect\\finnish.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))

		// Reads the file.
		using (var sr = new StreamReader(fs, Encoding.Default))
		{
			// Sets the current emotional data equal to the whole text.
			string currentEmotionData = sr.ReadToEnd();

			// Removes the percentages in the text.
			currentEmotionData = currentEmotionData.Replace("%", "");

			// Split the text by spaces into a string array.
			string[] Words = currentEmotionData.Split(' ');

			// If there is the correct amount of words.
			if (Words.Length == 15)
			{
				// Sets each cube to the correct size depending on the emotion percentage and multiplier.
				// Compares the percentage of emotion to find the largest.
				// Sets the text to the largest emotion percentage.

				Transform CurrentLargestEmotion = angry;
				angry.localScale = new Vector3(12.5f, float.Parse(Words[2]) * angryMultiplier, 12.5f);

				float DisgustAmount = float.Parse(Words[4]) * disgustMultiplier;
				if (DisgustAmount > CurrentLargestEmotion.localScale.y)
				{
					CurrentLargestEmotion = disgust;
				}

				disgust.localScale = new Vector3(12.5f, DisgustAmount, 12.5f);

				float ScaredAmount = float.Parse(Words[6]) * scaredMultiplier;
				if (ScaredAmount > CurrentLargestEmotion.localScale.y)
				{
					CurrentLargestEmotion = scared;
				}
				scared.localScale = new Vector3(12.5f, ScaredAmount, 12.5f);

				float HappyAmount = float.Parse(Words[8]) * happyMultiplier;
				if (HappyAmount > CurrentLargestEmotion.localScale.y)
				{
					CurrentLargestEmotion = happy;
				}
				happy.localScale = new Vector3(12.5f, HappyAmount, 12.5f);

				float SadAmount = float.Parse(Words[10]) * sadMultiplier;
				if (SadAmount > CurrentLargestEmotion.localScale.y)
				{
					CurrentLargestEmotion = sad;
				}
				sad.localScale = new Vector3(12.5f, SadAmount, 12.5f);

				float SurprisedAmount = float.Parse(Words[12]) * surprisedMultiplier;
				if (SurprisedAmount > CurrentLargestEmotion.localScale.y)
				{
					CurrentLargestEmotion = surprised;
				}
				surprised.localScale = new Vector3(12.5f, SurprisedAmount, 12.5f);

				float NeutralAmount = float.Parse(Words[14]) * neutralMultiplier;
				if (NeutralAmount > CurrentLargestEmotion.localScale.y)
				{
					CurrentLargestEmotion = neutral;
				}
				neutral.localScale = new Vector3(12.5f, NeutralAmount, 12.5f);

				currentEmotion.text = CurrentLargestEmotion.name;
			}

			// Decrease all cubes over time.
			foreach (Transform emotionTransform in allCubes)
			{
				emotionTransform.localScale -= new Vector3(0f, 0.01f, 0f);
				if (emotionTransform.localScale.y < 0)
				{
					emotionTransform.localScale += new Vector3(0f, 0.01f, 0f);
				}
			}
		}
	}

	/// <summary>
	/// When the executable is closed close the emotion detection process.
	/// </summary>
	private void OnApplicationQuit()
	{
		p.CloseMainWindow();
	}
	#endregion
	#region Public
	/// <summary>
	/// Goes back to the menu
	/// </summary>
	public void GoBackToMenu()
	{
		p.CloseMainWindow();
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}
}
#endregion
#endregion