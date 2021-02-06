using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene
    {
        MainMenu,
        TicTacKnow,
        AllGames,
        Demo,
        WordScramble,
        WordSearch,
        BigBrain,
    }

    private static Action onLoaderCallback;
    public static void Load(Scene scene)
    {
        /*onLoaderCallback = () =>
        {
            SceneManager.LoadScene(scene.ToString());

        };
        SceneManager.LoadScene(Scene.Loading.ToString());*/
        SceneManager.LoadScene(scene.ToString());
    }

    public static void PreLoad(Scene scene)   // this function is only used for preloading the scene
    {
        SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);   // we loading the scene in background
    }

    public static void Unload(Scene scene)   // this function is only used for unload  the scene
    {
        SceneManager.UnloadSceneAsync(scene.ToString());   // we unloading the scene in background
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}

