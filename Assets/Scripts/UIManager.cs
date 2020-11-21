using UnityEngine;
using UnityEngine.UI;

sealed class UIManager : MonoBehaviour
{
    #region Scene object references

    [SerializeField] Canvas canvas = null;
	[SerializeField] GameObject optionsPanel = null;
	[SerializeField] GameObject startButton = null;
	[SerializeField] DataObject data = null;


    #endregion

    #region MonoBehaviour implementation

	private bool _isOptions = false;

	public void OnStartButtonClick(){
		data.SetGameStarted(true);
		startButton.SetActive(false);
	}

    void Start()
    {
        optionsPanel.SetActive(false);
    }

    void Update()
    {
		if(Input.GetKeyDown("escape")){
			_isOptions = !_isOptions;
			optionsPanel.SetActive(_isOptions);
		}
    }

    #endregion
}
