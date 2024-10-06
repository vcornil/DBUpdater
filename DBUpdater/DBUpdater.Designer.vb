﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DBUpdater
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DBUpdater))
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.txtLogin = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.cmbDatabases = New System.Windows.Forms.ComboBox()
        Me.lblDatabases = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.btnRunMigration = New System.Windows.Forms.Button()
        Me.txtMigrationOutput = New System.Windows.Forms.TextBox()
        Me.dgvDBVersion = New System.Windows.Forms.DataGridView()
        Me.btnSaveToRegistry = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtCurrentVersion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvDBVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(108, 12)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(452, 20)
        Me.txtServer.TabIndex = 0
        '
        'txtLogin
        '
        Me.txtLogin.Location = New System.Drawing.Point(108, 38)
        Me.txtLogin.Name = "txtLogin"
        Me.txtLogin.Size = New System.Drawing.Size(452, 20)
        Me.txtLogin.TabIndex = 1
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(108, 65)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(452, 20)
        Me.txtPassword.TabIndex = 2
        Me.txtPassword.TabStop = False
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(63, 15)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(41, 13)
        Me.lblServer.TabIndex = 3
        Me.lblServer.Text = "Server:"
        Me.lblServer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLogin
        '
        Me.lblLogin.AutoSize = True
        Me.lblLogin.Location = New System.Drawing.Point(65, 38)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(33, 13)
        Me.lblLogin.TabIndex = 4
        Me.lblLogin.Text = "Login"
        Me.lblLogin.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(51, 65)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 5
        Me.lblPassword.Text = "Password"
        Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(108, 91)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 6
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'cmbDatabases
        '
        Me.cmbDatabases.Enabled = False
        Me.cmbDatabases.FormattingEnabled = True
        Me.cmbDatabases.Location = New System.Drawing.Point(108, 150)
        Me.cmbDatabases.Name = "cmbDatabases"
        Me.cmbDatabases.Size = New System.Drawing.Size(452, 21)
        Me.cmbDatabases.TabIndex = 8
        '
        'lblDatabases
        '
        Me.lblDatabases.AutoSize = True
        Me.lblDatabases.Location = New System.Drawing.Point(46, 150)
        Me.lblDatabases.Name = "lblDatabases"
        Me.lblDatabases.Size = New System.Drawing.Size(58, 13)
        Me.lblDatabases.TabIndex = 9
        Me.lblDatabases.Text = "Databases"
        Me.lblDatabases.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnSelect
        '
        Me.btnSelect.Enabled = False
        Me.btnSelect.Location = New System.Drawing.Point(108, 178)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(75, 23)
        Me.btnSelect.TabIndex = 10
        Me.btnSelect.Text = "Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnRunMigration
        '
        Me.btnRunMigration.Enabled = False
        Me.btnRunMigration.Location = New System.Drawing.Point(108, 325)
        Me.btnRunMigration.Name = "btnRunMigration"
        Me.btnRunMigration.Size = New System.Drawing.Size(452, 23)
        Me.btnRunMigration.TabIndex = 12
        Me.btnRunMigration.Text = "Update database"
        Me.btnRunMigration.UseVisualStyleBackColor = True
        '
        'txtMigrationOutput
        '
        Me.txtMigrationOutput.Location = New System.Drawing.Point(108, 354)
        Me.txtMigrationOutput.Multiline = True
        Me.txtMigrationOutput.Name = "txtMigrationOutput"
        Me.txtMigrationOutput.ReadOnly = True
        Me.txtMigrationOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMigrationOutput.Size = New System.Drawing.Size(452, 221)
        Me.txtMigrationOutput.TabIndex = 13
        '
        'dgvDBVersion
        '
        Me.dgvDBVersion.AllowUserToAddRows = False
        Me.dgvDBVersion.AllowUserToDeleteRows = False
        Me.dgvDBVersion.AllowUserToResizeRows = False
        Me.dgvDBVersion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDBVersion.Location = New System.Drawing.Point(108, 207)
        Me.dgvDBVersion.Name = "dgvDBVersion"
        Me.dgvDBVersion.ReadOnly = True
        Me.dgvDBVersion.Size = New System.Drawing.Size(452, 71)
        Me.dgvDBVersion.TabIndex = 14
        '
        'btnSaveToRegistry
        '
        Me.btnSaveToRegistry.Enabled = False
        Me.btnSaveToRegistry.Location = New System.Drawing.Point(455, 178)
        Me.btnSaveToRegistry.Name = "btnSaveToRegistry"
        Me.btnSaveToRegistry.Size = New System.Drawing.Size(105, 23)
        Me.btnSaveToRegistry.TabIndex = 15
        Me.btnSaveToRegistry.Text = "Update Registry"
        Me.btnSaveToRegistry.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(485, 589)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 16
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtCurrentVersion
        '
        Me.txtCurrentVersion.Enabled = False
        Me.txtCurrentVersion.Location = New System.Drawing.Point(108, 285)
        Me.txtCurrentVersion.Name = "txtCurrentVersion"
        Me.txtCurrentVersion.Size = New System.Drawing.Size(100, 20)
        Me.txtCurrentVersion.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 285)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Current version"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DBUpdater
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 624)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCurrentVersion)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSaveToRegistry)
        Me.Controls.Add(Me.dgvDBVersion)
        Me.Controls.Add(Me.txtMigrationOutput)
        Me.Controls.Add(Me.btnRunMigration)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.lblDatabases)
        Me.Controls.Add(Me.cmbDatabases)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblLogin)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtLogin)
        Me.Controls.Add(Me.txtServer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DBUpdater"
        Me.Text = "DB Updater"
        CType(Me.dgvDBVersion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtServer As TextBox
    Friend WithEvents txtLogin As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblServer As Label
    Friend WithEvents lblLogin As Label
    Friend WithEvents lblPassword As Label
    Friend WithEvents btnConnect As Button
    Friend WithEvents cmbDatabases As ComboBox
    Friend WithEvents lblDatabases As Label
    Friend WithEvents btnSelect As Button
    Friend WithEvents btnRunMigration As Button
    Friend WithEvents txtMigrationOutput As TextBox
    Friend WithEvents dgvDBVersion As DataGridView
    Friend WithEvents btnSaveToRegistry As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents txtCurrentVersion As TextBox
    Friend WithEvents Label1 As Label
End Class