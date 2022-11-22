Imports System.Security.Principal
Imports Microsoft.VisualBasic.CompilerServices

Public Class isrunning
    Dim processi As List(Of String)
    Dim mostra As List(Of String)
    Dim solonome As List(Of String)


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.Visible = False
        Me.ShowInTaskbar = False
        Me.Left = -1000
        Me.Top = -1000

        NotifyIconx.Visible = True

        If IsUserAdmin() = True Then
            CreateShortCut4all()
        End If
    End Sub

    Private Function IsUserAdmin() As Boolean
        Dim isAdmin As Boolean
        Try
            Dim user As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim principal As WindowsPrincipal = New WindowsPrincipal(user)
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator)
        Catch ex As UnauthorizedAccessException
            isAdmin = False
            'MessageBox.Show(ex.Message)
        Catch ex As Exception
            isAdmin = False
            'MessageBox.Show(ex.Message)
        End Try

        Return isAdmin

    End Function
    Private Function CreateShortCut4all() As Boolean

        Dim TargetName As String = Application.ProductName
        Dim ShortCutPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup)
        Dim oShell As Object
        Dim oLink As Object
        'you don’t need to import anything in the project reference to create the Shell Object

        Try

            oShell = CreateObject("WScript.Shell")
            oLink = oShell.CreateShortcut(ShortCutPath & "\" & TargetName & ".lnk")
            'Environment.GetFolderPath(Environment.SpecialFolder.Startup)

            oLink.TargetPath = Application.ExecutablePath
            oLink.WindowStyle = 1
            oLink.Save()
            '''EseguiAlLogonToolStripMenuItem.Checked = True
        Catch ex As Exception
            Return False
        End Try


        Return True


    End Function
    Public Sub Runexe(exe As String, Optional Show As String = "hide", Optional arguments As String = "")

        Show = Show.Trim.ToLower

        Dim p As Process = New Process()
        Dim pi As ProcessStartInfo = New ProcessStartInfo()
        pi.Arguments = arguments
        pi.FileName = exe

        Select Case Show
            Case "hide"
                pi.CreateNoWindow = True
                pi.WindowStyle = ProcessWindowStyle.Hidden

            Case "show"
                pi.CreateNoWindow = False
                pi.WindowStyle = ProcessWindowStyle.Normal
                caricaprocessi()

            Case "full"
                pi.CreateNoWindow = False
                pi.WindowStyle = ProcessWindowStyle.Maximized
        End Select


        p.StartInfo = pi
        p.Start()
        ' If WaitForProcessComplete Then Do Until p.HasExited : Loop
    End Sub

    Sub caricaprocessi()
        processi.Clear()
        mostra.Clear()
        solonome.Clear()

        Dim l As String = ""
        Dim r As String = IO.Path.Combine(Application.StartupPath, "processi.ini")
        If IO.File.Exists(r) = False Then
            Exit Sub
        End If
        Using reader As New IO.StreamReader(r)

            While reader.Peek <> -1
                Dim s As String = reader.ReadLine.Trim

                If s.StartsWith("#") Or s.StartsWith("'") Or s.Length < 5 Then
                    Continue While
                End If

                If s.Contains(",") = False Then
                    s = s & ",hide"
                End If

                Dim ss As String = Mid(s, 1, s.LastIndexOf(","))
                processi.Add(ss)
                ss = Mid(ss, ss.LastIndexOf("\") + 2, ss.Length)
                ss = Mid(ss, 1, ss.LastIndexOf("."))
                solonome.Add(ss)

                mostra.Add(Mid(s, s.LastIndexOf(",") + 2, s.Length))
            End While
        End Using
    End Sub

    Dim primavolta As Boolean = False
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        processi = New List(Of String)
        mostra = New List(Of String)
        solonome = New List(Of String)
        caricaprocessi()

        If primavolta = False Then
            NotifyIconx.BalloonTipText = ""
        End If

        For x As Integer = 0 To processi.Count - 1
            Dim ce As Boolean = False
            If IO.File.Exists(processi(x)) = False Then
                Continue For
            End If

            For Each proc As Process In Process.GetProcesses
                Dim fileName As String = System.IO.Path.GetFileName(processi(x))
                Dim processes As New Process()
                Dim runningProcesses As Process() = Process.GetProcesses()

                If proc.ProcessName.ToLower = "mipgwebfirm" And TieniChiusoMipgWebFirmperFirmareConVecchioMipgToolStripMenuItem.Checked = True Then
                    killaexe()
                    ce = True
                    Exit For
                End If
                Dim CurrentSessionID As Integer = Process.GetCurrentProcess.SessionId
                If proc.SessionId = CurrentSessionID And proc.ProcessName.ToLower = fileName.Substring(0, fileName.LastIndexOf("."c)).ToLower Then
                    ce = True
                    Exit For
                End If
            Next
            If ce = False Then
                Runexe(processi(x), mostra(x))
            End If
            If primavolta = False Then
                Dim fileNam As String = System.IO.Path.GetFileName(processi(x))
                NotifyIconx.BalloonTipText = NotifyIconx.BalloonTipText & fileNam.Substring(0, fileNam.LastIndexOf("."c)).ToLower & vbCr
            End If

        Next
        primavolta = True
        Timer1.Enabled = True
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub NotifyIconx_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIconx.MouseDoubleClick
        If e.Button = MouseButtons.Left Then
            NotifyIconx.ShowBalloonTip(100)
        End If
    End Sub

    Private Sub NotifyIconx_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIconx.MouseClick
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip1.Show()
        End If
    End Sub

    Private Sub TieniChiusoMipgWebFirmperFirmareConVecchioMipgToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TieniChiusoMipgWebFirmperFirmareConVecchioMipgToolStripMenuItem.Click
        If TieniChiusoMipgWebFirmperFirmareConVecchioMipgToolStripMenuItem.Checked Then
            TieniChiusoMipgWebFirmperFirmareConVecchioMipgToolStripMenuItem.Checked = False
            Timer1_Tick(Nothing, Nothing)
        Else
            killaexe
            TieniChiusoMipgWebFirmperFirmareConVecchioMipgToolStripMenuItem.Checked = True
        End If
    End Sub

    Public Sub killaexe()

        For Each process As Process In Process.GetProcesses()
            Try
                If Operators.CompareString(process.ProcessName.ToLower(), "mipgwebfirm", False) = 0 Then
                    process.Kill()
                End If
            Catch ex As Exception

            End Try
        Next

    End Sub

End Class
