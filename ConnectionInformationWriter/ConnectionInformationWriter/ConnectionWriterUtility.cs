using Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ConnectionInformationWriter
{
    public partial class ConnectionInfoWriterUtility : Form
    {
        private SqlConnectionStringBuilder csb;
        private ModifyRegistry registry;
        private string KEYNAME = "ConStr";
        private string SUBKEYNAME = "TESTREGISTRY";
        private string SALT = "298342346b29x8n2394";

        public ConnectionInfoWriterUtility()
        {
            InitializeComponent();
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            registry = new ModifyRegistry()
            {
                BaseRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32),
                ShowError = true,
                SubKey = $"SOFTWARE\\{SUBKEYNAME}"
            };
            GetAllDataSources();
            GetConnectionStringFromReg();
        }
        private void GetAllDataSources()
        {
            cmbserver.Items.Clear();
            int i = 0;
            DataTable dt = DatabaseUser.GetDataSources();
            foreach (DataRow item in dt.AsEnumerable())
            {
                cmbserver.Items.Add(dt.Rows[i][0].ToString());
                i++;
            }
        }
        private void GetConnectionStringFromReg()
        {
            csb = new SqlConnectionStringBuilder();
            if (registry.ValueCount() > 0)
            {
                csb.ConnectionString = StringCipher.Decrypt(registry.Read(KEYNAME), SALT);
            }
            txtsonstrinreg.Text = csb.ConnectionString;
        }
        private void btngetservers_Click(object sender, EventArgs e)
        {
            GetAllDataSources();
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            string connstr = "";
            if (chksqlau.Checked == true)
            {
                if (!string.IsNullOrWhiteSpace(txtusername.Text) && !string.IsNullOrWhiteSpace(txtpass.Text))
                {
                    var ops = new DatabaseUser(cmbserver.SelectedItem.ToString(), cmbdb.SelectedItem.ToString());
                    var key = "5121472313yAm";
                    var userNameBytes = StringCipher.Encrypt(txtusername.Text, key);
                    var passwordBytes = StringCipher.Encrypt(txtpass.Text, key);
                    var results = ops.SqlCredentialLogin(userNameBytes, passwordBytes, key);
                    if (results.Success)
                    {
                        connstr = results.Constr;
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show(results.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Incomplete information to continue.");
                }
            }
            if (chkwa.Checked == true)
            {
                var constr = new SqlConnectionStringBuilder()
                {
                    DataSource = cmbserver.SelectedItem.ToString(),
                    InitialCatalog = cmbdb.SelectedItem.ToString(),
                    IntegratedSecurity = true
                };
                SqlConnection con = new SqlConnection(constr.ConnectionString);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT 2", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] is DBNull)
                    {

                    }
                    if (dt.Rows[0][0].ToString() == "2")
                    {
                        connstr = constr.ConnectionString;
                    }
                }
            }
        }
        private void btngetconstr_Click(object sender, EventArgs e)
        {
            csb = new SqlConnectionStringBuilder();
            csb.DataSource = cmbserver.SelectedItem.ToString();
            csb.InitialCatalog = cmbdb.SelectedItem.ToString();
            csb.IntegratedSecurity = true;
            if (chksqlau.Checked == true)
            {
                csb.IntegratedSecurity = false;
                if (!string.IsNullOrEmpty(txtusername.Text))
                {
                    csb.UserID = txtusername.Text;
                }
                else
                {
                    csb.UserID = "";
                }
                if (!string.IsNullOrEmpty(txtpass.Text))
                {
                    csb.Password = txtpass.Text;
                }
                else
                {
                    csb.Password = "";
                }
            }
            txtconstr.Text = csb.ConnectionString;
        }
        private void btndecrypt_Click(object sender, EventArgs e)
        {
            //ConfigSectionProtector c = new ConfigSectionProtector("connectionStrings");
            //c.Decrypt("connectionStrings");
            //MessageBox.Show("Done");
        }
        private void btnencrypt_Click(object sender, EventArgs e)
        {
            //ConfigSectionProtector sectionProtector = new ConfigSectionProtector("connectionStrings");
            //string sectionName = "connectionStrings";
            ////string provName = "DPAPIProtection";
            ////sectionProtector.ProtectSection(sectionName, null);
            //sectionProtector.ProtectSection(sectionName, null);
            //MessageBox.Show("Done");
        }
        private void btnmodify_Click(object sender, EventArgs e)
        {
            string constr = StringCipher.Encrypt(txtconstr.Text, SALT);
            
            registry.Write(KEYNAME, constr);
            MessageBox.Show("Done");
            GetConnectionStringFromReg();
        }
        private void chkwa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkwa.Checked == true)
            {
                chksqlau.CheckState = CheckState.Unchecked;
                txtusername.Text = string.Empty;
                txtpass.Text = string.Empty;
            }
        }
        private void chksqlau_CheckedChanged(object sender, EventArgs e)
        {
            if (chksqlau.Checked == true)
            {
                chkwa.CheckState = CheckState.Unchecked;
                txtusername.Enabled = true;
                txtpass.Enabled = true;
            }
            if (chksqlau.Checked == false)
            {
                txtusername.Enabled = false;
                txtpass.Enabled = false;
            }
        }
        private void cmbserver_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdb.Items.Clear();
            int i = 0;
            DataTable dt = DatabaseUser.GetDatabases(cmbserver.SelectedItem.ToString());
            foreach (DataRow item in dt.AsEnumerable())
            {
                cmbdb.Items.Add(dt.Rows[i][0].ToString());
                i++;
            }
        }
        private void cmbdb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connStr = txtconstr.Text;
            csb = new SqlConnectionStringBuilder(connStr);
            if (cmbserver.SelectedItem.ToString() == $"{Environment.MachineName}\\")
            {
                csb.DataSource = ".";
            }
            else
            {
                csb.DataSource = cmbserver.SelectedItem.ToString();
            }
            csb.InitialCatalog = cmbdb.SelectedItem.ToString();
            txtconstr.Text = csb.ToString();
        }
    }
    public class Key
    {
        public string KeyName { get; set; }
        public List<KeyValuePair<string, object>> Values { get; set; }

        /// <summary>
        ///  Returns a list of key value pairs in registry
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hive"></param>
        /// <returns></returns>
        public static List<Key> GetSubkeysValue(string path, RegistryHive hive)
        {
            var result = new List<Key>();
            using (var hiveKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
            using (var key = hiveKey.OpenSubKey(path))
            {
                var subkeys = key.GetSubKeyNames();

                foreach (var subkey in subkeys)
                {
                    var values = GetKeyValue(hiveKey, path, subkey);
                    result.Add(values);
                }
            }
            return result;
        }

        /// <summary>
        ///  Returns a list of names of keys  in registry
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hive"></param>
        /// <returns></returns>
        public static List<string> GetSubkeysName(RegistryHive hive, string path)
        {
            var result = new List<string>();
            var subkey = $@"{path}";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey))
            {
                if (key == null)
                {
                    return null;
                }

                foreach (var valueName in key.GetValueNames())
                {
                    result.Add(valueName);
                }
            }
            return result;
        }
        /// <summary>
        /// Returns KeysValue
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static Key GetKeyValue(RegistryKey hive, string path, string keyName)
        {
            var result = new Key() { KeyName = keyName };
            var subkey = $@"{path}\{keyName}";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(subkey))
            {
                if (key == null)
                {
                    return null;
                }

                foreach (var valueName in key.GetValueNames())
                {
                    var val = key.GetValue(valueName);
                    var pair = new KeyValuePair<string, object>(valueName, val);
                    result.Values.Add(pair);
                }
            }

            return result;
        }
    }
}
namespace Library
{
    /// <summary>
    /// Model Library
    /// </summary>
    public class SqlServerLoginResult
    {
        public bool Success { get; set; }
        public bool Failed => Success == false;
        public bool GenericException { get; set; }
        public string Message { get; set; }
        public string Constr { get; set; }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// Responsible to validating a user has permissions
    /// to access the database, not tables.
    /// </summary>
    public class DatabaseUser
    {
        private readonly string serverName;
        private readonly string catalogName;
        public DatabaseUser(string pServerName, string pCatalogName)
        {
            serverName = pServerName;
            catalogName = pCatalogName;
        }
        public static DataTable GetDatabases(string pServerName)
        {
            DataTable sdt = new DataTable();
            string connectionString = $"Data Source={pServerName}; Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(sdt);
            }
            return sdt;
        }
        public static DataTable GetDataSources()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable table = instance.GetDataSources();
            string ServerName = Environment.MachineName;
            DataTable compinst = new DataTable();
            compinst.Columns.Add("Instances");
            foreach (DataRow row in table.Rows)
            {
                DataRow dataRow = compinst.NewRow();
                dataRow["Instances"] = ServerName + "\\" + row["InstanceName"].ToString();
                compinst.Rows.Add(dataRow);
            }
            return compinst;
        }
        public SqlServerLoginResult Login(string pName, string pPasswor, string key)
        {
            var loginResult = new SqlServerLoginResult();
            var userName = StringCipher.Decrypt(pName, key);
            var userPassword = StringCipher.Decrypt(pPasswor, key);

            string ConnectionString =
                $"Data Source={serverName};" +
                $"Initial Catalog={catalogName};" +
                $"User Id={userName};Password={userPassword};" +
                "Integrated Security=False";

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                try
                {
                    cn.Open();
                    loginResult.Success = true;
                    loginResult.Constr =
                                    $"Data Source={serverName};" +
                                    $"Initial Catalog={catalogName};" +
                                    $"User Id={userName};Password={userPassword};" +
                                    "Integrated Security=False";
                }
                catch (SqlException failedLoginException) when (failedLoginException.Number == 18456)
                {
                    loginResult.Success = false;
                    loginResult.GenericException = false;
                    loginResult.Message = "Can not access data.";
                }
                catch (Exception ex)
                {
                    loginResult.Success = false;
                    loginResult.GenericException = true;
                    loginResult.Message = ex.Message;
                }
            }
            return loginResult;
        }
        public SqlServerLoginResult SqlCredentialLogin(string pName, string pPasswor,string key)
        {
            var loginResult = new SqlServerLoginResult();
            var userName = StringCipher.Decrypt(pName, key);
            var userPassword = StringCipher.Decrypt(pPasswor, key);

            string connectionString =
                $"Data Source={serverName};" +
                $"Initial Catalog={catalogName};" +
                "Integrated Security=False";

            var securePassword = new SecureString();
            foreach (var character in userPassword)
            {
                securePassword.AppendChar(character);
            }
            securePassword.MakeReadOnly();
            var credentials = new SqlCredential(userName, securePassword);
            using (var cn = new SqlConnection { ConnectionString = connectionString })
            {
                try
                {
                    cn.Credential = credentials;
                    cn.Open();
                    loginResult.Success = true;
                    loginResult.Constr =
                                    $"Data Source={serverName};" +
                                    $"Initial Catalog={catalogName};" +
                                    $"User Id={userName};Password={userPassword};" +
                                    "Integrated Security=False";
                }
                catch (SqlException failedLoginException) when (failedLoginException.Number == 18456)
                {
                    loginResult.Success = false;
                    loginResult.GenericException = false;
                    loginResult.Message = "Can not access data.";
                }
                catch (Exception ex)
                {
                    loginResult.Success = false;
                    loginResult.GenericException = true;
                    loginResult.Message = ex.Message;
                }
            }
            return loginResult;
        }
    }
    /// <summary>
    ///  Used For Encryption
    /// </summary>
    public static class StringCipher
    {
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        public static string Encrypt(string plainText, string passPhrase)
        {
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }
        public static string Decrypt(string cipherText, string passPhrase)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
    /// <summary>
    /// An useful class to read/write/delete/count registry keys
    /// </summary>
    public class ModifyRegistry
    {
        private bool showError = false;
        /// <summary>
        /// A property to show or hide error messages 
        /// (default = false)
        /// </summary>
        public bool ShowError
        {
            get { return showError; }
            set { showError = value; }
        }
        private string subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();
        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        /// </summary>
        public string SubKey
        {
            get { return subKey; }
            set { subKey = value; }
        }
        private RegistryKey baseRegistryKey = Registry.LocalMachine;
        private RegistryKey rgk;

        /// <summary>
        /// A property to set the BaseRegistryKey value.
        /// (default = Registry.LocalMachine)
        /// </summary>
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }
        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName)
        {
            RegistryKey sk1 = baseRegistryKey.OpenSubKey(SubKey);
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }
        /// <summary>
        /// To write into a registry key.
        /// input: KeyName (string) , Value (object)
        /// output: true or false 
        /// </summary>
        public bool Write(string KeyName, object Value)
        {
            try
            {
                string user = Environment.UserDomainName + "\\" + Environment.UserName;

                RegistrySecurity rs = new RegistrySecurity();
                rs.AddAccessRule(new RegistryAccessRule(user,
                    RegistryRights.FullControl | RegistryRights.CreateSubKey | RegistryRights.SetValue | RegistryRights.ChangePermissions | RegistryRights.WriteKey,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow));

                DeleteKey(KeyName);

                rgk = baseRegistryKey.CreateSubKey(SubKey, RegistryKeyPermissionCheck.ReadWriteSubTree, rs);
                rgk.SetValue(KeyName.ToUpper(), Value, RegistryValueKind.String);
                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }
        /// <summary>
        /// To delete a registry key.
        /// input: KeyName (string)
        /// output: true or false 
        /// </summary>
        public bool DeleteKey(string KeyName)
        {
            try
            {
                string user = Environment.UserDomainName + "\\" + Environment.UserName;
                RegistrySecurity rs = new RegistrySecurity();
                rs.AddAccessRule(new RegistryAccessRule(user,
                    RegistryRights.FullControl | RegistryRights.Delete,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow));

                RegistryKey sk1 = baseRegistryKey.CreateSubKey(SubKey, RegistryKeyPermissionCheck.ReadWriteSubTree, rs);

                if (KeyExists(baseRegistryKey, SubKey + "\\" + KeyName))
                {
                    sk1.DeleteValue(KeyName);
                }
                else
                {
                    return true;
                }

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + SubKey);
                return false;
            }
        }
        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false 
        /// </summary>
        public bool DeleteSubKeyTree()
        {
            try
            {
                RegistryKey sk1 = baseRegistryKey.OpenSubKey(subKey);
                if (sk1 != null)
                {
                    baseRegistryKey.DeleteSubKeyTree(subKey);
                }

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }
        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// input: void
        /// output: number of subkeys
        /// </summary>
        public int SubKeyCount()
        {
            try
            {
                RegistryKey sk1 = baseRegistryKey.OpenSubKey(subKey);
                if (sk1 != null)
                {
                    return sk1.SubKeyCount;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Retriving subkeys of " + subKey);
                return 0;
            }
        }
        /// <summary>
        /// Retrive the count of values in the key.
        /// input: void
        /// output: number of keys
        /// </summary>
        public int ValueCount()
        {
            try
            {
                RegistryKey sk1 = baseRegistryKey.OpenSubKey(subKey);
                if (sk1 != null)
                {
                    return sk1.ValueCount;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Retriving keys of " + subKey);
                return 0;
            }
        }
        private bool KeyExists(RegistryKey baseKey, string subKeyName)
        {
            RegistryKey ret = baseKey.OpenSubKey(subKeyName);

            return ret != null;
        }
        private void ShowErrorMessage(Exception e, string Title)
        {
            throw e;
        }
    }
}