<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.lblAcknowledgment = New System.Windows.Forms.Label()
        Me.dgvScripts = New System.Windows.Forms.DataGridView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        CType(Me.dgvScripts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblAcknowledgment
        '
        Me.lblAcknowledgment.AutoSize = True
        Me.lblAcknowledgment.Location = New System.Drawing.Point(12, 75)
        Me.lblAcknowledgment.Name = "lblAcknowledgment"
        Me.lblAcknowledgment.Size = New System.Drawing.Size(95, 13)
        Me.lblAcknowledgment.TabIndex = 0
        Me.lblAcknowledgment.Text = "Acknowledgement"
        '
        'dgvScripts
        '
        Me.dgvScripts.AllowUserToAddRows = False
        Me.dgvScripts.AllowUserToDeleteRows = False
        Me.dgvScripts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvScripts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvScripts.Location = New System.Drawing.Point(15, 135)
        Me.dgvScripts.Name = "dgvScripts"
        Me.dgvScripts.ReadOnly = True
        Me.dgvScripts.Size = New System.Drawing.Size(460, 235)
        Me.dgvScripts.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(399, 415)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblProjectName
        '
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Font = New System.Drawing.Font("Arial Rounded MT Bold", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProjectName.Location = New System.Drawing.Point(9, 17)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(169, 32)
        Me.lblProjectName.TabIndex = 2
        Me.lblProjectName.Text = "DB Updater"
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(12, 49)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(107, 15)
        Me.lblCopyright.TabIndex = 3
        Me.lblCopyright.Text = "©    CoV Consult - 2024"
        '
        'AboutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 450)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.lblProjectName)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.dgvScripts)
        Me.Controls.Add(Me.lblAcknowledgment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.Text = "About"
        CType(Me.dgvScripts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblAcknowledgment As Label
    Friend WithEvents dgvScripts As DataGridView
    Friend WithEvents btnClose As Button
    Friend WithEvents lblProjectName As Label
    Friend WithEvents lblCopyright As Label
End Class
