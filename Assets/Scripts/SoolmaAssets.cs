namespace Soomla.Store
{
    internal class SoolmaAssets : IStoreAssets
    {
        public VirtualCategory[] GetCategories()
        {
            return new VirtualCategory[] { };
        }

        public VirtualCurrency[] GetCurrencies()
        {
            return new VirtualCurrency[] { };
        }

        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return new VirtualCurrencyPack[] { };
        }

        public VirtualGood[] GetGoods()
        { //change get goods to return your goods
            return new VirtualGood[] { NO_ADS_LIFETIME_VG};
        }

        public int GetVersion()
        {
            return 7;
        }

        //Stuff we add
        public LifetimeVG[] GetLifetimeVG()
        {
            return new LifetimeVG[] { NO_ADS_LIFETIME_VG};
        }

        public const string NO_ADS_Market_ID = "no_ads"; //string ID in Play android.test.purchased
        public const string NO_ADS_ID = "no_ads"; //string ID in game

        public static LifetimeVG NO_ADS_LIFETIME_VG = new LifetimeVG(
            "No Ads",
            "Test Product: No Ads",
            NO_ADS_ID,
            new PurchaseWithMarket(new MarketItem(NO_ADS_Market_ID, 0.99)));
    }
}
