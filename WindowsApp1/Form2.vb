Imports System.IO
Imports System.Net
Imports System.Security
Imports System.Security.Cryptography
Imports System.Threading
Imports Volcano.ManualMapInjection.Injection


Public Class Form2
    Public Property FilePath As String
    Private KeyText = "ASDfghJKL"
    Dim name
    Dim status
    Dim statusof
    Dim colorstatus As Color
    Dim canrun As Boolean

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim c As WebRequest = WebRequest.Create("https://volcanohacks.eu/loaderauth/status.txt")
        Dim response = c.GetResponse()
        Dim content = response.GetResponseStream()
        Dim reader = New StreamReader(content)

        status = reader.ReadToEnd()

        If (status = "0") Then
            statusof = "Wykryty !"
            colorstatus = System.Drawing.Color.Red
            canrun = False

        ElseIf (status = "1") Then
            statusof = "Niewykryty"
            colorstatus = System.Drawing.Color.Green
            canrun = True

        ElseIf (status = "2") Then
            statusof = "Złamany !"
            colorstatus = System.Drawing.Color.Red
            canrun = False

        ElseIf (status = "3") Then
        statusof = "Wyłączony"
            colorstatus = System.Drawing.Color.Red
            canrun = False
        End If

        name = Form1.TextBox1.Text
        Form1.WebBrowser1.AllowNavigation = False
        Form1.WebBrowser2.AllowNavigation = False
        Form1.WebBrowser3.AllowNavigation = False

        ' Label3.Text = Form1.TextBox1.Text
        If Form1.rank = 4 Or Form1.rank = 3 Then
            '  Label4.Text = "Nigdy"
        End If
        Timer1.Start()

    End Sub
    Function IsAdmin(ranked As Integer) As Boolean
        If ranked = 4 Or ranked = 3 Then
            Return True
        Else
            Return False
        End If
    End Function
    Function IsSub(ranked As Integer) As Boolean
        If ranked = 8 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged


    End Sub

    Private Sub ListBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedValueChanged

    End Sub

    'Nazwa:
    '-

    'Wygasa:
    '-

    'Status:

    '-
    'Ostatnia detekcja : 
    '-
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        RichTextBox1.ForeColor = colorstatus
        RichTextBox1.Text = (vbNewLine + "Nazwa :" + vbNewLine + name + vbNewLine + vbNewLine + "Wygasa :" + vbNewLine + "-" + vbNewLine + vbNewLine + "Status :" + vbNewLine + statusof + vbNewLine + vbNewLine + "Ostatnia detekcja :" + vbNewLine + "-")

    End Sub
    Private Sub DecryptFile()
        Try
            Dim inName As String = Me.FilePath

            If Me.KeyText = "" Then
                Throw New Exception("Please enter a key.")
            End If

            If Path.GetExtension(inName) <> ".des" Then
                Throw New Exception("Not a .des file.")
            End If

            Dim outName As String = Path.ChangeExtension(FilePath, "")

            If Not overwriteifExist(outName) Then
                Throw New IOException("File not overwritten")
            End If

            Dim desKey As Byte() = Me.keytoByteArray()
            Dim desIV As Byte() = Me.keytoByteArray()

            Using fin As FileStream = New FileStream(inName, FileMode.Open, FileAccess.Read)

                Using fout As FileStream = New FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write)
                    fout.SetLength(0)
                    Dim bin As Byte() = New Byte(99) {}
                    Dim rdlen As Long = 0
                    Dim totlen As Long = fin.Length
                    Dim len As Integer
                    Dim des As DES = New DESCryptoServiceProvider()
                    Dim decStream As CryptoStream = New CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write)

                    While rdlen < totlen
                        len = fin.Read(bin, 0, 100)
                        decStream.Write(bin, 0, len)
                        rdlen = rdlen + len
                    End While


                    decStream.Close()
                    fout.Close()
                    fin.Close()
                End Using
            End Using

        Catch e As Exception

            If TypeOf e Is System.IO.FileNotFoundException Then
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1)
            ElseIf TypeOf e Is System.Security.Cryptography.CryptographicException Then
                MessageBox.Show("Bad key or file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1)
                File.Delete(Path.ChangeExtension(FilePath, ""))
            ElseIf TypeOf e Is IOException Then
            Else
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error], MessageBoxDefaultButton.Button1)
            End If
        End Try
    End Sub

    Private Function keytoByteArray() As Byte()
        Dim KeyArray As Byte() = Enumerable.Repeat(CByte(0), 8).ToArray()

        For i As Integer = 0 To KeyText.Length - 1
            Dim b As Byte = CByte(KeyText(i))
            KeyArray(i Mod 8) = CByte((KeyArray(i Mod 8) + b))
        Next

        Return KeyArray
    End Function

    Public Function overwriteifExist(ByVal outName As String) As Boolean
        If File.Exists(outName) Then
            Return True
        End If

        Return True
    End Function
    Protected Sub AntiLeak()
        Dim path As String = "C:\Windows\twins\"
        DeleteDirectory(path)
    End Sub

    Private Sub DeleteDirectory(path As String)
        If Directory.Exists(path) Then
            'Delete all files from the Directory
            For Each filepath As String In Directory.GetFiles(path)
                File.Delete(filepath)
            Next
            'Delete all child Directories
            For Each dir As String In Directory.GetDirectories(path)
                DeleteDirectory(dir)
            Next
            'Delete a Directory
            Directory.Delete(path)
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        If (canrun) Then
            If ListBox1.SelectedIndex = 0 Then
                Thread.Sleep(200)
                Dim name = "csgo.exe"
                Dim target = Process.GetProcessesByName(name).FirstOrDefault()

                If target Is Nothing Then

                    Using wb = New WebClient()
                        wb.Headers.Add("User-Agent", "YnV1dWdzcHJvZHVjdGlvbm1hbmFnZWxlYWs=")
                        System.IO.Directory.CreateDirectory("C:\\Windows\\twins\\")
                        wb.DownloadFile("https://volcanohacks.eu/forum/inc/loadlib.deb", "C:\\Windows\\twins\\loadbin.dll")
                        wb.DownloadFile("https://volcanohacks.eu/forum/inc/loadlib.lib", "C:\\Windows\\twins\\loadlib.exe")

                    End Using


                    Dim path = "C:\Windows\twins\loadbin.dll"
                    File.ReadAllBytes(path)

                    If Not File.Exists(path) Then
                        MessageBox.Show("Error: An unexpected error happened, loader will now restart", "VOLCANO")
                        Application.Restart()
                    End If


                    Process.Start("C:\Windows\twins\loadlib.exe")

                    MsgBox("Test")
                    AntiLeak()
                    ' File.Delete("C:\Users\Public\Documents\ezpz.dll.des")
                    ' File.Delete("C:\Users\Public\Documents\ezpz.dll")


                Else
                    MessageBox.Show("Error: CS:GO is not open! Please start CS:GO the inject", "VOLCANO")
                End If

            End If

        Else
            If (status = "0") Then
                MsgBox("Cheat został wykryty przez VAC i jest chwilowo wyłączony !")
            End If
            If (status = "2") Then
                MsgBox("Cheat został złamany i jest chwilowo wyłączony !")
            End If
            If (status = "3") Then
                MsgBox("Trwa aktualizacja cheata !")
            End If
        End If






    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Form1BindingSource_CurrentChanged(sender As Object, e As EventArgs)

    End Sub
End Class