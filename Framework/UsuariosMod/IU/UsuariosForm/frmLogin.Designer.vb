<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
    Inherits MotorIU.FormulariosP.FormularioBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNick = New ControlesPBase.txtValidable
        Me.txtClave = New ControlesPBase.txtValidable
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.cmdCancelar = New System.Windows.Forms.Button
        Me.cmd_Aceptar = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(21, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Clave"
        '
        'txtNick
        '
        Me.txtNick.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNick.Location = New System.Drawing.Point(73, 11)
        Me.txtNick.MensajeErrorValidacion = Nothing
        Me.txtNick.Name = "txtNick"
        Me.txtNick.Size = New System.Drawing.Size(169, 20)
        Me.txtNick.TabIndex = 0
        Me.txtNick.ToolTipText = Nothing
        Me.txtNick.TrimText = False
        '
        'txtClave
        '
        Me.txtClave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClave.Location = New System.Drawing.Point(73, 37)
        Me.txtClave.MensajeErrorValidacion = Nothing
        Me.txtClave.Name = "txtClave"
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClave.Size = New System.Drawing.Size(169, 20)
        Me.txtClave.TabIndex = 1
        Me.txtClave.ToolTipText = Nothing
        Me.txtClave.TrimText = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Framework.Usuarios.IUWin.Form.My.Resources.Resources.password2
        Me.PictureBox1.Location = New System.Drawing.Point(275, 11)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(48, 46)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Image = Global.Framework.Usuarios.IUWin.Form.My.Resources.Resources.button_cancel
        Me.cmdCancelar.Location = New System.Drawing.Point(236, 78)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(87, 27)
        Me.cmdCancelar.TabIndex = 3
        Me.cmdCancelar.Text = "Cancelar"
        Me.cmdCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.cmdCancelar.UseVisualStyleBackColor = True
        '
        'cmd_Aceptar
        '
        Me.cmd_Aceptar.Image = Global.Framework.Usuarios.IUWin.Form.My.Resources.Resources.apply
        Me.cmd_Aceptar.Location = New System.Drawing.Point(145, 78)
        Me.cmd_Aceptar.Name = "cmd_Aceptar"
        Me.cmd_Aceptar.Size = New System.Drawing.Size(85, 27)
        Me.cmd_Aceptar.TabIndex = 2
        Me.cmd_Aceptar.Text = "Aceptar"
        Me.cmd_Aceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.cmd_Aceptar.UseVisualStyleBackColor = True
        '
        'frmLogin
        '
        Me.AcceptButton = Me.cmd_Aceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(351, 117)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.txtClave)
        Me.Controls.Add(Me.cmd_Aceptar)
        Me.Controls.Add(Me.txtNick)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login de usuario"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNick As ControlesPBase.txtValidable
    Friend WithEvents cmd_Aceptar As Windows.Forms.Button
    Friend WithEvents txtClave As ControlesPBase.txtValidable
    Friend WithEvents cmdCancelar As Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
