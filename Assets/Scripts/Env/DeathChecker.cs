using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathChecker : GenericSingleton<DeathChecker>
{
    private Character[] _characters;
    private bool _transitioning = false;

    public void Awake()
    {
        _characters = FindObjectsOfType<Character>();

        int l = _characters.Length;
        for (int i = 0; i < l; i++)
            _characters[i].healthZero.AddListener(OnDeath);
    }

    private void OnDeath()
    {
        Reload();
    }

    private void Update()
    {
        if (
#if UNITY_EDITOR
            Input.GetKeyDown(KeyCode.R)
#else
            Input.GetKeyDown(KeyCode.Semicolon)
#endif
        )
        {
            Reload();
        }
    }

    private void Reload()
    {
        if (_transitioning)
            return;

        _transitioning = true;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}