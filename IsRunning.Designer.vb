<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class isrunning
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(isrunning))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIconx = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 5000
        '
        'NotifyIconx
        '
        Me.NotifyIconx.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIconx.BalloonTipText = "Verifica app ogni 10 secondi."
        Me.NotifyIconx.BalloonTipTitle = "Is Running"
        Me.NotifyIconx.Icon = CType(resources.GetObject("NotifyIconx.Icon"), System.Drawing.Icon)
        Me.NotifyIconx.Text = "IsRunning"
        Me.NotifyIconx.Visible = True
        '
        'isrunning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 187)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(-1000, -1000)
        Me.Name = "isrunning"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IsRunning"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents NotifyIconx As NotifyIcon
End Class
