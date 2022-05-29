using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO.IsolatedStorage;
using System.IO;

using System.Security.Cryptography;

namespace NetSnifferApp
{
    public class AccountManager
    {
        const string connectionString = "Data Source=YAKOV-PC;Initial Catalog=NetSnifferDatabase;Integrated Security=True";
        SHA256 SHA256 { get;  init; }
        SqlConnection Connection { get; init; }

        const string usernameExixsts = "SELECT Username FROM Acounts WHERE Username LIKE @username";

        const string insertUser = "INSERT INTO Acounts (Username, PasswordHash) VALUES (@username, @passwordHash)";

        const string getUserPassword = "SELECT PasswordHash FROM Acounts WHERE Username LIKE @username";

        const string topLevelFolder = "PrivateCaptures";
        private AccountManager()
        {
            SHA256 = SHA256.Create();
            Connection = new SqlConnection(connectionString);
            Connection.Open();

            InitStorage();
        }

        public static AccountManager Create()
        {
            return new AccountManager();
        }

        const IsolatedStorageScope scope = IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly;
        static void InitStorage()
        {
            using var storage = IsolatedStorageFile.GetStore(scope, null, null);

            if (!storage.GetDirectoryNames().Contains("PrivateCaptures"))
            {
                storage.CreateDirectory("PrivateCaptures");
            }
        }

        public void RegisterUser(string username, string password)
        {
            var hash = SHA256.ComputeHash(Encoding.UTF8.GetBytes(password));

            var insertUserCommand = new SqlCommand(insertUser, Connection);
            insertUserCommand.Parameters.AddWithValue("@username", username);

            insertUserCommand.Parameters.Add("@passwordHash", SqlDbType.Binary);
            insertUserCommand.Parameters["@passwordHash"].Value = hash;

            insertUserCommand.ExecuteNonQuery();

            CreateStoreForUser(username);
        }

        public bool UsernameExists(string username)
        {
            var usernameExistsCommand = new SqlCommand(usernameExixsts, Connection);
            usernameExistsCommand.Parameters.Add("@username", SqlDbType.Text);
            usernameExistsCommand.Parameters["@username"].Value = username;

            using SqlDataReader reader = usernameExistsCommand.ExecuteReader();

            return reader.HasRows;
        }

        byte[] GetPasswordHash(string username)
        {
            var getUserPasswordCommand = new SqlCommand(getUserPassword, Connection);
            getUserPasswordCommand.Parameters.Add("@username", SqlDbType.Text);
            getUserPasswordCommand.Parameters["@username"].Value = username;

            using var reader = getUserPasswordCommand.ExecuteReader();

            byte[] hash = new byte[32];

            reader.Read();
            reader.GetBytes("PasswordHash", 0, hash, 0, 32);

            return hash;
            //return (byte[])((IDataRecord)reader)["PasswordHash"];
        }

        public bool IsPasswordCorrect(string username, string password)
        {
            var hash = SHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var rightHash = GetPasswordHash(username);

            return Enumerable.SequenceEqual(rightHash, hash);
        }

        public static void CreateStoreForUser(string username)
        {
            using var storage = IsolatedStorageFile.GetStore(scope, null, null);

            storage.CreateDirectory($"{topLevelFolder}/{username}");
        }

        public static string[] GetUserFiles(string username)
        {
            using var storage = IsolatedStorageFile.GetStore(scope, null, null);

            if (!storage.GetDirectoryNames(topLevelFolder).Contains(username))
            {
                storage.CreateDirectory($"{topLevelFolder}/{username}");
            }

            return storage.GetFileNames($"{topLevelFolder}/{username}/*");
        }

        public static void MoveFileToStorage(string username, string fileName, string fileSystemFileName)
        {
            var dump = File.ReadAllBytes(fileSystemFileName);
            File.Delete(fileSystemFileName);

            using var storage = IsolatedStorageFile.GetStore(scope, null, null);
            using var stream = storage.CreateFile($"{topLevelFolder}/{username}/{fileName}");
            stream.Write(dump);
        }

        public static string CreateTempFile(string username, string privateFileName)
        {
            using var storage = IsolatedStorageFile.GetStore(scope, null, null);
            using var stream = storage.OpenFile($"{topLevelFolder}/{username}/{privateFileName}", FileMode.Open);

            byte[] dump = new byte[stream.Length];
            
            stream.Read(dump, 0, (int)stream.Length);

            string regularFileName = Path.GetRandomFileName();
            using var regularStream = File.Create(regularFileName);

            regularStream.Write(dump);

            return regularFileName;
        }

        public static void DeleteTempFile(string tempFileName)
        {
            File.Delete(tempFileName);
        }
    }
}
