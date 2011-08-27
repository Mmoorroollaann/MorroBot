<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMorroBot
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdConnect = New System.Windows.Forms.Button()
        Me.cmdToggleOutput = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdDisconnect = New System.Windows.Forms.Button()
        Me.tabOutput = New System.Windows.Forms.TabControl()
        Me.tabOutputRaw = New System.Windows.Forms.TabPage()
        Me.lstOutputRaw = New System.Windows.Forms.ListBox()
        Me.tabOutputReceived = New System.Windows.Forms.TabPage()
        Me.lstOutputReceived = New System.Windows.Forms.ListBox()
        Me.tabOutputSent = New System.Windows.Forms.TabPage()
        Me.lstOutputSent = New System.Windows.Forms.ListBox()
        Me.tabOutputEvents = New System.Windows.Forms.TabPage()
        Me.lstOutputEvents = New System.Windows.Forms.ListBox()
        Me.tabOutputChat = New System.Windows.Forms.TabPage()
        Me.tblOutputChat = New System.Windows.Forms.TableLayoutPanel()
        Me.lstOutputChat = New System.Windows.Forms.ListBox()
        Me.txtChatInput = New System.Windows.Forms.TextBox()
        Me.tabOutputErrors = New System.Windows.Forms.TabPage()
        Me.lstOutputError = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.tabOutput.SuspendLayout()
        Me.tabOutputRaw.SuspendLayout()
        Me.tabOutputReceived.SuspendLayout()
        Me.tabOutputSent.SuspendLayout()
        Me.tabOutputEvents.SuspendLayout()
        Me.tabOutputChat.SuspendLayout()
        Me.tblOutputChat.SuspendLayout()
        Me.tabOutputErrors.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdConnect
        '
        Me.cmdConnect.Location = New System.Drawing.Point(5, 20)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(110, 30)
        Me.cmdConnect.TabIndex = 0
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'cmdToggleOutput
        '
        Me.cmdToggleOutput.Location = New System.Drawing.Point(5, 55)
        Me.cmdToggleOutput.Name = "cmdToggleOutput"
        Me.cmdToggleOutput.Size = New System.Drawing.Size(110, 30)
        Me.cmdToggleOutput.TabIndex = 2
        Me.cmdToggleOutput.Text = "Toggle output"
        Me.cmdToggleOutput.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.tabOutput, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(933, 334)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdDisconnect)
        Me.Panel1.Controls.Add(Me.cmdToggleOutput)
        Me.Panel1.Controls.Add(Me.cmdConnect)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(811, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(119, 328)
        Me.Panel1.TabIndex = 5
        '
        'cmdDisconnect
        '
        Me.cmdDisconnect.Location = New System.Drawing.Point(5, 90)
        Me.cmdDisconnect.Name = "cmdDisconnect"
        Me.cmdDisconnect.Size = New System.Drawing.Size(110, 30)
        Me.cmdDisconnect.TabIndex = 3
        Me.cmdDisconnect.Text = "Disconnect"
        Me.cmdDisconnect.UseVisualStyleBackColor = True
        '
        'tabOutput
        '
        Me.tabOutput.Controls.Add(Me.tabOutputRaw)
        Me.tabOutput.Controls.Add(Me.tabOutputReceived)
        Me.tabOutput.Controls.Add(Me.tabOutputSent)
        Me.tabOutput.Controls.Add(Me.tabOutputEvents)
        Me.tabOutput.Controls.Add(Me.tabOutputChat)
        Me.tabOutput.Controls.Add(Me.tabOutputErrors)
        Me.tabOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabOutput.Location = New System.Drawing.Point(3, 3)
        Me.tabOutput.Name = "tabOutput"
        Me.tabOutput.SelectedIndex = 0
        Me.tabOutput.Size = New System.Drawing.Size(802, 328)
        Me.tabOutput.TabIndex = 3
        '
        'tabOutputRaw
        '
        Me.tabOutputRaw.Controls.Add(Me.lstOutputRaw)
        Me.tabOutputRaw.Location = New System.Drawing.Point(4, 22)
        Me.tabOutputRaw.Name = "tabOutputRaw"
        Me.tabOutputRaw.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutputRaw.Size = New System.Drawing.Size(794, 302)
        Me.tabOutputRaw.TabIndex = 0
        Me.tabOutputRaw.Text = "Output - Raw"
        Me.tabOutputRaw.UseVisualStyleBackColor = True
        '
        'lstOutputRaw
        '
        Me.lstOutputRaw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOutputRaw.FormattingEnabled = True
        Me.lstOutputRaw.Location = New System.Drawing.Point(3, 3)
        Me.lstOutputRaw.Name = "lstOutputRaw"
        Me.lstOutputRaw.Size = New System.Drawing.Size(788, 296)
        Me.lstOutputRaw.TabIndex = 0
        '
        'tabOutputReceived
        '
        Me.tabOutputReceived.Controls.Add(Me.lstOutputReceived)
        Me.tabOutputReceived.Location = New System.Drawing.Point(4, 22)
        Me.tabOutputReceived.Name = "tabOutputReceived"
        Me.tabOutputReceived.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutputReceived.Size = New System.Drawing.Size(794, 302)
        Me.tabOutputReceived.TabIndex = 2
        Me.tabOutputReceived.Text = "Output - Received"
        Me.tabOutputReceived.UseVisualStyleBackColor = True
        '
        'lstOutputReceived
        '
        Me.lstOutputReceived.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOutputReceived.FormattingEnabled = True
        Me.lstOutputReceived.Location = New System.Drawing.Point(3, 3)
        Me.lstOutputReceived.Name = "lstOutputReceived"
        Me.lstOutputReceived.Size = New System.Drawing.Size(788, 296)
        Me.lstOutputReceived.TabIndex = 1
        '
        'tabOutputSent
        '
        Me.tabOutputSent.Controls.Add(Me.lstOutputSent)
        Me.tabOutputSent.Location = New System.Drawing.Point(4, 22)
        Me.tabOutputSent.Name = "tabOutputSent"
        Me.tabOutputSent.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutputSent.Size = New System.Drawing.Size(794, 302)
        Me.tabOutputSent.TabIndex = 3
        Me.tabOutputSent.Text = "Output - Sent"
        Me.tabOutputSent.UseVisualStyleBackColor = True
        '
        'lstOutputSent
        '
        Me.lstOutputSent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOutputSent.FormattingEnabled = True
        Me.lstOutputSent.Location = New System.Drawing.Point(3, 3)
        Me.lstOutputSent.Name = "lstOutputSent"
        Me.lstOutputSent.Size = New System.Drawing.Size(788, 296)
        Me.lstOutputSent.TabIndex = 1
        '
        'tabOutputEvents
        '
        Me.tabOutputEvents.Controls.Add(Me.lstOutputEvents)
        Me.tabOutputEvents.Location = New System.Drawing.Point(4, 22)
        Me.tabOutputEvents.Name = "tabOutputEvents"
        Me.tabOutputEvents.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutputEvents.Size = New System.Drawing.Size(794, 302)
        Me.tabOutputEvents.TabIndex = 1
        Me.tabOutputEvents.Text = "Output - Events"
        Me.tabOutputEvents.UseVisualStyleBackColor = True
        '
        'lstOutputEvents
        '
        Me.lstOutputEvents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOutputEvents.FormattingEnabled = True
        Me.lstOutputEvents.Location = New System.Drawing.Point(3, 3)
        Me.lstOutputEvents.Name = "lstOutputEvents"
        Me.lstOutputEvents.Size = New System.Drawing.Size(788, 296)
        Me.lstOutputEvents.TabIndex = 1
        '
        'tabOutputChat
        '
        Me.tabOutputChat.Controls.Add(Me.tblOutputChat)
        Me.tabOutputChat.Location = New System.Drawing.Point(4, 22)
        Me.tabOutputChat.Name = "tabOutputChat"
        Me.tabOutputChat.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutputChat.Size = New System.Drawing.Size(794, 302)
        Me.tabOutputChat.TabIndex = 5
        Me.tabOutputChat.Text = "Output - Chat"
        Me.tabOutputChat.UseVisualStyleBackColor = True
        '
        'tblOutputChat
        '
        Me.tblOutputChat.ColumnCount = 1
        Me.tblOutputChat.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblOutputChat.Controls.Add(Me.lstOutputChat, 0, 0)
        Me.tblOutputChat.Controls.Add(Me.txtChatInput, 0, 1)
        Me.tblOutputChat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblOutputChat.Location = New System.Drawing.Point(3, 3)
        Me.tblOutputChat.Name = "tblOutputChat"
        Me.tblOutputChat.RowCount = 2
        Me.tblOutputChat.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tblOutputChat.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tblOutputChat.Size = New System.Drawing.Size(788, 296)
        Me.tblOutputChat.TabIndex = 2
        '
        'lstOutputChat
        '
        Me.lstOutputChat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOutputChat.FormattingEnabled = True
        Me.lstOutputChat.Location = New System.Drawing.Point(3, 3)
        Me.lstOutputChat.Name = "lstOutputChat"
        Me.lstOutputChat.Size = New System.Drawing.Size(782, 260)
        Me.lstOutputChat.TabIndex = 1
        '
        'txtChatInput
        '
        Me.txtChatInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtChatInput.Location = New System.Drawing.Point(3, 269)
        Me.txtChatInput.Name = "txtChatInput"
        Me.txtChatInput.Size = New System.Drawing.Size(782, 20)
        Me.txtChatInput.TabIndex = 2
        '
        'tabOutputErrors
        '
        Me.tabOutputErrors.Controls.Add(Me.lstOutputError)
        Me.tabOutputErrors.Location = New System.Drawing.Point(4, 22)
        Me.tabOutputErrors.Name = "tabOutputErrors"
        Me.tabOutputErrors.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutputErrors.Size = New System.Drawing.Size(794, 302)
        Me.tabOutputErrors.TabIndex = 4
        Me.tabOutputErrors.Text = "Output - Errors"
        Me.tabOutputErrors.UseVisualStyleBackColor = True
        '
        'lstOutputError
        '
        Me.lstOutputError.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstOutputError.FormattingEnabled = True
        Me.lstOutputError.Location = New System.Drawing.Point(3, 3)
        Me.lstOutputError.Name = "lstOutputError"
        Me.lstOutputError.Size = New System.Drawing.Size(788, 296)
        Me.lstOutputError.TabIndex = 1
        '
        'frmMorroBot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(933, 334)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmMorroBot"
        Me.Text = "frmMorroBot"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.tabOutput.ResumeLayout(False)
        Me.tabOutputRaw.ResumeLayout(False)
        Me.tabOutputReceived.ResumeLayout(False)
        Me.tabOutputSent.ResumeLayout(False)
        Me.tabOutputEvents.ResumeLayout(False)
        Me.tabOutputChat.ResumeLayout(False)
        Me.tblOutputChat.ResumeLayout(False)
        Me.tblOutputChat.PerformLayout()
        Me.tabOutputErrors.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdConnect As System.Windows.Forms.Button
    Friend WithEvents cmdToggleOutput As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdDisconnect As System.Windows.Forms.Button
    Friend WithEvents tabOutput As System.Windows.Forms.TabControl
    Friend WithEvents tabOutputRaw As System.Windows.Forms.TabPage
    Friend WithEvents lstOutputRaw As System.Windows.Forms.ListBox
    Friend WithEvents tabOutputReceived As System.Windows.Forms.TabPage
    Friend WithEvents lstOutputReceived As System.Windows.Forms.ListBox
    Friend WithEvents tabOutputSent As System.Windows.Forms.TabPage
    Friend WithEvents lstOutputSent As System.Windows.Forms.ListBox
    Friend WithEvents tabOutputEvents As System.Windows.Forms.TabPage
    Friend WithEvents lstOutputEvents As System.Windows.Forms.ListBox
    Friend WithEvents tabOutputChat As System.Windows.Forms.TabPage
    Friend WithEvents tabOutputErrors As System.Windows.Forms.TabPage
    Friend WithEvents lstOutputError As System.Windows.Forms.ListBox
    Friend WithEvents lstOutputChat As System.Windows.Forms.ListBox
    Friend WithEvents tblOutputChat As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents txtChatInput As System.Windows.Forms.TextBox
End Class
