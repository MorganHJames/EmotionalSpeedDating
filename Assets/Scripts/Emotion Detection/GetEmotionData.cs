////////////////////////////////////////////////////////////
// File: GetEmotionData.cs
// Author: Morgan Henry James
// Date Created: 31-10-2019
// Brief: Gets and holds a structure pertaining to the users emotional state.
//////////////////////////////////////////////////////////// 

using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Text;

/// <summary>
/// Gets and holds a structure pertaining to the users emotional state.
/// </summary>
public class GetEmotionData : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The process of the python executable.
	/// </summary>
	private Process p = new Process();

	/// <summary>
	/// The previous emotion data to compare the current data to.
	/// </summary>
	private string previousEmotionData;
	#endregion
	#region Public
	/// <summary>
	/// The users current emotion profile.
	/// </summary>
	[HideInInspector] public EmotionProfile currentEmotionProfile;
	#endregion
	#endregion

	#region Structures
	#region Private

	#endregion
	#region Public
	/// <summary>
	/// An emotional profile indicating the percentage of each emotion represented in the users face.
	/// Also indicates if the data is current or not.
	/// </summary>
	public struct EmotionProfile
	{
		public bool isCurrent;
		public float neutral;
		public float surprised;
		public float sad;
		public float happy;
		public float scared;
		public float disgust;
		public float angry;
	}
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

			// Compares the previous emotion data to the current to indicate if the current profile is current or not.
			currentEmotionProfile.isCurrent = currentEmotionData != previousEmotionData;

			// Sets the previous emotional data to the current data.
			previousEmotionData = currentEmotionData;

			// Removes the percentages in the text.
			currentEmotionData = currentEmotionData.Replace("%", "");

			// Split the text by spaces into a string array.
			string[] Words = currentEmotionData.Split(' ');

			// If there is the correct amount of words.
			if (Words.Length == 15)
			{
				// Set each emotion in the profile to their respective value.
				currentEmotionProfile.angry = float.Parse(Words[2]);
				currentEmotionProfile.disgust = float.Parse(Words[4]);
				currentEmotionProfile.scared = float.Parse(Words[6]);
				currentEmotionProfile.happy = float.Parse(Words[8]);
				currentEmotionProfile.sad = float.Parse(Words[10]);
				currentEmotionProfile.surprised = float.Parse(Words[12]);
				currentEmotionProfile.neutral = float.Parse(Words[14]);
			}
			// If the data has too many words.
			else
			{
				// Set the profile to non current as to not use it.
				currentEmotionProfile.isCurrent = false;
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
	/// Closes the process.
	/// </summary>
	public void CloseProcess()
	{
		p.CloseMainWindow();
	}
	#endregion
	#endregion
}