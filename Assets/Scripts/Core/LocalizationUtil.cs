using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalizationUtil
{
    private static Dictionary<string, string> localizedId;

    private const string DEFAULT_LOCALIZATION = "en";
    private static string currentLocalization = DEFAULT_LOCALIZATION;
    private static bool isInitialized = false;

    public static string IdToLocalized(string id)
    {
        try
        {
            return localizedId[id];
        }
        catch
        {
            return id;
        }
    }

    public static void Init()
    {
        localizedId = new Dictionary<string, string>();

        var dbContents = Resources.Load<TextAsset>($"LocalizationDB/{currentLocalization}").text;
        var dbLines = dbContents.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        foreach(var dbLine in dbLines)
        {
            if (string.IsNullOrEmpty(dbLine))
                continue;

            if (!dbLine.Contains("\t"))
            {
                Debug.LogWarning($"Localization: broken {dbLine} line");
                continue;
            }

            var dbColumns = dbLine.Split('\t');
            var id = dbColumns[0];
            var localized = dbColumns[1];

            localizedId.Add(id, localized);
        }

        isInitialized = true;
    }
}
