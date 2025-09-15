<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        BtnGuardar = New Button()
        BtnVolver = New Button()
        txtNombre = New TextBox()
        txtCategoria = New TextBox()
        txtCantidad = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' BtnGuardar
        ' 
        BtnGuardar.Location = New Point(179, 199)
        BtnGuardar.Name = "BtnGuardar"
        BtnGuardar.Size = New Size(94, 29)
        BtnGuardar.TabIndex = 0
        BtnGuardar.Text = "Guardar"
        BtnGuardar.UseVisualStyleBackColor = True
        ' 
        ' BtnVolver
        ' 
        BtnVolver.Location = New Point(464, 199)
        BtnVolver.Name = "BtnVolver"
        BtnVolver.Size = New Size(94, 29)
        BtnVolver.TabIndex = 1
        BtnVolver.Text = "Volver"
        BtnVolver.UseVisualStyleBackColor = True
        ' 
        ' txtNombre
        ' 
        txtNombre.BorderStyle = BorderStyle.FixedSingle
        txtNombre.Location = New Point(121, 56)
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(125, 27)
        txtNombre.TabIndex = 2
        ' 
        ' txtCategoria
        ' 
        txtCategoria.BorderStyle = BorderStyle.FixedSingle
        txtCategoria.Location = New Point(296, 57)
        txtCategoria.Name = "txtCategoria"
        txtCategoria.Size = New Size(125, 27)
        txtCategoria.TabIndex = 3
        ' 
        ' txtCantidad
        ' 
        txtCantidad.BorderStyle = BorderStyle.FixedSingle
        txtCantidad.Location = New Point(472, 57)
        txtCantidad.Name = "txtCantidad"
        txtCantidad.Size = New Size(125, 27)
        txtCantidad.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(119, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(128, 20)
        Label1.TabIndex = 5
        Label1.Text = "Nombre Producto"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(296, 14)
        Label2.Name = "Label2"
        Label2.Size = New Size(74, 20)
        Label2.TabIndex = 6
        Label2.Text = "Categoria"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(474, 18)
        Label3.Name = "Label3"
        Label3.Size = New Size(69, 20)
        Label3.TabIndex = 7
        Label3.Text = "Cantidad"
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Highlight
        ClientSize = New Size(800, 450)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtCantidad)
        Controls.Add(txtCategoria)
        Controls.Add(txtNombre)
        Controls.Add(BtnVolver)
        Controls.Add(BtnGuardar)
        Name = "Form2"
        Text = "Agregar Productos"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BtnGuardar As Button
    Friend WithEvents BtnVolver As Button
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents txtCategoria As TextBox
    Friend WithEvents txtCantidad As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
