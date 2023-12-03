using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingMenuControl : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tipText;

    [SerializeField]
    private TextMeshProUGUI _loadingText;

    [SerializeField]
    private List<string> _tips;

    private bool _playingLoadingTextAnimation = false;
    private bool _playingTipTextAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playingTipTextAnimation)
        {
            StartCoroutine(PlayTipTextAnimation());
        }

        if (!_playingLoadingTextAnimation)
        {
            StartCoroutine(PlayLoadingTextAnimation());
        }
    }

    IEnumerator PlayTipTextAnimation()
    {
        _playingTipTextAnimation = true;
        _tipText.text = _tips[Random.RandomRange(0, _tips.Count)];

        yield return new WaitForSeconds(10.0f);
        _playingTipTextAnimation = false;
    }

    IEnumerator PlayLoadingTextAnimation()
    {
        _playingLoadingTextAnimation = true;
        _loadingText.text = "Loading";

        yield return new WaitForSeconds(0.1f);
        _loadingText.text = "Loading.";

        yield return new WaitForSeconds(0.1f);
        _loadingText.text = "Loading..";

        yield return new WaitForSeconds(0.1f);
        _loadingText.text = "Loading...";

        yield return new WaitForSeconds(0.2f);
        _loadingText.text = "Loading....";

        yield return new WaitForSeconds(0.1f);
        _playingLoadingTextAnimation = false;
    }
}
