////////////////////////////////////////////////////////////
// File: Configurator.cs
// Author: Morgan Henry James
// Date Created: 07-01-2020
// Brief: Allows for the creation of a configuration profile.
// This allows the user to interact more easily with the game.
//////////////////////////////////////////////////////////// 

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows for the creation of a configuration profile.
/// This allows the user to interact more easily with the game.
/// </summary>
public class Configurator : MonoBehaviour
{
	#region Variables
	#region Private
	/// <summary>
	/// The script that gets the emotion data within the scene.
	/// </summary>
	[Tooltip("The script that gets the emotion data within the scene.")]
	[SerializeField] private GetEmotionData getEmotionData;

	/// <summary>
	/// The objects that start on the screen within the configuration scene.
	/// </summary>
	[Tooltip("The objects that start on the screen within the configuration scene.")]
	[SerializeField] private GameObject startingObjects;

	/// <summary>
	/// The objects that are on the screen during the configuration.
	/// </summary>
	[Tooltip("The objects that are on the screen during the configuration.")]
	[SerializeField] private GameObject configuringObjects;

	/// <summary>
	/// The objects that end on the screen within the configuration scene.
	/// </summary>
	[Tooltip("The objects that end on the screen within the configuration scene.")]
	[SerializeField] private GameObject endObjects;

	/// <summary>
	/// The picture frames animator.
	/// </summary>
	[Tooltip("The picture frames animator.")]
	[SerializeField] private Animator frameAnimator;

	/// <summary>
	/// The text mesh pro object that indicates to the emotion to depict.
	/// </summary>
	private TextMeshProUGUI emotionToDepictText;

	/// <summary>
	/// The text mesh pro object that indicates to the remaining time.
	/// </summary>
	private TextMeshProUGUI timeText;
	#endregion
	#region Public

	#endregion
	#endregion

	#region Methods
	#region Private
	/// <summary>
	/// Sets references and adds button events.
	/// </summary>
	private void Start()
	{
		// Set the emotion to depict text mesh pro UGUI reference.
		emotionToDepictText = configuringObjects.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

		// Set the time left text mesh pro UGUI reference.
		timeText = configuringObjects.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

		// Adds the start event to the start button of the configuration.
		startingObjects.transform.GetChild(1).gameObject.GetComponent<ButtonWithPositionChange>().onClick.AddListener(() => StartConfigurating());

		// Adds the end event to the end button of the configuration.
		endObjects.transform.GetChild(1).gameObject.GetComponent<ButtonWithPositionChange>().onClick.AddListener(() => 
		{
			StartCoroutine(CloseConfigurator());
		});

		// Allows the get emotion data script to launch up and for the user to read the info on the screen.
		Invoke("TurnOnStartButton", 5f);
	}

	/// <summary>
	/// Closes the configuration after an animation.
	/// </summary>
	/// <returns></returns>
	private IEnumerator CloseConfigurator()
	{
		frameAnimator.Play("ConfiurationFrameOutro");
		getEmotionData.CloseProcess();
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}

	/// <summary>
	/// Turns on the play button.
	/// </summary>
	private void TurnOnStartButton()
	{
		startingObjects.transform.GetChild(1).gameObject.SetActive(true);
	}

	/// <summary>
	/// Starts up the configuration.
	/// </summary>
	private void StartConfigurating()
	{
		startingObjects.gameObject.SetActive(false);
		configuringObjects.gameObject.SetActive(true);
		StartCoroutine(Configure());
	}

	/// <summary>
	/// Changes the screen to indicate what is going on and records the users emotional profile.
	/// </summary>
	/// <returns></returns>
	private IEnumerator Configure()
	{
		#region Neutral
		emotionToDepictText.text = "Neutral";
		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalNeutralnessPercentage = 0.0f;
		int profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalNeutralnessPercentage += getEmotionData.currentEmotionProfile.neutral;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Neutral", totalNeutralnessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Neutral", 0f);
		}
		#endregion

		#region Happy
		configuringObjects.transform.GetChild(0).gameObject.SetActive(true);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(false);

		emotionToDepictText.text = "Happy";

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);

		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalHappynessPercentage = 0.0f;
		profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalHappynessPercentage += getEmotionData.currentEmotionProfile.happy;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Happy", totalHappynessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Happy", 0f);
		}
		#endregion

		#region Surprised
		configuringObjects.transform.GetChild(0).gameObject.SetActive(true);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(false);

		emotionToDepictText.text = "Surprised";

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);

		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalSurprisednessPercentage = 0.0f;
		profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSurprisednessPercentage += getEmotionData.currentEmotionProfile.surprised;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Surprised", totalSurprisednessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Surprised", 0f);
		}
		#endregion

		#region Sad
		configuringObjects.transform.GetChild(0).gameObject.SetActive(true);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(false);

		emotionToDepictText.text = "Sad";

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);

		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalSadnessPercentage = 0.0f;
		profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalSadnessPercentage += getEmotionData.currentEmotionProfile.sad;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Sad", totalSadnessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Sad", 0f);
		}
		#endregion

		#region Scared
		configuringObjects.transform.GetChild(0).gameObject.SetActive(true);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(false);

		emotionToDepictText.text = "Scared";

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);

		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalScarednessPercentage = 0.0f;
		profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalScarednessPercentage += getEmotionData.currentEmotionProfile.scared;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Scared", totalScarednessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Scared", 0f);
		}
		#endregion

		#region Disgust
		configuringObjects.transform.GetChild(0).gameObject.SetActive(true);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(false);

		emotionToDepictText.text = "Disgust";

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);

		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalDisgustnessPercentage = 0.0f;
		profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalDisgustnessPercentage += getEmotionData.currentEmotionProfile.disgust;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Disgust", totalDisgustnessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Disgust", 0f);
		}
		#endregion

		#region Angry
		configuringObjects.transform.GetChild(0).gameObject.SetActive(true);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(false);

		emotionToDepictText.text = "Angry";

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);

		timeText.text = "3";
		yield return new WaitForSeconds(1f);
		timeText.text = "2";
		yield return new WaitForSeconds(1f);
		timeText.text = "1";
		yield return new WaitForSeconds(1f);
		timeText.text = "0";

		configuringObjects.transform.GetChild(0).gameObject.SetActive(false);
		configuringObjects.transform.GetChild(1).gameObject.SetActive(true);

		timeText.text = "5";

		// Play the hold noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Hold);

		float totalAngrynessPercentage = 0.0f;
		profileInstancesAdded = 0;

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		timeText.text = "4";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		timeText.text = "3";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		timeText.text = "2";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		timeText.text = "1";

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		yield return new WaitForSeconds(0.25f);

		if (getEmotionData.currentEmotionProfile.isCurrent)
		{
			totalAngrynessPercentage += getEmotionData.currentEmotionProfile.angry;
			profileInstancesAdded++;
		}

		timeText.text = "0";

		if (profileInstancesAdded != 0)
		{
			PlayerPrefs.SetFloat("Angry", totalAngrynessPercentage / profileInstancesAdded);
		}
		else
		{
			PlayerPrefs.SetFloat("Angry", 0f);
		}

		// Play the relax noise.
		AudioManager.instance.PlayOneShot((int)AudioManager.SFXClips.Relax);
		#endregion

		configuringObjects.SetActive(false);
		endObjects.SetActive(true);
	}
	#endregion
	#region Public

	#endregion
	#endregion
}