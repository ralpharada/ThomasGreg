namespace ThomasGreg.Application.Services
{
    public static class DeleteFileService
    {
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

}
