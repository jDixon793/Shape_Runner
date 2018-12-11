using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

    public void ShowAchievements()
    {
        GooglePlayManager.instance.ShowAchievements();
    }
    public void ShowLeaderboard()
    {
        GooglePlayManager.instance.ShowLeaderboard();
    }
    public void BuyNoAds()
    {
        IAPManager.instance.BuyNoAds();
    }
    public void ShowRatePage()
    {
        GooglePlayManager.instance.ShowRatePage();
    }

    public void SignInToggle()
    {
        GooglePlayManager.instance.SignInToggle();
    }
    public void LoadFromCloud()
    {
        GooglePlayManager.instance.LoadFromCloud();
    }

}
