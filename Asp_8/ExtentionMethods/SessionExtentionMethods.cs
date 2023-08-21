﻿using System.Text.Json;

namespace BookStore.WebUI.ExtentionMethods;
public static class SessionExtentionMethods
{
    public static void SetObject(this ISession session, string key, object value)
    {
        string objectString = JsonSerializer.Serialize(value);
        session.SetString(key, objectString);
    }

    public static T GetObject<T>(this ISession session, string key) where T : class
    {
        string objectString = session.GetString(key)!;

        if (string.IsNullOrEmpty(objectString))
            return null!;

        T? result = JsonSerializer.Deserialize<T>(objectString);

        return result!;
    }
}
