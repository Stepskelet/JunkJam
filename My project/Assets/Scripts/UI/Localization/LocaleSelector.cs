using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool localeChanging = false;
    public static int localeID = 0; // english

    private void Awake()
    {
        StartCoroutine(SetLocale(localeID));
    }

    public void ChangeLocale()
    {
        localeID = (localeID + 1) % LocalizationSettings.AvailableLocales.Locales.Count;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int alocaleID)
    {
        localeChanging = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[alocaleID];
        localeChanging = false;
    }
}
