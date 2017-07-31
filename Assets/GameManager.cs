using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance
    {
        get
        {
            return m_Instance ?? (m_Instance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager);
        }
    }
    private static GameManager m_Instance;

    //0 = mountain, 1 = jungle, 2 = cavern, 3 = beach;
    public int biome = 0;
    public GameObject EggPrefab;
    public Canvas MainCanvas;
    public Text Player1Text;
    public Text Player2Text;
    public Text Player3Text;
    public Text Player4Text;

    private List<EggController> m_EggControllers = new List<EggController>();
    private List<RunnerController> m_RunnerControllers = new List<RunnerController>();

    public void AddRunner(RunnerController runner)
    {
        m_RunnerControllers.Add(runner);
    }

    public void RemoveRunner(RunnerController runner)
    {
        m_RunnerControllers.Remove(runner);
    }

    public void AddEgg(EggController egg)
    {
        m_EggControllers.Add(egg);
    }

    public void RemoveEgg(EggController egg)
    {
        if (!m_EggControllers.Remove(egg)) return;

        float zPosition = egg.transform.position.z;
        if (m_EggControllers.Count == 0)
        {
            GameObject eggGo = Instantiate(EggPrefab);
            Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, zPosition);
            eggGo.transform.position = spawnPosition;

            eggGo.GetComponent<EggController>().IsTaken = false;
            eggGo.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            eggGo.GetComponent<Rigidbody>().useGravity = true;

            AddEgg(eggGo.GetComponent<EggController>());
        }
    }

	string[] ambianceNames;
	string[] musiqueNames;

	public bool cavernLight;
	public Light dirLight;
    public bool levelDone = false;

	void Start() {
		musiqueNames = new string[4];
		musiqueNames[0] = "MUS_Level_Play";
		musiqueNames[1] = "MUS_Level_Jungle";
		musiqueNames[2] = "MUS_Level_Grotto";
		musiqueNames[3] = "MUS_Level_Jungle";

		ambianceNames = new string[4];
		ambianceNames[0] = "Ambiance_start";
		ambianceNames[1] = "Mountain_Jungle";
		ambianceNames[2] = "Jungle_Cave";
		ambianceNames[3] = "Cave_Beach";
		SwitchBiome();

	}

    void Update()
    {
        if (!levelDone) return;
        if (GamepadManager.Instance.GetButtonDownAny("Start"))
        {
            SceneManager.LoadScene("SplashScreen", LoadSceneMode.Single);
            AkSoundEngine.PostEvent("Sounds_Off", gameObject);
        }
    }

    public void SwitchBiome() {
		AkSoundEngine.PostEvent(musiqueNames[biome], gameObject);
		AkSoundEngine.PostEvent(ambianceNames[biome], gameObject);
		biome++;
	}

	public IEnumerator ChangeLight () {
		yield return new WaitForEndOfFrame();
        if (cavernLight == true)
        {
            dirLight.DOIntensity(0, 0.50f);
            RenderSettings.ambientIntensity = 0.1f;
            RenderSettings.reflectionIntensity = 0.1f;
        }
        else
        {
            dirLight.DOIntensity(1, 0.50f);
            RenderSettings.ambientIntensity = 1f;
            RenderSettings.reflectionIntensity = 1f;
        }
    }

    public void VictoryScreen()
    {
        MainCanvas.gameObject.SetActive(true);
        foreach (RunnerController runner in m_RunnerControllers)
        {
            runner.m_ControllerLock = true;
            if (runner.m_PlayerIndex == 1)
            {
                Player1Text.gameObject.SetActive(true);
                string status = runner.characterState == RunnerController.CharacterState.withEgg ? "Passed !" : "Failed !";
                Player1Text.text = "Player 1 : " + status;
            }

            if (runner.m_PlayerIndex == 2)
            {
                Player2Text.gameObject.SetActive(true);
                string status = runner.characterState == RunnerController.CharacterState.withEgg ? "Passed !" : "Failed !";
                Player2Text.text = "Player 2 : " + status;
            }

            if (runner.m_PlayerIndex == 3)
            {
                Player3Text.gameObject.SetActive(true);
                string status = runner.characterState == RunnerController.CharacterState.withEgg ? "Passed !" : "Failed !";
                Player3Text.text = "Player 3 : " + status;
            }

            if (runner.m_PlayerIndex == 4)
            {
                Player4Text.gameObject.SetActive(true);
                string status = runner.characterState == RunnerController.CharacterState.withEgg ? "Passed !" : "Failed !";
                Player4Text.text = "Player 4 : " + status;
            }
        }

        levelDone = true;
    }

}
