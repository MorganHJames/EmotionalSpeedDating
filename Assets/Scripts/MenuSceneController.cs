////////////////////////////////////////////////////////////
// File: MenuSceneController.cs
// Author: Morgan Henry James
// Date Created: 08-01-2020
// Brief: Controls the menu scene.
//////////////////////////////////////////////////////////// 

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the menu scene.
/// </summary>
public class MenuSceneController : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The high score value text.
	/// </summary>
	[Tooltip("The high score value text.")]
	[SerializeField] private TextMeshProUGUI highScoreValue;

	/// <summary>
	/// The canvas animator.
	/// </summary>
	[Tooltip("The canvas animator.")]
	[SerializeField] private Animator canvasAnimator;

	/// <summary>
	/// The play button.
	/// </summary>
	[Tooltip("The play button.")]
	[SerializeField] private GameObject playButton;

	/// <summary>
	/// The quit button.
	/// </summary>
	[Tooltip("The quit button.")]
	[SerializeField] private ButtonWithPositionChange quitButton;

	/// <summary>
	/// The animator attached to the object.
	/// </summary>
	private Animator sceneControllerAnimator;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Turns on the play button if the game has been configured for the user.
	/// </summary>
	private void Start()
	{
		sceneControllerAnimator = GetComponent<Animator>();
		highScoreValue.text = PlayerPrefs.GetInt("HighScore").ToString();
		quitButton.onClick.AddListener(() => Application.Quit());

		// Angry is the last emotion to be set in the configuration.
		if (PlayerPrefs.HasKey("Angry"))
		{
			playButton.SetActive(true);
		}
	}

	/// <summary>
	/// Loads a scene after playing an animation.
	/// </summary>
	/// <param name="sceneToLoad">The scene to load.</param>
	/// <returns></returns>
	private IEnumerator LoadScene(string sceneToLoad)
	{
		sceneControllerAnimator.Play("MenuOut");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
	}

	/// <summary>
	/// Loads a scene after playing an animation but without a blur change.
	/// </summary>
	/// <param name="sceneToLoad">The scene to load.</param>
	/// <returns></returns>
	private IEnumerator LoadSceneNoBlur(string sceneToLoad)
	{
		sceneControllerAnimator.Play("MenuOutNoBlur");
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
	}
	#endregion
	#region Public
	/// <summary>
	/// Opens or closes the settings
	/// </summary>
	/// <param name="open">True when wanting to open and false when wanting to close.</param>
	public void OpenSettings(bool open)
	{
		if (open)
		{
			canvasAnimator.Play("SoundSettingsOpen");
		}
		else
		{
			canvasAnimator.Play("SoundSettingsClose");
		}
	}

	/// <summary>
	/// Loads the face detection demo.
	/// </summary>
	public void GoToFaceDetectionDemo()
	{
		StartCoroutine(LoadSceneNoBlur("FaceDetectionDemo"));
	}

	/// <summary>
	/// Loads the emotion detection demo.
	/// </summary>
	public void GoToEmotionDetectionDemo()
	{
		StartCoroutine(LoadSceneNoBlur("EmotionDetectionDemo"));
	}

	/// <summary>
	/// Loads the configuration scene.
	/// </summary>
	public void GoToConfiguration()
	{
		StartCoroutine(LoadSceneNoBlur("Configurator"));
	}

	/// <summary>
	/// Plays the game.
	/// </summary>
	public void PlayGame()
	{
		StartCoroutine(LoadScene("GameScene"));
	}
	#endregion
	#endregion
}