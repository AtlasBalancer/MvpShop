using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace com.ab.mvpshop.core.localization
{
    public class LocalizationService : ILocalizationService
    {
        public const string TABLE_REFERENCE = "Ui";
        public const string defaultLocaleCode = "en";

        public LocalizationService() =>
            InitLocales();

        void InitLocales()
        {
            var intLocaleOp = LocalizationSettings.InitializationOperation;
            intLocaleOp.WaitForCompletion();
            SetLocale(defaultLocaleCode);
        }

        public string GetString(string key) =>
            LocalizationSettings.StringDatabase.GetLocalizedString(TABLE_REFERENCE, key);

        void SetLocale(string locale)
        {
            var selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale)
                                 ?? LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier(locale));

            if (selectedLocale != null)
                LocalizationSettings.SelectedLocale = selectedLocale;
            else
                Debug.LogWarning($"Default locale '{defaultLocaleCode}' not found in Available Locales.");
        }
    }
}