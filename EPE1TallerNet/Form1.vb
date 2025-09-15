Imports ClosedXML.Excel
Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    ' En Form1.vb

    Private Sub BtnNuevoRegistro_Click(sender As Object, e As EventArgs) Handles BtnNuevoRegistro.Click
        ' Abrir Form2 pasando referencia a este Form1
        Dim f2 As New Form2(Me)
        f2.Show()
        Me.Hide()
    End Sub

    ' Ejemplo de método público que ya deberías tener (puede que tu implementación sea distinta):
    Public Sub CargarTodo()
        ' Aquí va tu lógica actual para cargar desde Excel al DataGridView
        ' Por ejemplo con ClosedXML puedes leer y poblar un DataTable/BindingSource
    End Sub

    ' --- Clase Producto ---
    Class Producto
        Public Property ID As String
        Public Property NombreProducto As String
        Public Property Categoria As String
        Public Property CantidadPorUnidad As String
        Public Property PrecioUnidad As Double
        Public Property Proveedor As String
        Public Overrides Function ToString() As String
            Return ID
        End Function
    End Class

    ' Lista en memoria
    Private productos As New List(Of Producto)()

    ' ---------------- Utilidades de normalización ----------------
    Private Function NormalizeHeader(s As String) As String
        If s Is Nothing Then Return ""
        s = s.Trim().ToLowerInvariant()
        s = Regex.Replace(s, "[^a-z0-9áéíóúüñ]", " ") ' permitir acentos para procesarlos
        Dim normalized = s.Normalize(NormalizationForm.FormD)
        Dim sb As New StringBuilder()
        For Each ch As Char In normalized
            Dim uc = Globalization.CharUnicodeInfo.GetUnicodeCategory(ch)
            If uc <> Globalization.UnicodeCategory.NonSpacingMark Then
                sb.Append(ch)
            End If
        Next
        Dim noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC)
        noDiacritics = Regex.Replace(noDiacritics, "\s+", " ").Trim()
        noDiacritics = noDiacritics.Replace(" ", "")
        Return noDiacritics
    End Function

    Private Function GetAliasDictionary() As Dictionary(Of String, List(Of String))
        Dim d As New Dictionary(Of String, List(Of String))()
        d("IdProducto") = New List(Of String) From {"idproducto", "id", "productid", "id_producto", "codigo", "sku"}
        d("NombreProducto") = New List(Of String) From {"nombreproducto", "nombre", "productname", "descripcion", "product"}
        d("Categoria") = New List(Of String) From {"categoria", "categoría", "category", "tipo", "grupo"}
        d("CantidadPorUnidad") = New List(Of String) From {"cantidadporunidad", "cantidad", "unidad", "cantidadxunidad"}
        d("PrecioUnidad") = New List(Of String) From {"preciounidad", "precio", "unitprice", "precio_unitario"}
        d("Proveedor") = New List(Of String) From {"proveedor", "supplier", "vendor", "empresa"}
        ' Normalizar alias
        Dim dNorm As New Dictionary(Of String, List(Of String))()
        For Each kv In d
            Dim listNorm As New List(Of String)
            For Each a In kv.Value
                listNorm.Add(NormalizeHeader(a))
            Next
            dNorm(kv.Key) = listNorm.Distinct().ToList()
        Next
        Return dNorm
    End Function

    Private Function MapHeaders(headers As List(Of String)) As Dictionary(Of String, Integer)
        Dim map As New Dictionary(Of String, Integer)()
        Dim aliases = GetAliasDictionary()
        Dim normHeaders As New Dictionary(Of String, Integer)
        For i = 0 To headers.Count - 1
            Dim nh = NormalizeHeader(headers(i))
            If Not normHeaders.ContainsKey(nh) Then normHeaders(nh) = i + 1 ' 1-based
        Next

        For Each expected In aliases.Keys
            Dim found As Integer = -1
            For Each aliasNorm In aliases(expected)
                If normHeaders.ContainsKey(aliasNorm) Then
                    found = normHeaders(aliasNorm)
                    Exit For
                End If
            Next
            If found = -1 Then
                For Each nh In normHeaders.Keys
                    For Each aliasNorm In aliases(expected)
                        If nh.Contains(aliasNorm) OrElse aliasNorm.Contains(nh) Then
                            found = normHeaders(nh)
                            Exit For
                        End If
                    Next
                    If found <> -1 Then Exit For
                Next
            End If
            If found <> -1 Then map(expected) = found
        Next

        Return map
    End Function

    ' ------------------- Cargar archivo y detectar columnas -------------------
    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Try
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Archivos Excel|*.xlsx;*.xls"
            ofd.Title = "Selecciona el archivo Excel"
            If ofd.ShowDialog() <> DialogResult.OK Then Return
            Dim ruta As String = ofd.FileName

            ' Abrir libro con ClosedXML
            Using wb As New XLWorkbook(ruta)
                ' Elegir la primera hoja visible
                Dim ws = wb.Worksheets.FirstOrDefault(Function(s) s.Visibility = XLWorksheetVisibility.Visible)
                If ws Is Nothing Then
                    MessageBox.Show("No se encontró ninguna hoja visible en el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                ' Asumimos encabezados en fila 1, pero si no están allí puedes cambiar headerRowNumber
                Dim headerRowNumber As Integer = 1
                Dim headerRow = ws.Row(headerRowNumber)
                Dim lastColCell = headerRow.LastCellUsed()
                If lastColCell Is Nothing Then
                    MessageBox.Show("No se detectaron encabezados en la fila " & headerRowNumber.ToString() & " de la hoja '" & ws.Name & "'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim lastCol = lastColCell.Address.ColumnNumber
                Dim headers As New List(Of String)
                For c = 1 To lastCol
                    headers.Add(headerRow.Cell(c).GetString())
                Next

                Dim mapping = MapHeaders(headers)

                ' Validación mínima
                If Not mapping.ContainsKey("IdProducto") OrElse Not mapping.ContainsKey("NombreProducto") Then
                    Dim sb As New StringBuilder()
                    sb.AppendLine("No se detectaron columnas críticas (IdProducto y/o NombreProducto).")
                    sb.AppendLine("Encabezados detectados (normalizado -> columna):")
                    For i = 0 To headers.Count - 1
                        sb.AppendLine($"  {NormalizeHeader(headers(i))} -> Columna {i + 1} (raw: '{headers(i)}')")
                    Next
                    sb.AppendLine()
                    sb.AppendLine("Asegúrate de que la fila de encabezado contenga al menos IdProducto y NombreProducto (pueden variar levemente).")
                    MessageBox.Show(sb.ToString(), "Columnas faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Limpiar estructura previa y UI
                productos.Clear()
                cmbProductos.Items.Clear()
                cmbCategorias.Items.Clear()
                txtNombre.Clear()
                txtDescripcion.Clear()
                txtPrecio.Clear()
                txtStock.Clear()

                ' Leer filas desde headerRowNumber + 1 hasta última fila usada
                Dim lastRow = ws.LastRowUsed().RowNumber()
                For r = headerRowNumber + 1 To lastRow
                    Dim row = ws.Row(r)
                    Dim idVal = ""
                    If mapping.ContainsKey("IdProducto") Then idVal = row.Cell(mapping("IdProducto")).GetString().Trim()
                    If String.IsNullOrWhiteSpace(idVal) Then Continue For

                    Dim p As New Producto()
                    p.ID = idVal
                    If mapping.ContainsKey("NombreProducto") Then p.NombreProducto = row.Cell(mapping("NombreProducto")).GetString().Trim()
                    If mapping.ContainsKey("Categoria") Then p.Categoria = row.Cell(mapping("Categoria")).GetString().Trim()
                    If mapping.ContainsKey("CantidadPorUnidad") Then p.CantidadPorUnidad = row.Cell(mapping("CantidadPorUnidad")).GetString().Trim()
                    If mapping.ContainsKey("Proveedor") Then p.Proveedor = row.Cell(mapping("Proveedor")).GetString().Trim()

                    If mapping.ContainsKey("PrecioUnidad") Then
                        Dim rawPrecio = row.Cell(mapping("PrecioUnidad")).GetString().Trim()
                        Dim precio As Double = 0
                        If Not String.IsNullOrWhiteSpace(rawPrecio) Then
                            If Not Double.TryParse(rawPrecio, NumberStyles.Any, CultureInfo.CurrentCulture, precio) Then
                                Double.TryParse(rawPrecio, NumberStyles.Any, CultureInfo.InvariantCulture, precio)
                            End If
                        End If
                        p.PrecioUnidad = precio
                    End If

                    productos.Add(p)
                    cmbProductos.Items.Add(p)
                Next

                If productos.Count = 0 Then
                    MessageBox.Show("No se encontró ninguna fila con IdProducto válido. Revisa el archivo.", "Sin registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' Llenar categorías detectadas
                PopulateCategorias()

                MessageBox.Show("Carga exitosa: " & productos.Count.ToString() & " productos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnCargar.Enabled = False
            End Using

        Catch ex As Exception
            MessageBox.Show("Error al leer el archivo: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ------------------- Poblado de categorías -------------------
    Private Sub PopulateCategorias()
        cmbCategorias.Items.Clear()
        Dim categorias = productos.
                          Select(Function(p) If(String.IsNullOrWhiteSpace(p.Categoria), "(Sin categoría)", p.Categoria)).
                          Distinct(StringComparer.OrdinalIgnoreCase).
                          OrderBy(Function(x) x).
                          ToList()
        cmbCategorias.Items.Add("Todos")
        For Each c In categorias
            cmbCategorias.Items.Add(c)
        Next
        cmbCategorias.SelectedIndex = 0
    End Sub

    ' ------------------- Filtrar por categoría -------------------
    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
        Try
            If cmbCategorias.SelectedItem Is Nothing Then
                MessageBox.Show("Seleccione una categoría primero.", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim seleccionado = cmbCategorias.SelectedItem.ToString()
            Dim listaMostrar As IEnumerable(Of Producto)

            If seleccionado.Equals("Todos", StringComparison.OrdinalIgnoreCase) Then
                listaMostrar = productos
            Else
                listaMostrar = productos.Where(Function(p)
                                                   Dim cat = If(String.IsNullOrWhiteSpace(p.Categoria), "(Sin categoría)", p.Categoria)
                                                   Return String.Equals(cat, seleccionado, StringComparison.OrdinalIgnoreCase)
                                               End Function)
            End If

            cmbProductos.Items.Clear()
            For Each p In listaMostrar
                cmbProductos.Items.Add(p)
            Next

            If cmbProductos.Items.Count = 0 Then
                MessageBox.Show("No hay productos en la categoría seleccionada.", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error al aplicar filtro: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ------------------- Mostrar todos (restaurar) -------------------
    Private Sub btnMostrarTodos_Click(sender As Object, e As EventArgs) Handles btnMostrarTodos.Click
        ' Seleccionar "Todos" si existe
        For i = 0 To cmbCategorias.Items.Count - 1
            If String.Equals(cmbCategorias.Items(i).ToString(), "Todos", StringComparison.OrdinalIgnoreCase) Then
                cmbCategorias.SelectedIndex = i
                Exit For
            End If
        Next
        ' Repoblar productos
        cmbProductos.Items.Clear()
        For Each p In productos
            cmbProductos.Items.Add(p)
        Next
    End Sub

    ' ------------------- Al seleccionar producto mostrar detalles -------------------
    Private Sub cmbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductos.SelectedIndexChanged
        Dim sel = TryCast(cmbProductos.SelectedItem, Producto)
        If sel Is Nothing Then Return

        txtNombre.Text = sel.NombreProducto

        Dim parts As New List(Of String)
        If Not String.IsNullOrWhiteSpace(sel.Categoria) Then parts.Add(sel.Categoria)
        If Not String.IsNullOrWhiteSpace(sel.Proveedor) Then parts.Add("Proveedor: " & sel.Proveedor)
        txtDescripcion.Text = If(parts.Count > 0, String.Join(" | ", parts), "")

        txtPrecio.Text = sel.PrecioUnidad.ToString("C2")
        txtStock.Text = sel.CantidadPorUnidad
    End Sub

    ' ------------------- Form Load -------------------
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        txtNombre.ReadOnly = True
        txtDescripcion.ReadOnly = True
        txtPrecio.ReadOnly = True
        txtStock.ReadOnly = True
    End Sub

    ' (Opcional) aplicar filtro automáticamente al cambiar selección de categoría
    Private Sub cmbCategorias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCategorias.SelectedIndexChanged
        ' Si quieres que filtre automáticamente al cambiar la categoría, descomenta la siguiente línea:
        ' btnFiltrar.PerformClick()
    End Sub

End Class
