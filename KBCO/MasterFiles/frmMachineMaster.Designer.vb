<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMachineMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtMake = New System.Windows.Forms.TextBox()
        Me.txtMachineID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTonerPN = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDevPN = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDrumPN = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSagePN = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtP2PRTCode = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtLastUnitCost = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblTonerYield = New System.Windows.Forms.Label()
        Me.lblDevYield = New System.Windows.Forms.Label()
        Me.lblDrumYield = New System.Windows.Forms.Label()
        Me.txtAvQty = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(271, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "F2"
        '
        'txtModel
        '
        Me.txtModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtModel.Location = New System.Drawing.Point(108, 64)
        Me.txtModel.MaxLength = 50
        Me.txtModel.Name = "txtModel"
        Me.txtModel.Size = New System.Drawing.Size(563, 20)
        Me.txtModel.TabIndex = 2
        '
        'txtMake
        '
        Me.txtMake.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMake.Location = New System.Drawing.Point(108, 38)
        Me.txtMake.MaxLength = 50
        Me.txtMake.Name = "txtMake"
        Me.txtMake.Size = New System.Drawing.Size(563, 20)
        Me.txtMake.TabIndex = 1
        '
        'txtMachineID
        '
        Me.txtMachineID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMachineID.Location = New System.Drawing.Point(108, 12)
        Me.txtMachineID.MaxLength = 20
        Me.txtMachineID.Name = "txtMachineID"
        Me.txtMachineID.Size = New System.Drawing.Size(157, 20)
        Me.txtMachineID.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Model"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Make"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Machine ID"
        '
        'txtTonerPN
        '
        Me.txtTonerPN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTonerPN.Location = New System.Drawing.Point(108, 106)
        Me.txtTonerPN.MaxLength = 20
        Me.txtTonerPN.Name = "txtTonerPN"
        Me.txtTonerPN.Size = New System.Drawing.Size(157, 20)
        Me.txtTonerPN.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Toner PN"
        '
        'txtDevPN
        '
        Me.txtDevPN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDevPN.Location = New System.Drawing.Point(108, 132)
        Me.txtDevPN.MaxLength = 20
        Me.txtDevPN.Name = "txtDevPN"
        Me.txtDevPN.Size = New System.Drawing.Size(157, 20)
        Me.txtDevPN.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Developer PN"
        '
        'txtDrumPN
        '
        Me.txtDrumPN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDrumPN.Location = New System.Drawing.Point(108, 158)
        Me.txtDrumPN.MaxLength = 20
        Me.txtDrumPN.Name = "txtDrumPN"
        Me.txtDrumPN.Size = New System.Drawing.Size(157, 20)
        Me.txtDrumPN.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 162)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Drum PN"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label8.Location = New System.Drawing.Point(271, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "F2"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label9.Location = New System.Drawing.Point(271, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(19, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "F2"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label10.Location = New System.Drawing.Point(271, 162)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(19, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "F2"
        '
        'txtSagePN
        '
        Me.txtSagePN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSagePN.Location = New System.Drawing.Point(514, 106)
        Me.txtSagePN.MaxLength = 20
        Me.txtSagePN.Name = "txtSagePN"
        Me.txtSagePN.Size = New System.Drawing.Size(157, 20)
        Me.txtSagePN.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(435, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 13)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Sage PN"
        '
        'txtP2PRTCode
        '
        Me.txtP2PRTCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtP2PRTCode.Location = New System.Drawing.Point(514, 132)
        Me.txtP2PRTCode.MaxLength = 20
        Me.txtP2PRTCode.Name = "txtP2PRTCode"
        Me.txtP2PRTCode.Size = New System.Drawing.Size(157, 20)
        Me.txtP2PRTCode.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(435, 136)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(73, 13)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "P2P RT Code"
        '
        'txtLastUnitCost
        '
        Me.txtLastUnitCost.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLastUnitCost.Location = New System.Drawing.Point(514, 158)
        Me.txtLastUnitCost.MaxLength = 20
        Me.txtLastUnitCost.Name = "txtLastUnitCost"
        Me.txtLastUnitCost.Size = New System.Drawing.Size(157, 20)
        Me.txtLastUnitCost.TabIndex = 8
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(435, 162)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "Last Unit Cost"
        '
        'lblTonerYield
        '
        Me.lblTonerYield.AutoSize = True
        Me.lblTonerYield.ForeColor = System.Drawing.Color.Brown
        Me.lblTonerYield.Location = New System.Drawing.Point(332, 110)
        Me.lblTonerYield.Name = "lblTonerYield"
        Me.lblTonerYield.Size = New System.Drawing.Size(13, 13)
        Me.lblTonerYield.TabIndex = 25
        Me.lblTonerYield.Text = "0"
        '
        'lblDevYield
        '
        Me.lblDevYield.AutoSize = True
        Me.lblDevYield.ForeColor = System.Drawing.Color.Brown
        Me.lblDevYield.Location = New System.Drawing.Point(332, 136)
        Me.lblDevYield.Name = "lblDevYield"
        Me.lblDevYield.Size = New System.Drawing.Size(13, 13)
        Me.lblDevYield.TabIndex = 25
        Me.lblDevYield.Text = "0"
        '
        'lblDrumYield
        '
        Me.lblDrumYield.AutoSize = True
        Me.lblDrumYield.ForeColor = System.Drawing.Color.Brown
        Me.lblDrumYield.Location = New System.Drawing.Point(332, 162)
        Me.lblDrumYield.Name = "lblDrumYield"
        Me.lblDrumYield.Size = New System.Drawing.Size(13, 13)
        Me.lblDrumYield.TabIndex = 25
        Me.lblDrumYield.Text = "0"
        '
        'txtAvQty
        '
        Me.txtAvQty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAvQty.Location = New System.Drawing.Point(514, 184)
        Me.txtAvQty.MaxLength = 20
        Me.txtAvQty.Name = "txtAvQty"
        Me.txtAvQty.Size = New System.Drawing.Size(157, 20)
        Me.txtAvQty.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(435, 187)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(69, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Available Qty"
        '
        'frmMachineMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(698, 218)
        Me.Controls.Add(Me.txtAvQty)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lblDrumYield)
        Me.Controls.Add(Me.lblDevYield)
        Me.Controls.Add(Me.lblTonerYield)
        Me.Controls.Add(Me.txtLastUnitCost)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtP2PRTCode)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtSagePN)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtDrumPN)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtDevPN)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTonerPN)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtModel)
        Me.Controls.Add(Me.txtMake)
        Me.Controls.Add(Me.txtMachineID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "frmMachineMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Tag = "X00002"
        Me.Text = "Machine Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtModel As System.Windows.Forms.TextBox
    Friend WithEvents txtMake As System.Windows.Forms.TextBox
    Friend WithEvents txtMachineID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTonerPN As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDevPN As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDrumPN As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSagePN As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtP2PRTCode As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtLastUnitCost As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblTonerYield As System.Windows.Forms.Label
    Friend WithEvents lblDevYield As System.Windows.Forms.Label
    Friend WithEvents lblDrumYield As System.Windows.Forms.Label
    Friend WithEvents txtAvQty As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
End Class
