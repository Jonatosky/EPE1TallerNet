' Form2.vb
Imports ClosedXML.Excel
Imports System.IO

Public Class Form2

    Private ReadOnly rutaExcel As String = "C:\Users\Jonathan\Downloads\Lista.xlsx"
    Private ReadOnly nombreHoja As String = "Listado de Productos"
    Private parentForm As Form1

    ' Constructor que recibe la referencia al Form1
    Public Sub New(parent As Form1)
        InitializeComponent()
        parentForm = parent
    End Sub

    ' Botón Guardar
    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim nombre As String = txtNombre.Text.Trim()
        Dim categoria As String = txtCategoria.Text.Trim()
        Dim cantidad As String = txtCantidad.Text.Trim()

        If String.IsNullOrWhiteSpace(nombre) OrElse String.IsNullOrWhiteSpace(categoria) Then
            MessageBox.Show("Por favor completa Nombre y Categoría.")
            Return
        End If

        Dim nuevoId As Integer = 1 ' valor por defecto

        Try
            ' Si el archivo no existe, crear uno con encabezados
            If Not File.Exists(rutaExcel) Then
                Using wbNew As New XLWorkbook()
                    Dim wsNew = wbNew.Worksheets.Add(nombreHoja)
                    wsNew.Cell(1, 1).Value = "IdProducto"
                    wsNew.Cell(1, 2).Value = "NombreProducto"
                    wsNew.Cell(1, 3).Value = "Categoria"
                    wsNew.Cell(1, 4).Value = "CantidadPor"
                    wbNew.SaveAs(rutaExcel)
                End Using
            End If

            Using wb As New XLWorkbook(rutaExcel)
                Dim ws As IXLWorksheet

                ' Obtener hoja o crear si no existe
                If wb.Worksheets.Contains(nombreHoja) Then
                    ws = wb.Worksheet(nombreHoja)
                Else
                    ws = wb.Worksheets.Add(nombreHoja)
                    ws.Cell(1, 1).Value = "IdProducto"
                    ws.Cell(1, 2).Value = "NombreProducto"
                    ws.Cell(1, 3).Value = "Categoria"
                    ws.Cell(1, 4).Value = "CantidadPor"
                End If

                ' Última fila usada
                Dim lastCell = ws.Column(1).LastCellUsed()
                Dim lastRow As Integer = If(lastCell IsNot Nothing, lastCell.Address.RowNumber, 1)

                ' Calcular nuevo Id
                Dim maxId As Integer = 0
                If lastRow >= 2 Then
                    For r As Integer = 2 To lastRow
                        Dim s As String = ws.Cell(r, 1).GetString().Trim()
                        Dim n As Integer
                        If Integer.TryParse(s, n) AndAlso n > maxId Then
                            maxId = n
                        End If
                    Next
                End If
                nuevoId = maxId + 1

                ' Nueva fila
                Dim filaNueva As Integer = If(lastRow < 2, 2, lastRow + 1)

                ' Insertar datos
                ws.Cell(filaNueva, 1).SetValue(nuevoId)
                ws.Cell(filaNueva, 2).SetValue(nombre)
                ws.Cell(filaNueva, 3).SetValue(categoria)
                ws.Cell(filaNueva, 4).SetValue(cantidad)

                ' Guardar
                wb.Save()
            End Using

            ' Mostrar Id creado en un MessageBox
            MessageBox.Show("Producto guardado correctamente. El Id asignado es: " & nuevoId.ToString(), "Producto Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Limpiar campos
            txtNombre.Clear()
            txtCategoria.Clear()
            txtCantidad.Clear()

        Catch ioEx As IOException
            MessageBox.Show("No se pudo guardar el archivo. ¿Está abierto en Excel?" & vbCrLf & ioEx.Message)
        Catch ex As Exception
            MessageBox.Show("Error al insertar el registro: " & ex.Message)
        End Try
    End Sub

    ' Botón Volver
    Private Sub BtnVolver_Click(sender As Object, e As EventArgs) Handles BtnVolver.Click
        ' Mostrar Form1
        If parentForm IsNot Nothing Then
            parentForm.Show()
            Try
                parentForm.CargarTodo() ' recarga automáticamente los datos
            Catch
            End Try
        End If
        Me.Close() ' cerrar Form2
    End Sub

    ' Manejar cierre de formulario con la X
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If parentForm IsNot Nothing AndAlso Not parentForm.Visible Then
            parentForm.Show()
            Try
                parentForm.CargarTodo()
            Catch
            End Try
        End If
    End Sub

End Class
