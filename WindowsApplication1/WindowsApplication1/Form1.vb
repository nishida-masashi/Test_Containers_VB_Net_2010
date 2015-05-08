Public Class Form1

    Dim i As UInteger
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim temp As Integer = Integer.MaxValue
        temp += Integer.MaxValue

        i += 1
        Select Case i Mod 3
            Case 0
                Button1.BackColor = Color.Red
            Case 1
                Button1.BackColor = Color.Green
            Case 2
                Button1.BackColor = Color.Blue
        End Select
    End Sub

End Class
