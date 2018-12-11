using UnityEngine;
using System.Collections.Generic;
using Soomla.Store;



    public class IAPManager : MonoBehaviour
    {


        private static IAPManager _instance;
        public static IAPManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<IAPManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        public static Soomla.Store.LifetimeVG NoAds = null;

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
        }

        void Start()
        {
            StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;
            StoreEvents.OnItemPurchased += OnItemPurchased;
            StoreEvents.OnRestoreTransactionsFinished += OnRestoreTransactionsFinished;
            SoomlaStore.Initialize(new Soomla.Store.SoolmaAssets());

            
    }

        public void OnItemPurchased(PurchasableVirtualItem pvi, string payload)
        {
            Debug.Log("OnItemPurchased ran with itemID: "+pvi.ID);
            switch (pvi.ID)
            {
                case SoolmaAssets.NO_ADS_ID:
                    Debug.Log("No Ads Purchased!!");
                    AdManager.instance.DisableAds();

                break;
            }
            
        }
        
        public void onSoomlaStoreInitialized()
        {
       
            NoAds = Soomla.Store.SoolmaAssets.NO_ADS_LIFETIME_VG;
            
        }

        public void OnRestoreTransactionsFinished(bool sucess) 
        {
            if (VirtualGoodsStorage.GetBalance(NoAds) == 1)
            {

                AdManager.instance.DisableAds();
                PlayerPrefs.SetInt("paid", 1);
                PlayerPrefs.Save();
            }


        }

        public void BuyNoAds()
        {
            LifetimeVG no_ads = NoAds;

            try
            {
                StoreInventory.BuyItem(no_ads.ItemId);
            } catch (System.Exception e)
            {
                Debug.Log("Soomla/Unity" + e.Message);
            }
        }
    }
