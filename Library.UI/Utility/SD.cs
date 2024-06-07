namespace Library.UI.Utility;

public class SD
{
    // API Url
    public static string LibraryAPIBase { get; set; }

    // Roles
    public const string RoleAdmin = "ADMIN";

    // JWT
    public const string TokenCookie = "JwtToken";

    // Content Type
    public enum ContentType
    {
        Json,
        MultipartFormData,
    }
}
