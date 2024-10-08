Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports DbUp
Imports Microsoft.Win32
Imports System.Collections.Generic
Imports Microsoft.VisualBasic.Logging
Imports DbUp.Engine.Output
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text


Public Class DBUpdater
    Dim connection As SqlConnection

    ' Form load: checks config, loads initial values
    Private Sub DBUpdater_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check if the config file or settings are missing and create it if needed
        Try
            If ConfigurationManager.AppSettings("Server") Is Nothing OrElse
               ConfigurationManager.AppSettings("Login") Is Nothing OrElse
               ConfigurationManager.AppSettings("Password") Is Nothing OrElse
               ConfigurationManager.ConnectionStrings("MyConnectionString") Is Nothing OrElse
                ConfigurationManager.AppSettings("SavePassword") Is Nothing Then

                ' If any of the settings are missing, create the config
                CreateDefaultConfig()
            End If

            ' Load values from config into the form
            txtServer.Text = ConfigurationManager.AppSettings("Server")
            txtLogin.Text = ConfigurationManager.AppSettings("Login")

            Dim savePassword As Boolean = Boolean.Parse(ConfigurationManager.AppSettings("SavePassword"))
            chkSavePassword.Checked = savePassword

            'txtPassword.Text = ConfigurationManager.AppSettings("Password")
            ' Decrypt the password before loading it
            Dim encryptedPassword As String = ConfigurationManager.AppSettings("Password")
            If Not String.IsNullOrEmpty(encryptedPassword) Then
                txtPassword.Text = DecryptString(encryptedPassword)
            Else
                txtPassword.Text = String.Empty
            End If

            btnConnect.BackColor = Color.LightCoral

        Catch ex As Exception
            MessageBox.Show("Error reading config: " & ex.Message)
        End Try

        ' Initially disable database combo and select button
        cmbDatabases.Enabled = False
        btnSelect.Enabled = False
    End Sub

    ' Connect button click handler
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try

            btnConnect.BackColor = SystemColors.ButtonFace
            btnSelect.BackColor = Color.LightCoral

            ' Clear the DataGridView and the migration output text
            dgvDBVersion.Rows.Clear()      ' Clear the DataGridView
            txtMigrationOutput.Clear()     ' Clear the migration output textbox
            cmbDatabases.SelectedIndex = -1

            ' Construct the connection string dynamically
            Dim connString As String = $"Server={txtServer.Text};User Id={txtLogin.Text};Password={txtPassword.Text};"
            connection = New SqlConnection(connString)
            connection.Open()

            ' If connection is successful, enable the databases dropdown and select button
            cmbDatabases.Enabled = True
            btnSelect.Enabled = True
            btnRunMigration.Enabled = False

            ' Populate the list of databases
            Dim cmd As New SqlCommand("SELECT name FROM sys.databases WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb')", connection)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            cmbDatabases.Items.Clear()

            While reader.Read()
                cmbDatabases.Items.Add(reader("name"))
            End While
            reader.Close()

            ' Save the server, login, and password back to App.config
            UpdateAppConfig("Server", txtServer.Text)
            UpdateAppConfig("Login", txtLogin.Text)
            'UpdateAppConfig("Password", txtPassword.Text)
            UpdateAppConfig("SavePassword", chkSavePassword.Checked.ToString())

            ' Handle the password saving based on checkbox state
            If chkSavePassword.Checked Then
                UpdateAppConfig("Password", EncryptString(txtPassword.Text))
            Else
                RemoveAppConfig("Password") ' Remove password if checkbox is unchecked
            End If


            MessageBox.Show("Connected successfully!")
        Catch ex As Exception
            MessageBox.Show("Connection failed: " & ex.Message)
        End Try
    End Sub

    ' Helper function to encrypt a string
    Private Function EncryptString(input As String) As String
        Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(input)
        Dim encryptedBytes As Byte() = ProtectedData.Protect(inputBytes, Nothing, DataProtectionScope.CurrentUser)
        Return Convert.ToBase64String(encryptedBytes)
    End Function

    ' Helper function to decrypt a string
    Private Function DecryptString(input As String) As String
        Dim encryptedBytes As Byte() = Convert.FromBase64String(input)
        Dim decryptedBytes As Byte() = ProtectedData.Unprotect(encryptedBytes, Nothing, DataProtectionScope.CurrentUser)
        Return Encoding.UTF8.GetString(decryptedBytes)
    End Function

    Private Sub RemoveAppConfig(key As String)
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        If ConfigurationManager.AppSettings(key) IsNot Nothing Then
            config.AppSettings.Settings.Remove(key)
        End If
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub

    ' Select button click handler to run the query and show results from SchemaVersions
    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        ' Clear the DataGridView and the migration output text
        dgvDBVersion.Rows.Clear()      ' Clear the DataGridView
        txtMigrationOutput.Clear()     ' Clear the migration output textbox

        btnSelect.BackColor = SystemColors.ButtonFace
        btnRunMigration.BackColor = Color.LightCoral

        ' Clear the previous version
        txtCurrentVersion.Clear()

        ' Get the current assembly
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()

        ' Get all embedded resource names
        Dim resourceNames As String() = assembly.GetManifestResourceNames()

        ' Initialize variables to find the highest sequence number
        Dim highestSequence As Integer = -1
        Dim highestResourceName As String = String.Empty

        ' Loop through each resource name
        For Each resourceName As String In resourceNames
            Debug.WriteLine(resourceName) ' Output resource names to the Debug console
            ' Check if the resource name starts with "DBU_"
            If resourceName.StartsWith("DBUpdater.DBU_") Then ' Adjust to your project's namespace
                ' Extract the sequence number
                Dim parts As String() = resourceName.Split("_"c) ' Split by underscore
                If parts.Length > 1 Then
                    Dim sequencePart As String = parts(1).Split("-"c)(0) ' Take the part after "DBU_" and before the "-"
                    Dim sequenceNumber As Integer

                    ' Remove leading zeros by parsing the number
                    If Integer.TryParse(sequencePart, sequenceNumber) Then
                        ' Check if this sequence number is higher than the current highest
                        If sequenceNumber > highestSequence Then
                            highestSequence = sequenceNumber
                        End If
                    End If
                End If
            End If
        Next

        ' Display the highest sequence number in the textbox
        If highestSequence >= 0 Then
            txtCurrentVersion.Text = highestSequence.ToString("D7") ' Format as 7 digits (e.g., 0000001)
        Else
            txtCurrentVersion.Text = "No matching files found."
        End If

        If cmbDatabases.SelectedItem IsNot Nothing Then
            Try
                ' Update connection string with the selected database
                Dim connString As String = $"Server={txtServer.Text};Database={cmbDatabases.SelectedItem.ToString()};User Id={txtLogin.Text};Password={txtPassword.Text};"
                connection = New SqlConnection(connString)
                connection.Open()

                ' Check if the dbo.Company table exists in the database - If not, the selected database is certainly not one for the application
                Dim cmd As New SqlCommand("IF OBJECT_ID('dbo.Company', 'U') IS NOT NULL SELECT 1 ELSE SELECT 0", connection)
                Dim tableExists As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If tableExists = 1 Then
                    ' Table dbo.Company exists, enable the Migration button
                    btnRunMigration.Enabled = True
                    btnSaveToRegistry.Enabled = True

                    ' SQL query to get data from SchemaVersions table
                    Dim schemaCmd As New SqlCommand("SELECT TOP 1 * FROM SchemaVersions ORDER BY 3 DESC", connection)
                    Dim reader As SqlDataReader = schemaCmd.ExecuteReader()

                    ' Clear previous data in DataGridView
                    dgvDBVersion.Rows.Clear()
                    dgvDBVersion.Columns.Clear()

                    ' Dynamically add columns to the DataGridView based on the query result
                    dgvDBVersion.ColumnCount = reader.FieldCount
                    For i As Integer = 0 To reader.FieldCount - 1
                        dgvDBVersion.Columns(i).Name = reader.GetName(i) ' Set the column names based on the SchemaVersions table fields
                    Next

                    ' Add rows to the DataGridView
                    While reader.Read()
                        Dim row As String() = New String(reader.FieldCount - 1) {}
                        For i As Integer = 0 To reader.FieldCount - 1
                            row(i) = reader(i).ToString()
                        Next
                        dgvDBVersion.Rows.Add(row)
                    End While
                    reader.Close()
                Else
                    ' Table dbo.Company does not exist, show an error message and do not enable migration
                    btnRunMigration.Enabled = False
                    btnSaveToRegistry.Enabled = False
                    MessageBox.Show("The selected database is not valid as it does not contain the dbo.Company table.", "Invalid Database", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Catch ex As Exception
                ' MessageBox.Show("Query failed: " & ex.Message)
                MessageBox.Show("Query failed: version table not found. Run the migration to create it." & vbCrLf & vbCrLf _
                    & ex.Message)
            End Try
        Else
            MessageBox.Show("Please select a database.")
        End If

    End Sub

    ' "Run Migration" button click handler
    Private Sub btnRunMigration_Click(sender As Object, e As EventArgs) Handles btnRunMigration.Click
        If cmbDatabases.SelectedItem IsNot Nothing Then
            Try

                btnRunMigration.BackColor = SystemColors.ButtonFace

                ' Update connection string with the selected database
                Dim connString As String = $"Server={txtServer.Text};Database={cmbDatabases.SelectedItem.ToString()};User Id={txtLogin.Text};Password={txtPassword.Text};"

                ' Create an instance of the custom logger
                Dim customLogger As New CustomLogger()

                ' Configure DbUp with the selected connection string and custom logger
                Dim upgrader = DeployChanges.To.SqlDatabase(connString) _
                                           .WithScriptsEmbeddedInAssembly(System.Reflection.Assembly.GetExecutingAssembly()) _
                                           .LogTo(customLogger) _
                                           .LogScriptOutput() _
                                           .Build()

                ' Run migration
                Dim result = upgrader.PerformUpgrade()

                ' Clear previous output messages
                txtMigrationOutput.Clear()

                ' Append log messages to the output
                Dim logMessages As String = String.Join(vbCrLf, customLogger.Messages)
                If Not String.IsNullOrWhiteSpace(logMessages) Then
                    txtMigrationOutput.AppendText("Log Messages:" & vbCrLf)
                    txtMigrationOutput.AppendText(logMessages)
                    txtMigrationOutput.AppendText(vbCrLf)
                End If

                ' Display migration result
                If result.Successful Then
                    txtMigrationOutput.AppendText("Migration successful!" & vbCrLf)
                Else
                    txtMigrationOutput.AppendText("Migration failed: " & result.Error.Message & vbCrLf)
                End If

                ' If the migration failed, report the error
                If Not result.Successful Then
                    MessageBox.Show("Migration failed! Check output for details.")
                End If
            Catch ex As Exception
                MessageBox.Show("Migration failed: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Please select a database before running migration.")
        End If
    End Sub

    ' Helper function to update the App.config file
    Private Sub UpdateAppConfig(key As String, value As String)
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        If ConfigurationManager.AppSettings(key) IsNot Nothing Then
            config.AppSettings.Settings(key).Value = value
        Else
            config.AppSettings.Settings.Add(key, value)
        End If
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub

    ' Function to create the default App.config if missing
    Private Sub CreateDefaultConfig()
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

        ' Registry key path
        Dim registryKeyPath As String = "HKEY_CURRENT_USER\Software\SAM00\Database"

        ' Fetch server from .config or registry
        Dim server As String = ConfigurationManager.AppSettings("Server")
        If String.IsNullOrEmpty(server) Then
            ' Try to read the value from the registry
            server = Registry.GetValue(registryKeyPath, "Datasource", Nothing)?.ToString()
            If Not String.IsNullOrEmpty(server) Then
                ' Handle double backslashes in the server name
                server = server.Replace("\\", "\") ' Replace any double backslashes with a single backslash

                ' Save to .config if found in the registry
                config.AppSettings.Settings.Add("Server", server)
            End If
        End If

        ' Fetch login from .config or registry
        Dim login As String = ConfigurationManager.AppSettings("Login")
        If String.IsNullOrEmpty(login) Then
            ' Try to read the value from the registry
            login = Registry.GetValue(registryKeyPath, "User", Nothing)?.ToString()
            If Not String.IsNullOrEmpty(login) Then
                ' Save to .config if found in the registry
                config.AppSettings.Settings.Add("Login", login)
            End If
        End If

        ' Check if "SavePassword" option exists
        Dim savePassword As Boolean = False
        If ConfigurationManager.AppSettings("SavePassword") Is Nothing Then
            config.AppSettings.Settings.Add("SavePassword", "False")
        Else
            savePassword = Boolean.Parse(ConfigurationManager.AppSettings("SavePassword"))
        End If


        ' Fetch password if "SavePassword" is true
        If savePassword Then
            Dim password As String = ConfigurationManager.AppSettings("Password")
            If String.IsNullOrEmpty(password) Then
                ' Try to read the password from the registry
                password = Registry.GetValue(registryKeyPath, "Password", Nothing)?.ToString()
                If Not String.IsNullOrEmpty(password) Then
                    ' Encrypt and save the password to the config file if it exists in the registry
                    config.AppSettings.Settings.Add("Password", EncryptString(password))
                End If
            End If
        Else
            ' If SavePassword is False, make sure password is not saved in the config file
            If ConfigurationManager.AppSettings("Password") IsNot Nothing Then
                config.AppSettings.Settings.Remove("Password")
            End If
        End If

        ' Save the updated configuration to the .config file
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub

    Private Sub btnSaveToRegistry_Click(sender As Object, e As EventArgs) Handles btnSaveToRegistry.Click
        Try
            ' Open the registry key where you want to save the values
            Dim regKey As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\SAM00\Database")

            ' Set the values for the server name and login
            regKey.SetValue("DataSource", txtServer.Text)
            regKey.SetValue("InitialCatalog", cmbDatabases.Text)
            regKey.SetValue("User", txtLogin.Text)

            ' Close the registry key
            regKey.Close()

            MessageBox.Show("Server and Login have been saved to the registry.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error saving to registry: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        ' Close the application
        Me.Close()
    End Sub

    Private Sub btnCopyToClipboard_Click(sender As Object, e As EventArgs) Handles btnCopyToClipboard.Click
        ' Copy the content of txtMigrationOutput to the clipboard
        If Not String.IsNullOrEmpty(txtMigrationOutput.Text) Then
            Clipboard.SetText(txtMigrationOutput.Text)
            MessageBox.Show("Migration output copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Migration output is empty. Nothing to copy.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        Dim aboutForm As New AboutForm()
        aboutForm.ShowDialog() ' Open the About form as a modal dialog
    End Sub

    Private Sub cmbDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDatabases.SelectedIndexChanged
        btnSelect.BackColor = Color.LightCoral
        btnRunMigration.BackColor = SystemColors.ButtonFace
        btnRunMigration.Enabled = False

        ' Clear the DataGridView and the migration output text
        dgvDBVersion.Rows.Clear()      ' Clear the DataGridView
        txtMigrationOutput.Clear()     ' Clear the migration output textbox

    End Sub
End Class


Public Class CustomLogger
    Implements IUpgradeLog

    Private ReadOnly _messages As New List(Of String)()

    ' Property to retrieve the logged messages
    Public ReadOnly Property Messages As List(Of String)
        Get
            Return _messages
        End Get
    End Property


    ' Log an error message
    Public Sub WriteError(format As String, ParamArray args() As Object) Implements IUpgradeLog.WriteError
        _messages.Add("ERROR: " & String.Format(format, args))
    End Sub

    ' Log a warning message
    Public Sub WriteWarning(format As String, ParamArray args() As Object) Implements IUpgradeLog.WriteWarning
        _messages.Add("WARNING: " & String.Format(format, args))
    End Sub

    ' Log an informational message
    Public Sub WriteInformation(format As String, ParamArray args() As Object) Implements IUpgradeLog.WriteInformation
        _messages.Add("INFO: " & String.Format(format, args))
    End Sub
End Class
