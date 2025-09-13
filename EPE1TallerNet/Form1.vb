Imports System.Data.OleDb

Public Class Form1

    ' Clase para manejar productos
    Class Producto
        Public Property ID As String
        Public Property Nombre As String
        Public Property Descripcion As String
        Public Property Precio As Double
        Public Property Stock As Integer
        Public Overrides Function ToString() As String
            Return ID ' ComboBox mostrará el ID
        End Function
    End Class

    Dim productos As New List(Of Producto)

    ' Cargar productos desde Excel (.xls)
    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Try
            ' Ruta del archivo Excel
            Dim rutaExcel As String = "C:\Users\Jonathan\Downloads\Lista.xls"

            ' Conexión para Excel 97-2003 (.xls)
            Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & rutaExcel & ";Extended Properties='Excel 8.0;HDR=YES;'"

            Using conn As New OleDbConnection(connStr)
                conn.Open()
                ' Cambiar "Listado de Productos" por el nombre exacto de la hoja si es diferente
                Dim cmd As New OleDbCommand("SELECT * FROM [Listado de Productos$]", conn)
                Dim adapter As New OleDbDataAdapter(cmd)
                Dim dt As New DataTable
                adapter.Fill(dt)

                ' Limpiar lista y ComboBox
                productos.Clear()
                cmbProductos.Items.Clear()

                ' Recorrer filas y llenar lista de productos
                For Each fila As DataRow In dt.Rows
                    Dim p As New Producto With {
                        .ID = fila("ID").ToString(),
                        .Nombre = fila("Nombre").ToString(),
                        .Descripcion = fila("Descripcion").ToString(),
                        .Precio = Convert.ToDouble(fila("Precio")),
                        .Stock = Convert.ToInt32(fila("Stock"))
                    }
                    productos.Add(p)
                    cmbProductos.Items.Add(p) ' ComboBox muestra el ID
                Next
            End Using

            ' Bloquear botón después de cargar
            btnCargar.Enabled = False

        Catch ex As Exception
            MessageBox.Show("Error al cargar Excel: " & ex.Message)
        End Try
    End Sub

    ' Mostrar detalles al seleccionar un ID
    Private Sub cmbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductos.SelectedIndexChanged
        Dim seleccionado As Producto = CType(cmbProductos.SelectedItem, Producto)
        If seleccionado IsNot Nothing Then
            txtNombre.Text = seleccionado.Nombre
            txtDescripcion.Text = seleccionado.Descripcion
            txtPrecio.Text = seleccionado.Precio.ToString("C2")
            txtStock.Text = seleccionado.Stock.ToString()
        End If
    End Sub

    ' Configuración inicial del formulario
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' TextBox solo lectura
        txtNombre.ReadOnly = True
        txtDescripcion.ReadOnly = True
        txtPrecio.ReadOnly = True
        txtStock.ReadOnly = True
    End Sub

End Class
