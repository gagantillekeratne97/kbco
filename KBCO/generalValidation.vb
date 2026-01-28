Imports System.Text.RegularExpressions
Public Class generalValidation
    Public ErrObj As New ErrorProvider
    '//Checks whether a value exists or not
    Public Function isPresent(ByVal sender As TextBox) As Boolean
        isPresent = False
        If Trim(sender.Text) = "" Then
            isPresent = False
            ' MessageBox.Show("Please fill the required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrObj.SetError(sender, "Please fill the required field.")
            sender.Focus()
        Else
            ErrObj.Clear()
            isPresent = True
        End If
    End Function

    '//Check Combo is null or not
    Public Function iscomboPresent(ByVal sender As ComboBox) As Boolean
        If sender.Text = "" Then
            iscomboPresent = False
            'MessageBox.Show("Please select the required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrObj.SetError(sender, "Please fill the required field.")
            sender.Focus()
        Else
            ErrObj.Clear()
            iscomboPresent = True
        End If
    End Function

    Public Sub isNumericWithDecimals(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = "." Then
            If sender.Text.IndexOf(".") > -1 Then
                e.Handled = True
            End If
        ElseIf Char.IsNumber(e.KeyChar) = False AndAlso Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
        End If
    End Sub

    '//Check text length is equal
    Public Function isEqualTo(ByVal sender As Object, ByVal range As Integer)
        isEqualTo = False
        If Trim(sender.Text).Length < range Then
            ' sender.enable = True
            ' MessageBox.Show("Text length must be equal to " & range & ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrObj.SetError(sender, "Text length must be equal to " & range & ".")
            sender.Focus()
            Exit Function
        End If
        ErrObj.Clear()
        isEqualTo = True
        Return isEqualTo
    End Function

    '//check text for greater than
    Public Function isGreaterThan(ByVal sender As Object, ByVal minimum As Integer)
        If Trim(sender.text) >= minimum Then
            'MessageBox.Show("Text length must be greater than " & minimum & "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrObj.SetError(sender, "Text length must be greater than " & minimum & "")
            sender.Focus()
            isGreaterThan = False
        Else
            ErrObj.Clear()
            isGreaterThan = True
        End If
    End Function

    '//Check text length in range
    Public Function inRange(ByVal sender As Object, ByVal range As Integer)
        If Trim(sender.Text).Length < range Then
            'MessageBox.Show("Character length invalid, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrObj.SetError(sender, "Character length invalid, please try again.")
            sender.Focus()
            inRange = False
        Else
            ErrObj.Clear()
            inRange = True
        End If
    End Function

    '//check whether keypress isDigit
    Public Sub isDigit(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = CChar(ChrW(Keys.Back)) Then
            e.Handled = True
        End If
    End Sub

    '//check whether keypress isletter Or digit
    Public Sub isLetterOrDigit(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not e.KeyChar = CChar(ChrW(Keys.Back)) Then
            e.Handled = True
        End If
    End Sub

    '//Email validation
    Function IsEmail(ByVal email As TextBox) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim x As String = Trim(email.Text)
        Dim emailAddressMatch As Match = Regex.Match(x, pattern)
        If emailAddressMatch.Success Then
            ErrObj.Clear()
            IsEmail = True
        Else
            'MessageBox.Show("Invalid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ErrObj.SetError(email, "Invalid email address.")
            IsEmail = False
            email.Focus()
        End If
    End Function
End Class
