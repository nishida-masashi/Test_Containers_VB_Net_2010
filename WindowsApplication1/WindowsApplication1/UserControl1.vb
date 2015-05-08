Public Class UserControl1

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Throw New Exception
    End Sub

    Dim i As Integer = 0
    Private Sub Button_NGQ_Click(sender As System.Object, e As System.EventArgs) Handles Button_NGQ.Click
        i += 1
        If i Mod 2 Then
            Throw New Exception
        End If
    End Sub

End Class
