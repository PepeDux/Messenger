using Microsoft.Win32;

namespace Messenger
{
    public class AuthService
    {
        private const string RegistryKeyPath = @"Software\Messenger";
        private const string TokenKey = "AuthToken";

        public void SaveToken(string token)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
            key.SetValue(TokenKey, token);
            key.Close();
        }

        public string LoadToken()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            if (key != null)
            {
                var token = key.GetValue(TokenKey)?.ToString();
                key.Close();
                return token;
            }
            return null;
        }

        public void ClearToken()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
            key.DeleteValue(TokenKey, false);
            key.Close();
        }
    }
}
