Imports System.Windows.Forms

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button_Sleep10000_Click(sender As System.Object, e As System.EventArgs) Handles Button_Sleep10000.Click
        Threading.Thread.Sleep(10000)
    End Sub

    Private Sub Button_NG_Click(sender As System.Object, e As System.EventArgs) Handles Button_NG.Click
        Button_NG.BackColor = Color.Red
        Throw New Exception
    End Sub
End Class
