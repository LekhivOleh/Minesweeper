using System.Text.Json;

namespace Minesweeper.Extensions;

public static class SessionExtensions
{
    // TODO: integrate username in session key when multiplayer
    public static void SetObject<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null)
        {
            return default;
        }
        return JsonSerializer.Deserialize<T>(value);
    }
}