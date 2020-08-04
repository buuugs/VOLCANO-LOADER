using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Volcano
{
    public partial class Form2
    {
        public Form2()
        {
            InitializeComponent();
            _ListBox1.Name = "ListBox1";
            _GroupBox1.Name = "GroupBox1";
            _Button1.Name = "Button1";
            _Button2.Name = "Button2";
            _Splitter1.Name = "Splitter1";
            _RichTextBox1.Name = "RichTextBox1";
            _GroupBox2.Name = "GroupBox2";
        }

        public string FilePath { get; set; }

        private object KeyText = "ASDfghJKL";
        private object name;

        private void Form3_Load(object sender, EventArgs e)
        {
            name = Volcano.My.MyProject.Forms.Form1.TextBox1.Text;
            Volcano.My.MyProject.Forms.Form1.WebBrowser1.AllowNavigation = false;
            Volcano.My.MyProject.Forms.Form1.WebBrowser2.AllowNavigation = false;
            Volcano.My.MyProject.Forms.Form1.WebBrowser3.AllowNavigation = false;

            // Label3.Text = Form1.TextBox1.Text
            if (Volcano.My.MyProject.Forms.Form1.rank == 4 | Volcano.My.MyProject.Forms.Form1.rank == 3)
            {
                // Label4.Text = "Nigdy"
            }

            this.Timer1.Start();
        }

        public bool IsAdmin(int ranked)
        {
            if (ranked == 4 | ranked == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsSub(int ranked)
        {
            if (ranked == 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        // Nazwa:
        // -

        // Wygasa:
        // -

        // Status:

        // -
        // Ostatnia detekcja : 
        // -
        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.RichTextBox1.Text = Conversions.ToString(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(Constants.vbNewLine + "Nazwa :" + Constants.vbNewLine, name), Constants.vbNewLine), Constants.vbNewLine), "Wygasa :"), Constants.vbNewLine), "-"), Constants.vbNewLine), Constants.vbNewLine), "Status :"), Constants.vbNewLine), "-"), Constants.vbNewLine), Constants.vbNewLine), "Ostatnia detekcja :"), Constants.vbNewLine), "-"));
        }

        private void DecryptFile()
        {
            try
            {
                string inName = FilePath;
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(KeyText, "", false)))
                {
                    throw new Exception("Please enter a key.");
                }

                if (Path.GetExtension(inName) != ".des")
                {
                    throw new Exception("Not a .des file.");
                }

                string outName = Path.ChangeExtension(FilePath, "");
                if (!overwriteifExist(outName))
                {
                    throw new IOException("File not overwritten");
                }

                var desKey = keytoByteArray();
                var desIV = keytoByteArray();
                using (var fin = new FileStream(inName, FileMode.Open, FileAccess.Read))
                {
                    using (var fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fout.SetLength(0);
                        var bin = new byte[100];
                        long rdlen = 0;
                        long totlen = fin.Length;
                        int len;
                        DES des = new DESCryptoServiceProvider();
                        var decStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
                        while (rdlen < totlen)
                        {
                            len = fin.Read(bin, 0, 100);
                            decStream.Write(bin, 0, len);
                            rdlen = rdlen + len;
                        }

                        decStream.Close();
                        fout.Close();
                        fin.Close();
                    }
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else if (e is CryptographicException)
                {
                    MessageBox.Show("Bad key or file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    File.Delete(Path.ChangeExtension(FilePath, ""));
                }
                else if (e is IOException)
                {
                }
                else
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private byte[] keytoByteArray()
        {
            var KeyArray = Enumerable.Repeat(Conversions.ToByte(0), 8).ToArray();
            for (int i = 0, loopTo = Operators.SubtractObject(KeyText.Length, 1); i <= Conversions.ToInteger(loopTo); i++)
            {
                byte b = Conversions.ToByte(this.KeyText((object)i));
                KeyArray[i % 8] = KeyArray[i % 8] + b;
            }

            return KeyArray;
        }

        public bool overwriteifExist(string outName)
        {
            if (File.Exists(outName))
            {
                return true;
            }

            return true;
        }

        protected void AntiLeak()
        {
            string path = @"C:\Windows\twins\";
            DeleteDirectory(path);
        }

        private void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                // Delete all files from the Directory
                foreach (string filepath in Directory.GetFiles(path))
                    File.Delete(filepath);
                // Delete all child Directories
                foreach (string dir in Directory.GetDirectories(path))
                    DeleteDirectory(dir);
                // Delete a Directory
                Directory.Delete(path);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.ListBox1.SelectedIndex == 0)
            {
                Thread.Sleep(200);
                string name = "csgo";
                var target = Process.GetProcessesByName(name).FirstOrDefault();
                if (target is null)
                {
                    using (var wb = new WebClient())
                    {
                        wb.Headers.Add("User-Agent", "YnV1dWdzcHJvZHVjdGlvbm1hbmFnZWxlYWs=");
                        Directory.CreateDirectory(@"C:\\Windows\\twins\\");
                        wb.DownloadFile("https://volcanohacks.eu/forum/inc/loadlib.deb", @"C:\\Windows\\twins\\loadbin.dll");
                    }

                    string path = @"C:\Windows\twins\loadbin.dll";
                    File.ReadAllBytes(path);
                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Error: An unexpected error happened, loader will now restart", "Thaisen.pw");
                        Application.Restart();
                    }

                    var(injector == (new ManualMapInjector[target + 1]));
                    Interaction.MsgBox("Test");
                    AntiLeak();
                }
                // File.Delete("C:\Users\Public\Documents\ezpz.dll.des")
                // File.Delete("C:\Users\Public\Documents\ezpz.dll")


                else
                {
                    MessageBox.Show("Error: CS:GO is not open! Please start CS:GO the inject", "Thaisen.pw");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1BindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }
    }
}