namespace Reign.Generics
{
    public class SaveFileHandler
    {
        private bool encryption => GameCertificates.ENCRYPT;
        private string saveFileDirectory => GameCertificates.SAVE_FILE_DIRECTORY;

    }
}