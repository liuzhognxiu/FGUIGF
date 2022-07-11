namespace MetaArea
{
    interface ILocalizationComponent
    {
        string LocalizationKey { get; }

        void Localization();
    }
    
}