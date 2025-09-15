Imports ClosedXML.Excel
Imports System.IO

Public Class Form1

    Public rutaExcel As String = "C:\Users\Jonathan\Downloads\Lista.xlsx"
    Private ReadOnly nombreHoja As String = "Listado de Productos"

    ' Cargar productos (IDs) y categorías (únicas) desde Excel
    Public Sub CargarProductos()
        Try
            cmbProductos.Items.Clear()
            cmbCategorias.Items.Clear()

            If Not File.Exists(rutaExcel) Then Return

            Using wb As New XLWorkbook(rutaExcel)
                Dim ws As IXLWorksheet = wb.Worksheets _
                    .FirstOrDefault(Function(sh) sh.Name.Trim().Equals(nombreHoja.Trim(), StringComparison.OrdinalIgnoreCase))
                If ws Is Nothing Then
                    MessageBox.Show("No se encontró la hoja: " & nombreHoja)
                    Return
                End If

                Dim lastCell = ws.Column(1).LastCellUsed()
                Dim lastRow As Integer = If(lastCell IsNot Nothing, lastCell.Address.RowNumber, 1)

                Dim categorias As New HashSet(Of String)

                For r As Integer = 2 To lastRow
                    Dim idProd As String = ws.Cell(r, 1).GetString().Trim()
                    Dim categoria As String = ws.Cell(r, 4).GetString().Trim() ' columna 4 = Categoria

                    If Not String.IsNullOrEmpty(idProd) Then
                        cmbProductos.Items.Add(idProd)
                    End If

                    If Not String.IsNullOrEmpty(categoria) Then
                        categorias.Add(categoria)
                    End If
                Next

                ' Llenar ComboBox de categorías con valores únicos
                cmbCategorias.Items.Clear()
                For Each cat In categorias
                    cmbCategorias.Items.Add(cat)
                Next
            End Using

        Catch ex As Exception
            MessageBox.Show("Error al cargar productos: " & ex.Message)
        End Try
    End Sub

    ' Cuando el usuario selecciona un Id, mostrar nombre, stock y precio
    Private Sub cmbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductos.SelectedIndexChanged
        Dim id As String = cmbProductos.SelectedItem?.ToString()
        If String.IsNullOrEmpty(id) Then Return

        Try
            Using wb As New XLWorkbook(rutaExcel)
                Dim ws As IXLWorksheet = wb.Worksheets _
                    .FirstOrDefault(Function(sh) sh.Name.Trim().Equals(nombreHoja.Trim(), StringComparison.OrdinalIgnoreCase))
                If ws Is Nothing Then Return

                Dim lastCell = ws.Column(1).LastCellUsed()
                Dim lastRow As Integer = If(lastCell IsNot Nothing, lastCell.Address.RowNumber, 1)

                For r As Integer = 2 To lastRow
                    If ws.Cell(r, 1).GetString().Trim() = id Then
                        txtNombre.Text = ws.Cell(r, 2).GetString().Trim()   ' columna 2 = Nombre/Descripción
                        txtStock.Text = ws.Cell(r, 5).GetString().Trim()    ' columna 5 = Cantidad por unidad (stock)
                        txtPrecio.Text = ws.Cell(r, 6).GetString().Trim()   ' columna 6 = Precio por unidad
                        Exit For
                    End If
                Next
            End Using

        Catch ex As Exception
            MessageBox.Show("Error al mostrar datos: " & ex.Message)
        End Try
    End Sub

    ' Botón para seleccionar archivo Excel y cargar productos
    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Archivos Excel|*.xlsx;*.xls"
            ofd.Title = "Selecciona un archivo Excel"
            If ofd.ShowDialog() = DialogResult.OK Then
                rutaExcel = ofd.FileName
                CargarProductos()
            End If
        End Using
    End Sub

    ' Abrir Form2 para nuevo registro
    Private Sub BtnNuevoRegistro_Click(sender As Object, e As EventArgs) Handles BtnNuevoRegistro.Click
        Dim f2 As New Form2(Me)
        f2.Show()
        Me.Hide()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarProductos()
    End Sub

End Class
