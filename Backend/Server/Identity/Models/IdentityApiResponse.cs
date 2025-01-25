namespace server.Models;

public class IdentityApiResponse
{
    /// <summary>
    /// Success
    /// </summary>
    public bool success { get; set; }

    /// <summary>
    /// Message ID (use SetMessage instead of directly assigning)
    /// </summary>
    public string messageId { get; set; }

    /// <summary>
    /// Error message (use SetMessage instead of directly assigning)
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// Error list
    /// </summary>
    public List<IndentityDetailError> detailErrorList { get; set; }

    /// <summary>
    /// Response
    /// </summary>
    public string response { get; set; }

}

/// <summary>
/// エラー詳細
/// </summary>
public class IndentityDetailError
{
    /// <summary>
    /// Field names with errors will be automatically converted to lowercase
    /// </summary>
    public string field { get; set; }

    /// <summary>
    /// Line number (for lists only)
    /// </summary>
    public string columnName { get; set; }

    /// <summary>
    /// Message ID (use SetMessage instead of directly assigning)
    /// </summary>
    public string messageId { get; set; }

    /// <summary>
    /// Error message (use SetMessage instead of directly assigning)
    /// </summary>
    public string errorMessage { get; set; }
}
