using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;



public class GooglePlayManager : MonoBehaviour
{

    private static GooglePlayManager _instance;
    public static GooglePlayManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GooglePlayManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }



    private GooglePlayGames.BasicApi.OnStateLoadedListener mAppStateListener;
    private uint numSaves = 4;
    private bool isSaving;
    public bool loggedIn = false;
    private Texture2D screenImage;

    public bool Authenticated
    {
        get
        {
            return Social.Active.localUser.authenticated;
        }
    }


    // Use this for initialization
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }


        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .EnableSavedGames()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

    }

    
    public void SignInToggle()
    {
        if (loggedIn)
        {
            PlayGamesPlatform.Instance.SignOut();
            Debug.Log("Signed Out");
            loggedIn = false;
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    loggedIn = true;
                    Debug.Log("Sign In: Success");
                }
                else
                {
                    loggedIn = false;
                    Debug.Log("Sign In: Failure");
                }
            });
        }
    }


    string MakeSaveString()//name:lives:points:highScore
    {
        string s = inputManager.userName.ToString() + ":" +
                    inputManager.lives.ToString() + ":" +
                    inputManager.points.ToString() + ":" +
                    inputManager.highScore.ToString();
        return s;
    }

    void LoadFromSaveString(string s)//name:lives:points:highScore
    {
        string[] data = s.Split(new char[] { ':' });
        inputManager.userName = data[0];
        inputManager.lives = Int32.Parse(data[0]);
        inputManager.points = Int32.Parse(data[0]);
        inputManager.highScore= Int32.Parse(data[0]); ;

    }
    byte[] ToBytes()
    {
        return System.Text.ASCIIEncoding.Default.GetBytes(MakeSaveString());
    }
    string FromBytes(byte[] b)
    {
        return System.Text.ASCIIEncoding.Default.GetString(b);
    }

    public void MultiSaveToCloud()
    {
        if (Authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI(
                "Save game progress",
                numSaves, true, true, SavedGameSelected);

        }
    }
    
    public void SaveToCloud()
    {
        if (Authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                     "saveFile",
                     DataSource.ReadCacheOrNetwork,
                     ConflictResolutionStrategy.UseOriginal,
                     SaveFileOpened);
        }
    }

    public void LoadFromCloud()
    {
        if (Authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                     "saveFile",
                     DataSource.ReadCacheOrNetwork,
                     ConflictResolutionStrategy.UseOriginal,
                     SaveFileOpened);
        }
    }
    public void MultiLoadFromCloud()
    {
        if (Authenticated)
        {
            isSaving = false;
           ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI(
                "Save game progress",
                numSaves, false, false, SavedGameSelected);

        }

    }

    public void SavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {

        Debug.Log("opening saved game:  " + game);
        if (status == SelectUIStatus.SavedGameSelected)
        {
            string filename = game.Filename;
            if (isSaving && (filename == null || filename.Length == 0))
            {
                filename = "saveFile" + DateTime.Now.ToBinary();
            }
            //open the data.
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(
                     filename,
                     DataSource.ReadCacheOrNetwork,
                     ConflictResolutionStrategy.UseLongestPlaytime,
                     SaveFileOpened);
        }
        else
        {
            Debug.LogWarning("Error selecting save game: " + status);
        }
    }

    public void SaveFileOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we have opened the file then we are either saving or loading
        if (status == SavedGameRequestStatus.Success)
        {
            if (isSaving) //writting save
            {

                if (screenImage == null)
                {
                    CaptureScreenshot();
                }

                byte[] pngData = null;
                if (screenImage != null)
                {
                    pngData = screenImage.EncodeToPNG();
                }


                byte[] data = ToBytes();
                //Can update metadata with played time description and screenshoot
                SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder().WithUpdatedDescription(
                                    inputManager.userName +" saved Game at " + DateTime.Now);
                

                if (pngData != null)
                {
                    Debug.Log("Save image of len " + pngData.Length);
                    builder = builder.WithUpdatedPngCoverImage(pngData);
                }
                else
                {
                    Debug.Log("No image avail");
                }

                SavedGameMetadataUpdate updatedMetadata = builder.Build();
                Debug.Log("Saved Game Built");
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, updatedMetadata, data, WroteSavedGame);


            }
            else // loading save
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, LoadedSavedGame);
            }

        }
        else
        {
            Debug.LogWarning("Error opening game: " + status);
        }
    }

    public void LoadedSavedGame(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("SaveGameLoaded, success=" + status);
            LoadFromSaveString(FromBytes(data));

        }
        else
        {
            Debug.LogWarning("Error reading game: " + status);
        }
    }

    public void WroteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Game " + game.Description + " written");
            GUIManager.ShowMessage("Game Saved!");
        }
        else
        {
            Debug.LogWarning("Error saving game: " + status);
            GUIManager.ShowMessage("Error Saving Game");
        }
    }

    public void CaptureScreenshot()
    {
        screenImage = new Texture2D(Screen.width, Screen.height);
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        Debug.Log("Captured screen: " + screenImage);
    }



}
