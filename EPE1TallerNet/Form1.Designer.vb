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
        cmbCategorias = New ComboBox()
        btnFiltrar = New Button()
        btnMostrarTodos = New Button()
        BtnNuevoRegistro = New Button()
        lblNombre = New Label()
        SuspendLayout()
        ' 
        ' lblNombre
        ' 
        lblNombre.AutoSize = True
        lblNombre.Location = New Point(421, 399)
        lblNombre.Name = "lblNombre"
        lblNombre.Size = New Size(45, 20)
        lblNombre.TabIndex = 9
        lblNombre.Text = "Stock"
        ' 
        ' cmbProductos
        ' 
        cmbProductos.FormattingEnabled = True
        cmbProductos.Location = New Point(232, 141)
        cmbProductos.Margin = New Padding(3, 4, 3, 4)
        cmbProductos.Name = "cmbProductos"
        cmbProductos.Size = New Size(138, 28)
        cmbProductos.TabIndex = 0
        ' 
        ' btnCargar
        ' 
        btnCargar.BackColor = SystemColors.AppWorkspace
        btnCargar.Location = New Point(392, 141)
        btnCargar.Margin = New Padding(3, 4, 3, 4)
        btnCargar.Name = "btnCargar"
        btnCargar.Size = New Size(158, 31)
        btnCargar.TabIndex = 1
        btnCargar.Text = "Cargar Productos"
        btnCargar.UseVisualStyleBackColor = False
        ' 
        ' txtNombre
        ' 
        txtNombre.BackColor = SystemColors.ActiveCaption
        txtNombre.Location = New Point(38, 324)
        txtNombre.Margin = New Padding(3, 4, 3, 4)
        txtNombre.Name = "txtNombre"
        txtNombre.ReadOnly = True
        txtNombre.Size = New Size(322, 27)
        txtNombre.TabIndex = 2
        ' 
        ' txtPrecio
        ' 
        txtPrecio.BackColor = SystemColors.ActiveCaption
        txtPrecio.Location = New Point(418, 324)
        txtPrecio.Margin = New Padding(3, 4, 3, 4)
        txtPrecio.Name = "txtPrecio"
        txtPrecio.ReadOnly = True
        txtPrecio.Size = New Size(114, 27)
        txtPrecio.TabIndex = 3
        ' 
        ' txtDescripcion
        ' 
        txtDescripcion.BackColor = SystemColors.ActiveCaption
        txtDescripcion.Location = New Point(38, 425)
        txtDescripcion.Margin = New Padding(3, 4, 3, 4)
        txtDescripcion.Name = "txtDescripcion"
        txtDescripcion.ReadOnly = True
        txtDescripcion.Size = New Size(322, 27)
        txtDescripcion.TabIndex = 4
        ' 
        ' txtStock
        ' 
        txtStock.BackColor = SystemColors.ActiveCaption
        txtStock.Location = New Point(418, 425)
        txtStock.Margin = New Padding(3, 4, 3, 4)
        txtStock.Name = "txtStock"
        txtStock.ReadOnly = True
        txtStock.Size = New Size(114, 27)
        txtStock.TabIndex = 5
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(38, 292)
        Label1.Name = "Label1"
        Label1.Size = New Size(128, 20)
        Label1.TabIndex = 6
        Label1.Text = "Nombre Producto"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(40, 395)
        Label2.Name = "Label2"
        Label2.Size = New Size(151, 20)
        Label2.TabIndex = 7
        Label2.Text = "Descripcion Producto"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(418, 292)
        Label3.Name = "Label3"
        Label3.Size = New Size(50, 20)
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
        Label4.Location = New Point(34, 16)
        Label4.Name = "Label4"
        Label4.Size = New Size(344, 48)
        Label4.TabIndex = 10
        Label4.Text = "Sistema de Inventario"
        ' 
        ' cmbCategorias
        ' 
        cmbCategorias.FormattingEnabled = True
        cmbCategorias.Location = New Point(598, 144)
        cmbCategorias.Margin = New Padding(3, 4, 3, 4)
        cmbCategorias.Name = "cmbCategorias"
        cmbCategorias.Size = New Size(138, 28)
        cmbCategorias.TabIndex = 11
        ' 
        ' btnFiltrar
        ' 
        btnFiltrar.BackColor = SystemColors.ButtonShadow
        btnFiltrar.Location = New Point(568, 91)
        btnFiltrar.Margin = New Padding(3, 4, 3, 4)
        btnFiltrar.Name = "btnFiltrar"
        btnFiltrar.Size = New Size(86, 31)
        btnFiltrar.TabIndex = 12
        btnFiltrar.Text = "Filtrar"
        btnFiltrar.UseVisualStyleBackColor = False
        ' 
        ' btnMostrarTodos
        ' 
        btnMostrarTodos.BackColor = SystemColors.ButtonShadow
        btnMostrarTodos.Location = New Point(673, 91)
        btnMostrarTodos.Margin = New Padding(3, 4, 3, 4)
        btnMostrarTodos.Name = "btnMostrarTodos"
        btnMostrarTodos.Size = New Size(111, 31)
        btnMostrarTodos.TabIndex = 13
        btnMostrarTodos.Text = "Mostrar Todos"
        btnMostrarTodos.UseVisualStyleBackColor = False
        ' 
        ' BtnNuevoRegistro
        ' 
        BtnNuevoRegistro.Location = New Point(685, 234)
        BtnNuevoRegistro.Name = "BtnNuevoRegistro"
        BtnNuevoRegistro.Size = New Size(145, 29)
        BtnNuevoRegistro.TabIndex = 14
        BtnNuevoRegistro.Text = "Nuevo Registro"
        BtnNuevoRegistro.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.HotTrack
        ClientSize = New Size(912, 600)
        Controls.Add(BtnNuevoRegistro)
        Controls.Add(btnMostrarTodos)
        Controls.Add(btnFiltrar)
        Controls.Add(cmbCategorias)
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
        Margin = New Padding(3, 4, 3, 4)
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
    Friend WithEvents cmbCategorias As ComboBox
    Friend WithEvents btnFiltrar As Button
    Friend WithEvents btnMostrarTodos As Button
    Friend WithEvents BtnNuevoRegistro As Button

End Class
