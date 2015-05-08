Public Class DebugUserControl

#Region "Dim"
    Private _assembly As System.Reflection.Assembly
    Private _control As Control

    Const delimiter_str = "."

    ReadOnly Property LoadControl As Control
        Get
            Return _control
        End Get
    End Property

#End Region

#Region "Set"
    Sub SetTypesToComboBox(fileName As String)
        If String.IsNullOrEmpty(fileName) Then
            _assembly = System.Reflection.Assembly.Load(My.Resources.WindowsApplication1)
        Else
            _assembly = System.Reflection.Assembly.LoadFrom(OpenFileDialog1.FileName)
        End If

        If _assembly Is Nothing Then
            Return
        End If


        FormClear()

        For Each t As Type In _assembly.GetTypes
            Select Case True
                Case t.IsSubclassOf(GetType(Form))
                Case t.IsSubclassOf(GetType(Control))
                Case t.IsSubclassOf(GetType(UserControl))
                Case Else
                    Continue For
            End Select

            ComboBox1.Items.Add(t.FullName)
        Next
    End Sub

    Function MakeControl() As Control
        _control = _assembly.CreateInstance(ComboBox1.Text)

        Select Case True
            Case TypeOf _control Is Form
                Dim f As Form = _control
                f.TopLevel = False
        End Select

        Return _control
    End Function

    Sub FormClear()
        ComboBox1.Items.Clear()
        If _control IsNot Nothing Then
            _control.Hide()
            _control = Nothing
        End If
    End Sub
#End Region

#Region "Event"

    Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        SetTypesToComboBox(Nothing)
        ComboBox1.Text = "WindowsApplication1.Form1"
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Panel_Load.Controls.Clear()

        Dim control = MakeControl()

        Panel_Load.Controls.Add(control)
        control.Show()
    End Sub

    Private Sub Button_Load_Click(sender As System.Object, e As System.EventArgs) Handles Button_Load.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        SetTypesToComboBox(OpenFileDialog1.FileName)
    End Sub

    Private Sub Button_Check_Click(sender As System.Object, e As System.EventArgs) Handles Button_Check.Click
        Dim sw = Nothing
        If _control IsNot Nothing Then
            ErrorProvider1.Clear()
            sw = Stopwatch.StartNew
            CheckButton(_control, "")
            sw.stop()
        End If

        ShowMessage("Check" + If(sw IsNot Nothing, " : " + sw.ElapsedMilliseconds.ToString + " ms", ""))
    End Sub

#End Region

#Region "Sub"
    Sub CheckButton(c As Control, str As String)
        If c Is Nothing Then
            Return
        End If

        For Each in_c In c.Controls
            CheckButton(in_c, str + c.Name + delimiter_str)
        Next

        If TypeOf c Is Button Then
            Try
                PerformButton(c, str)
            Catch ex As Exception
                ErrorProvider1.SetError(c, ex.Message)
                LogMessage(str + c.Name + ": NG")
            End Try
        End If
    End Sub
    Function CheckCloseButton(_parent As Control, b As Button) As Boolean
        If TypeOf _parent Is Form Then
            Dim f As Form = _parent

            Select Case True
                Case f.CancelButton Is b
                    Return True
                Case f.AcceptButton Is b
                    Return True
            End Select

            Return False
        Else
            If _parent.Parent IsNot Nothing Then
                Return CheckCloseButton(_parent.Parent, b)
            Else
                Return False
            End If
        End If
    End Function
    Sub PerformButton(b As Button, str As String)
        If CheckCloseButton(b.Parent, b) Then
            LogMessage(str + b.Name + ": Unchecked")
            Return
        End If

        Dim sw = Stopwatch.StartNew
        b.PerformClick()
        sw.Stop()

        LogMessage(str + b.Name + ": " + sw.ElapsedMilliseconds.ToString + " ms")
    End Sub

    Sub ShowMessage(str As String)
        MessageBox.Show(str)
    End Sub
    Sub LogMessage(str As String)
        My.Application.Log.WriteEntry(str)
    End Sub
#End Region

    
End Class
