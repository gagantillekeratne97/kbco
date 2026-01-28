<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageSearchControllers
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
        Me.components = New System.ComponentModel.Container()
        Me.dgSearchControllersItems = New System.Windows.Forms.DataGridView()
        Me.FORM_TAG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CTRL_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CUSTOME_SQL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEFUALT_SEARCH_BY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RETURN_FIEALD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.COLUMN_SIZE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEFUALT_VIEW = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RESULT_PER_PAGE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ORDER_BY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DEFUALT_ORDER_BY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblFomTag = New System.Windows.Forms.Label()
        Me.lblcontrolName = New System.Windows.Forms.Label()
        Me.lblCustomSQL = New System.Windows.Forms.Label()
        Me.lblDefualtSearchBy = New System.Windows.Forms.Label()
        Me.lblReturnField = New System.Windows.Forms.Label()
        Me.lblColumnSize = New System.Windows.Forms.Label()
        Me.lblDefualtViewMode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblOrderBy = New System.Windows.Forms.Label()
        Me.txtFormTag = New System.Windows.Forms.TextBox()
        Me.txtControlName = New System.Windows.Forms.TextBox()
        Me.txtCustomSQL = New System.Windows.Forms.TextBox()
        Me.txtDefualtSearchBy = New System.Windows.Forms.TextBox()
        Me.txtDefualtViewMode = New System.Windows.Forms.TextBox()
        Me.txtReturnField = New System.Windows.Forms.TextBox()
        Me.txtOrderBy = New System.Windows.Forms.TextBox()
        Me.txtColumnSize = New System.Windows.Forms.TextBox()
        Me.txtDefualtOrderBy = New System.Windows.Forms.TextBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblResultPerPage = New System.Windows.Forms.Label()
        Me.txtResultPerPage = New System.Windows.Forms.TextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.dgSearchControllersItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgSearchControllersItems
        '
        Me.dgSearchControllersItems.AllowUserToAddRows = False
        Me.dgSearchControllersItems.AllowUserToDeleteRows = False
        Me.dgSearchControllersItems.AllowUserToOrderColumns = True
        Me.dgSearchControllersItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSearchControllersItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.FORM_TAG, Me.CTRL_NAME, Me.CUSTOME_SQL, Me.DEFUALT_SEARCH_BY, Me.RETURN_FIEALD, Me.COLUMN_SIZE, Me.DEFUALT_VIEW, Me.RESULT_PER_PAGE, Me.ORDER_BY, Me.DEFUALT_ORDER_BY})
        Me.dgSearchControllersItems.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgSearchControllersItems.Location = New System.Drawing.Point(0, 175)
        Me.dgSearchControllersItems.Name = "dgSearchControllersItems"
        Me.dgSearchControllersItems.Size = New System.Drawing.Size(1180, 330)
        Me.dgSearchControllersItems.TabIndex = 0
        '
        'FORM_TAG
        '
        Me.FORM_TAG.HeaderText = "FORM TAG"
        Me.FORM_TAG.Name = "FORM_TAG"
        '
        'CTRL_NAME
        '
        Me.CTRL_NAME.HeaderText = "CONTROL NAME"
        Me.CTRL_NAME.Name = "CTRL_NAME"
        Me.CTRL_NAME.Width = 120
        '
        'CUSTOME_SQL
        '
        Me.CUSTOME_SQL.HeaderText = "CUSTOM SQL"
        Me.CUSTOME_SQL.Name = "CUSTOME_SQL"
        Me.CUSTOME_SQL.Width = 200
        '
        'DEFUALT_SEARCH_BY
        '
        Me.DEFUALT_SEARCH_BY.HeaderText = "DEFUALT SEARCH BY"
        Me.DEFUALT_SEARCH_BY.Name = "DEFUALT_SEARCH_BY"
        '
        'RETURN_FIEALD
        '
        Me.RETURN_FIEALD.HeaderText = "RETURN FIELD"
        Me.RETURN_FIEALD.Name = "RETURN_FIEALD"
        '
        'COLUMN_SIZE
        '
        Me.COLUMN_SIZE.HeaderText = "COLUMN SIZE"
        Me.COLUMN_SIZE.Name = "COLUMN_SIZE"
        '
        'DEFUALT_VIEW
        '
        Me.DEFUALT_VIEW.HeaderText = "DEFUALT VIEW MODE"
        Me.DEFUALT_VIEW.Name = "DEFUALT_VIEW"
        '
        'RESULT_PER_PAGE
        '
        Me.RESULT_PER_PAGE.HeaderText = "RESULT PER PAGE"
        Me.RESULT_PER_PAGE.Name = "RESULT_PER_PAGE"
        '
        'ORDER_BY
        '
        Me.ORDER_BY.HeaderText = "ORDER BY"
        Me.ORDER_BY.Name = "ORDER_BY"
        '
        'DEFUALT_ORDER_BY
        '
        Me.DEFUALT_ORDER_BY.HeaderText = "DEFUALT ORDER BY"
        Me.DEFUALT_ORDER_BY.Name = "DEFUALT_ORDER_BY"
        '
        'lblFomTag
        '
        Me.lblFomTag.AutoSize = True
        Me.lblFomTag.Location = New System.Drawing.Point(13, 13)
        Me.lblFomTag.Name = "lblFomTag"
        Me.lblFomTag.Size = New System.Drawing.Size(48, 13)
        Me.lblFomTag.TabIndex = 1
        Me.lblFomTag.Text = "Form tag"
        '
        'lblcontrolName
        '
        Me.lblcontrolName.AutoSize = True
        Me.lblcontrolName.Location = New System.Drawing.Point(13, 39)
        Me.lblcontrolName.Name = "lblcontrolName"
        Me.lblcontrolName.Size = New System.Drawing.Size(71, 13)
        Me.lblcontrolName.TabIndex = 2
        Me.lblcontrolName.Text = "Control Name"
        '
        'lblCustomSQL
        '
        Me.lblCustomSQL.AutoSize = True
        Me.lblCustomSQL.Location = New System.Drawing.Point(12, 65)
        Me.lblCustomSQL.Name = "lblCustomSQL"
        Me.lblCustomSQL.Size = New System.Drawing.Size(66, 13)
        Me.lblCustomSQL.TabIndex = 3
        Me.lblCustomSQL.Text = "Custom SQL"
        '
        'lblDefualtSearchBy
        '
        Me.lblDefualtSearchBy.AutoSize = True
        Me.lblDefualtSearchBy.Location = New System.Drawing.Point(13, 91)
        Me.lblDefualtSearchBy.Name = "lblDefualtSearchBy"
        Me.lblDefualtSearchBy.Size = New System.Drawing.Size(92, 13)
        Me.lblDefualtSearchBy.TabIndex = 4
        Me.lblDefualtSearchBy.Text = "Defualt Search by"
        '
        'lblReturnField
        '
        Me.lblReturnField.AutoSize = True
        Me.lblReturnField.Location = New System.Drawing.Point(320, 91)
        Me.lblReturnField.Name = "lblReturnField"
        Me.lblReturnField.Size = New System.Drawing.Size(64, 13)
        Me.lblReturnField.TabIndex = 5
        Me.lblReturnField.Text = "Return Field"
        '
        'lblColumnSize
        '
        Me.lblColumnSize.AutoSize = True
        Me.lblColumnSize.Location = New System.Drawing.Point(546, 94)
        Me.lblColumnSize.Name = "lblColumnSize"
        Me.lblColumnSize.Size = New System.Drawing.Size(65, 13)
        Me.lblColumnSize.TabIndex = 6
        Me.lblColumnSize.Text = "Column Size"
        '
        'lblDefualtViewMode
        '
        Me.lblDefualtViewMode.AutoSize = True
        Me.lblDefualtViewMode.Location = New System.Drawing.Point(13, 117)
        Me.lblDefualtViewMode.Name = "lblDefualtViewMode"
        Me.lblDefualtViewMode.Size = New System.Drawing.Size(97, 13)
        Me.lblDefualtViewMode.TabIndex = 7
        Me.lblDefualtViewMode.Text = "Defualt View Mode"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(546, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Defualt order by"
        '
        'lblOrderBy
        '
        Me.lblOrderBy.AutoSize = True
        Me.lblOrderBy.Location = New System.Drawing.Point(320, 117)
        Me.lblOrderBy.Name = "lblOrderBy"
        Me.lblOrderBy.Size = New System.Drawing.Size(47, 13)
        Me.lblOrderBy.TabIndex = 9
        Me.lblOrderBy.Text = "Order by"
        '
        'txtFormTag
        '
        Me.txtFormTag.Location = New System.Drawing.Point(130, 10)
        Me.txtFormTag.Name = "txtFormTag"
        Me.txtFormTag.Size = New System.Drawing.Size(100, 20)
        Me.txtFormTag.TabIndex = 10
        '
        'txtControlName
        '
        Me.txtControlName.Location = New System.Drawing.Point(130, 36)
        Me.txtControlName.Name = "txtControlName"
        Me.txtControlName.Size = New System.Drawing.Size(369, 20)
        Me.txtControlName.TabIndex = 10
        '
        'txtCustomSQL
        '
        Me.txtCustomSQL.Location = New System.Drawing.Point(130, 62)
        Me.txtCustomSQL.Name = "txtCustomSQL"
        Me.txtCustomSQL.Size = New System.Drawing.Size(926, 20)
        Me.txtCustomSQL.TabIndex = 10
        '
        'txtDefualtSearchBy
        '
        Me.txtDefualtSearchBy.Location = New System.Drawing.Point(130, 88)
        Me.txtDefualtSearchBy.Name = "txtDefualtSearchBy"
        Me.txtDefualtSearchBy.Size = New System.Drawing.Size(100, 20)
        Me.txtDefualtSearchBy.TabIndex = 10
        Me.txtDefualtSearchBy.Text = "1"
        '
        'txtDefualtViewMode
        '
        Me.txtDefualtViewMode.Location = New System.Drawing.Point(130, 114)
        Me.txtDefualtViewMode.Name = "txtDefualtViewMode"
        Me.txtDefualtViewMode.Size = New System.Drawing.Size(100, 20)
        Me.txtDefualtViewMode.TabIndex = 10
        Me.txtDefualtViewMode.Text = "1"
        '
        'txtReturnField
        '
        Me.txtReturnField.Location = New System.Drawing.Point(422, 88)
        Me.txtReturnField.Name = "txtReturnField"
        Me.txtReturnField.Size = New System.Drawing.Size(100, 20)
        Me.txtReturnField.TabIndex = 10
        Me.txtReturnField.Text = "0"
        '
        'txtOrderBy
        '
        Me.txtOrderBy.Location = New System.Drawing.Point(422, 114)
        Me.txtOrderBy.Name = "txtOrderBy"
        Me.txtOrderBy.Size = New System.Drawing.Size(100, 20)
        Me.txtOrderBy.TabIndex = 10
        Me.txtOrderBy.Text = "ORDER BY CODE"
        '
        'txtColumnSize
        '
        Me.txtColumnSize.Location = New System.Drawing.Point(639, 91)
        Me.txtColumnSize.Name = "txtColumnSize"
        Me.txtColumnSize.Size = New System.Drawing.Size(221, 20)
        Me.txtColumnSize.TabIndex = 10
        '
        'txtDefualtOrderBy
        '
        Me.txtDefualtOrderBy.Location = New System.Drawing.Point(639, 117)
        Me.txtDefualtOrderBy.Name = "txtDefualtOrderBy"
        Me.txtDefualtOrderBy.Size = New System.Drawing.Size(221, 20)
        Me.txtDefualtOrderBy.TabIndex = 10
        Me.txtDefualtOrderBy.Text = "CODE"
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(262, 146)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 11
        Me.btnRemove.Text = "REMOVE"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(130, 146)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 11
        Me.btnAdd.Text = "ADD"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblResultPerPage
        '
        Me.lblResultPerPage.AutoSize = True
        Me.lblResultPerPage.Location = New System.Drawing.Point(866, 91)
        Me.lblResultPerPage.Name = "lblResultPerPage"
        Me.lblResultPerPage.Size = New System.Drawing.Size(84, 13)
        Me.lblResultPerPage.TabIndex = 12
        Me.lblResultPerPage.Text = "Result Per Page"
        '
        'txtResultPerPage
        '
        Me.txtResultPerPage.Location = New System.Drawing.Point(956, 88)
        Me.txtResultPerPage.Name = "txtResultPerPage"
        Me.txtResultPerPage.Size = New System.Drawing.Size(100, 20)
        Me.txtResultPerPage.TabIndex = 13
        Me.txtResultPerPage.Text = "100"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmManageSearchControllers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1180, 505)
        Me.Controls.Add(Me.txtResultPerPage)
        Me.Controls.Add(Me.lblResultPerPage)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.txtDefualtOrderBy)
        Me.Controls.Add(Me.txtColumnSize)
        Me.Controls.Add(Me.txtOrderBy)
        Me.Controls.Add(Me.txtReturnField)
        Me.Controls.Add(Me.txtDefualtViewMode)
        Me.Controls.Add(Me.txtDefualtSearchBy)
        Me.Controls.Add(Me.txtCustomSQL)
        Me.Controls.Add(Me.txtControlName)
        Me.Controls.Add(Me.txtFormTag)
        Me.Controls.Add(Me.lblOrderBy)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblDefualtViewMode)
        Me.Controls.Add(Me.lblColumnSize)
        Me.Controls.Add(Me.lblReturnField)
        Me.Controls.Add(Me.lblDefualtSearchBy)
        Me.Controls.Add(Me.lblCustomSQL)
        Me.Controls.Add(Me.lblcontrolName)
        Me.Controls.Add(Me.lblFomTag)
        Me.Controls.Add(Me.dgSearchControllersItems)
        Me.Name = "frmManageSearchControllers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Manage Search Controllers"
        CType(Me.dgSearchControllersItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgSearchControllersItems As System.Windows.Forms.DataGridView
    Friend WithEvents FORM_TAG As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CTRL_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CUSTOME_SQL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEFUALT_SEARCH_BY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RETURN_FIEALD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents COLUMN_SIZE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEFUALT_VIEW As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RESULT_PER_PAGE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ORDER_BY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DEFUALT_ORDER_BY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblFomTag As System.Windows.Forms.Label
    Friend WithEvents lblcontrolName As System.Windows.Forms.Label
    Friend WithEvents lblCustomSQL As System.Windows.Forms.Label
    Friend WithEvents lblDefualtSearchBy As System.Windows.Forms.Label
    Friend WithEvents lblReturnField As System.Windows.Forms.Label
    Friend WithEvents lblColumnSize As System.Windows.Forms.Label
    Friend WithEvents lblDefualtViewMode As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblOrderBy As System.Windows.Forms.Label
    Friend WithEvents txtFormTag As System.Windows.Forms.TextBox
    Friend WithEvents txtControlName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomSQL As System.Windows.Forms.TextBox
    Friend WithEvents txtDefualtSearchBy As System.Windows.Forms.TextBox
    Friend WithEvents txtDefualtViewMode As System.Windows.Forms.TextBox
    Friend WithEvents txtReturnField As System.Windows.Forms.TextBox
    Friend WithEvents txtOrderBy As System.Windows.Forms.TextBox
    Friend WithEvents txtColumnSize As System.Windows.Forms.TextBox
    Friend WithEvents txtDefualtOrderBy As System.Windows.Forms.TextBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblResultPerPage As System.Windows.Forms.Label
    Friend WithEvents txtResultPerPage As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
