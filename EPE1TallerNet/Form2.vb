' Form2.vb
Imports ClosedXML.Excel
Imports System.IO

Public Class Form2

    Private ReadOnly nombreHoja As String = "Listado de Productos"
    Private parentForm As Form1
    Private rutaExcel As String

    Public Sub New(parent As Form1)
        InitializeComponent()
        parentForm = parent
        rutaExcel = parent.rutaExcel
    End Sub

    ' Botón Guardar
    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim nombre As String = txtNombre.Text.Trim()
        Dim categoria As String = txtCategoria.Text.Trim()
        Dim stock As String = txtCantidad.Text.Trim()
        Dim precio As String = txtPrecio.Text.Trim()

        If String.IsNullOrWhiteSpace(nombre) OrElse String.IsNullOrWhiteSpace(categoria) Then
            MessageBox.Show("Por favor completa Nombre y Categoría.")
            Return
        End If

        Dim nuevoId As Integer = 1

        Try
            ' Crear archivo si no existe (con encabezados hasta columna 6)
            If Not File.Exists(rutaExcel) Then
                Using wbNew As New XLWorkbook()
                    Dim wsNew = wbNew.Worksheets.Add(nombreHoja)
                    wsNew.Cell(1, 1).Value = "IdProducto"
                    wsNew.Cell(1, 2).Value = "NombreProducto"
                    wsNew.Cell(1, 3).Value = "Proveedor" ' dejamos encabezado por compatibilidad, no lo usamos
                    wsNew.Cell(1, 4).Value = "Categoria"
                    wsNew.Cell(1, 5).Value = "CantidadPorUnidad"
                    wsNew.Cell(1, 6).Value = "PrecioPorUnidad"
                    wbNew.SaveAs(rutaExcel)
                End Using
            End If

            Using wb As New XLWorkbook(rutaExcel)
                Dim ws As IXLWorksheet = wb.Worksheets _
                    .FirstOrDefault(Function(sh) sh.Name.Trim().Equals(nombreHoja.Trim(), StringComparison.OrdinalIgnoreCase))

                If ws Is Nothing Then
                    ws = wb.Worksheets.Add(nombreHoja)
                    ws.Cell(1, 1).Value = "IdProducto"
                    ws.Cell(1, 2).Value = "NombreProducto"
                    ws.Cell(1, 3).Value = "Proveedor"
                    ws.Cell(1, 4).Value = "Categoria"
                    ws.Cell(1, 5).Value = "CantidadPorUnidad"
                    ws.Cell(1, 6).Value = "PrecioPorUnidad"
                End If

                ' Última fila usada en Id columna
                Dim lastCell = ws.Column(1).LastCellUsed()
                Dim lastRow As Integer = If(lastCell IsNot Nothing, lastCell.Address.RowNumber, 1)
                Dim filaNueva As Integer = If(lastRow < 2, 2, lastRow + 1)

                ' Calcular nuevo Id
                Dim maxId As Integer = 0
                If lastRow >= 2 Then
                    For r As Integer = 2 To lastRow
                        Dim n As Integer
                        If Integer.TryParse(ws.Cell(r, 1).GetString().Trim(), n) AndAlso n > maxId Then maxId = n
                    Next
                End If
                nuevoId = maxId + 1

                ' Escribir en las columnas 1..6
                ws.Cell(filaNueva, 1).SetValue(nuevoId)
                ws.Cell(filaNueva, 2).SetValue(nombre)
                ws.Cell(filaNueva, 3).SetValue(String.Empty)  ' columna 3 = Proveedor (dejamos vacío)
                ws.Cell(filaNueva, 4).SetValue(categoria)
                ws.Cell(filaNueva, 5).SetValue(stock)
                ws.Cell(filaNueva, 6).SetValue(precio)

                wb.Save()
            End Using

            MessageBox.Show("Producto guardado. Id asignado: " & nuevoId.ToString())

            ' Actualizar lista y categorías en Form1 (leer desde Excel)
            parentForm.rutaExcel = rutaExcel
            parentForm.CargarProductos()

            ' Seleccionar automáticamente el Id recién creado en cmbProductos (si se añadió)
            Dim idx As Integer = parentForm.cmbProductos.Items.IndexOf(nuevoId.ToString())
            If idx >= 0 Then
                parentForm.cmbProductos.SelectedIndex = idx
            End If

            ' Limpiar campos
            txtNombre.Clear()
            txtCategoria.Clear()
            txtCantidad.Clear()
            txtPrecio.Clear()

        Catch ex As Exception
            MessageBox.Show("Error al guardar producto: " & ex.Message)
        End Try
    End Sub

    ' Volver a Form1
    Private Sub BtnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click
        If parentForm IsNot Nothing Then
            parentForm.Show()
        End If
        Me.Close()
    End Sub

End Class
