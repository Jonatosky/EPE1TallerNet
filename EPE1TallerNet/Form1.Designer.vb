<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim lblNombre As Label
        cmbProductos = New ComboBox()
        btnCargar = New Button()
        txtNombre = New TextBox()
        txtPrecio = New TextBox()
        txtDescripcion = New TextBox()
        txtStock = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        lblNombre = New Label()
        SuspendLayout()
        ' 
        ' lblNombre
        ' 
        lblNombre.AutoSize = True
        lblNombre.Location = New Point(368, 299)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New Size(36, 15)
        lblNombre.TabIndex = 9
        lblNombre.Text = "Stock"
        ' 
        ' cmbProductos
        ' 
        cmbProductos.FormattingEnabled = True
        cmbProductos.Location = New Point(203, 106)
        cmbProductos.Name = "cmbProductos"
        cmbProductos.Size = New Size(121, 23)
        cmbProductos.TabIndex = 0
        ' 
        ' btnCargar
        ' 
        btnCargar.BackColor = SystemColors.AppWorkspace
        btnCargar.Location = New Point(343, 106)
        btnCargar.Name = "btnCargar"
        btnCargar.Size = New Size(138, 23)
        btnCargar.TabIndex = 1
        btnCargar.Text = "Cargar Productos"
        btnCargar.UseVisualStyleBackColor = False
        ' 
        ' txtNombre
        ' 
        txtNombre.BackColor = SystemColors.ActiveCaption
        txtNombre.Location = New Point(33, 243)
        txtNombre.Name = "txtNombre"
        txtNombre.ReadOnly = True
        txtNombre.Size = New Size(282, 23)
        txtNombre.TabIndex = 2
        ' 
        ' txtPrecio
        ' 
        txtPrecio.BackColor = SystemColors.ActiveCaption
        txtPrecio.Location = New Point(366, 243)
        txtPrecio.Name = "txtPrecio"
        txtPrecio.ReadOnly = True
        txtPrecio.Size = New Size(100, 23)
        txtPrecio.TabIndex = 3
        ' 
        ' txtDescripcion
        ' 
        txtDescripcion.BackColor = SystemColors.ActiveCaption
        txtDescripcion.Location = New Point(33, 319)
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.ReadOnly = True
        txtDescripcion.Size = New Size(282, 23)
        txtDescripcion.TabIndex = 4
        ' 
        ' txtStock
        ' 
        txtStock.BackColor = SystemColors.ActiveCaption
        txtStock.Location = New Point(366, 319)
        txtStock.Name = "txtStock"
        txtStock.ReadOnly = True
        txtStock.Size = New Size(100, 23)
        txtStock.TabIndex = 5
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(33, 219)
        Label1.Name = "Label1"
        Label1.Size = New Size(103, 15)
        Label1.TabIndex = 6
        Label1.Text = "Nombre Producto"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(35, 296)
        Label2.Name = "Label2"
        Label2.Size = New Size(121, 15)
        Label2.TabIndex = 7
        Label2.Text = "Descripcion Producto"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(366, 219)
        Label3.Name = "Label3"
        Label3.Size = New Size(40, 15)
        Label3.TabIndex = 8
        Label3.Text = "Precio"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = SystemColors.ActiveCaption
        Label4.BorderStyle = BorderStyle.Fixed3D
        Label4.Font = New Font("Segoe UI", 20F)
        Label4.ImageAlign = ContentAlignment.MiddleLeft
        Label4.Location = New Point(30, 12)
        Label4.Name = "Label4"
        Label4.Size = New Size(274, 39)
        Label4.TabIndex = 10
        Label4.Text = "Sistema de Inventario"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Label4)
        Controls.Add(lblNombre)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtStock)
        Controls.Add(txtDescripcion)
        Controls.Add(txtPrecio)
        Controls.Add(txtNombre)
        Controls.Add(btnCargar)
        Controls.Add(cmbProductos)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        Name = "Form1"
        Text = "Epe1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents cmbProductos As ComboBox
    Friend WithEvents btnCargar As Button
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents txtPrecio As TextBox
    Friend WithEvents txtDescripcion As TextBox
    Friend WithEvents txtStock As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblNombre As Label
    Friend WithEvents Label4 As Label

End Class
