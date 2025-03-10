namespace Client.Repositories;

public interface ILogicCommonRepository
{
    string EncryptText(string text);
    
    string DecryptText(string text);

    Task<bool> ImageCheck(List<string> imageUrls);
}