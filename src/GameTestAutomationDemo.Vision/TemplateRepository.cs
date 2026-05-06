public static class TemplateRepository
{
    private static readonly string BasePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "templates", "Brawlhalla");

    private static readonly Dictionary<string, string> Templates = new()
    {
        // ===== MAIN MENU =====
        { "loading_screen", "loading_screen.png"},
        { "main_menu_logo", "main_menu_logo.png" },
        { "play_button", "play_button.png" },
        { "settings_button", "settings_button.png" },

        // ===== MODE SELECTION =====
        { "mode_selection_title", "mode_selection_title.png" },
        { "free_for_all_button", "free_for_all_button.png" },
        { "1v1_strikeout_button", "1v1_strikeout_button.png" },
        { "friendly_2v2_button", "friendly_2v2_button.png" },
        { "unranked_1v1_button", "unranked_1v1_button.png" },

        // ===== CHARACTER SELECTION =====
        { "character_select_title", "character_select_title.png" },
        { "teros_icon", "teros_icon.png" },
        {"random_icon", "random_icon.png"},
        { "confirm_character_button", "confirm_character_button.png" },
        { "ready_banner", "ready_banner.png"},

        // ===== MATCHMAKING =====
        { "matchmaking_label", "matchmaking_label.png" },
        { "searching_indicator", "searching_indicator.png" },

        // ===== IN MATCH (HUD) =====
        { "match_hud", "match_hud.png" },
        { "player_health_bar", "player_health_bar.png" },

        // ===== MATCH END =====
        { "results_banner", "results_banner.png" },
        { "podium_first", "podium_first.png" },
        { "podium_second", "podium_second.png" },
        { "next_button", "next_button.png" },

        // ===== NAVIGATION =====
        { "back_button", "back_button.png" }
    };

    public static string Get(string elementName)
    {
        if (!Templates.ContainsKey(elementName))
            throw new Exception($"Template '{elementName}' not registered.");

        var fullPath = Path.Combine(BasePath, Templates[elementName]);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Template file not found: {fullPath}");

        return fullPath;
    }

    public static void Register(string elementName, string fileName)
    {
        Templates[elementName] = fileName;
    }

    public static IReadOnlyDictionary<string, string> GetAll()
    {
        return Templates;
    }
}
