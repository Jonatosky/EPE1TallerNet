Imports ClosedXML.Excel
Imports System.Globalization
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1

    ' Clase Producto
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

    Private productos As New List(Of Producto)()

    ' Normaliza cadenas: quita acentos, espacios, guiones, pone todo en minúscula
    Private Function NormalizeHeader(s As String) As String
        If s Is Nothing Then Return ""
        s = s.Trim().ToLowerInvariant()
        ' Reemplazar caracteres no alfanuméricos por espacio
        s = Regex.Replace(s, "[^a-z0-9]", " ")
        ' Quitar tildes/diacríticos
        Dim normalized = s.Normalize(NormalizationForm.FormD)
        Dim sb As New StringBuilder()
        For Each ch As Char In normalized
            Dim uc = CharUnicodeInfo.GetUnicodeCategory(ch)
            If uc <> UnicodeCategory.NonSpacingMark Then
                sb.Append(ch)
            End If
        Next
        Dim noDiacritics = sb.ToString().Normalize(NormalizationForm.FormC)
        ' Unir espacios múltiples
        noDiacritics = Regex.Replace(noDiacritics, "\s+", " ").Trim()
        ' Remover espacios para facilitar matching (ej: "idproducto")
        noDiacritics = noDiacritics.Replace(" ", "")
        Return noDiacritics
    End Function

    ' Lista de posibles alias normalizados para cada campo esperado
    Private Function GetAliasDictionary() As Dictionary(Of String, List(Of String))
        Dim d As New Dictionary(Of String, List(Of String))()

        d("IdProducto") = New List(Of String) From {
            "idproducto", "id", "productid", "idproducto", "id_producto", "codigo", "codigoarticulo", "sku"
        }
        d("NombreProducto") = New List(Of String) From {
            "nombreproducto", "nombre", "productname", "product", "descripcion", "nombredelproducto"
        }
        d("Categoria") = New List(Of String) From {
            "categoria", "categoría", "category", "tipo", "grupo"
        }
        d("CantidadPorUnidad") = New List(Of String) From {
            "cantidadporunidad", "cantidad", "unidad", "cantidad_por_unidad", "cantidadxunidad"
        }
        d("PrecioUnidad") = New List(Of String) From {
            "preciounidad", "precio", "unitprice", "precio_unitario", "preciounidad"
        }
        d("Proveedor") = New List(Of String) From {
            "proveedor", "supplier", "vendor", "empresa"
        }

        ' Normalizamos alias también (quita tildes y espacios)
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

    ' Buscar correspondencia entre encabezados del archivo y campos esperados
    Private Function MapHeaders(headers As List(Of String)) As Dictionary(Of String, Integer)
        Dim map As New Dictionary(Of String, Integer)()
        Dim aliases = GetAliasDictionary()

        ' Normalizar headers del excel y mantener índice original (1-based columnas)
        Dim normHeaders As New Dictionary(Of String, Integer)
        For i = 0 To headers.Count - 1
            Dim h = headers(i)
            Dim nh = NormalizeHeader(h)
            If Not normHeaders.ContainsKey(nh) Then
                normHeaders(nh) = i + 1 ' columnas 1-based
            End If
        Next

        ' Para cada campo esperado, buscar el header que coincida (por alias o que contenga alias)
        For Each expected In aliases.Keys
            Dim found As Integer = -1
            For Each aliasNorm In aliases(expected)
                If normHeaders.ContainsKey(aliasNorm) Then
                    found = normHeaders(aliasNorm)
                    Exit For
                End If
            Next

            ' Si no encontró exacto, intentar contains (por ejemplo header "id producto" -> "idproducto")
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

            If found <> -1 Then
                map(expected) = found
            End If
        Next

        Return map ' keys: "IdProducto","NombreProducto",... values: columna (1-based)
    End Function

    ' Botón cargar: abre archivo y detecta columnas robustamente
    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Try
            Dim ofd As New OpenFileDialog()
            ofd.Filter = "Excel files|*.xlsx;*.xls"
            If ofd.ShowDialog() <> DialogResult.OK Then Return
            Dim ruta As String = ofd.FileName

            Dim wb = New XLWorkbook(ruta)
            ' Elegimos la primera hoja visible
            Dim ws = wb.Worksheets.FirstOrDefault(Function(s) s.Visibility = XLWorksheetVisibility.Visible)
            If ws Is Nothing Then
                MessageBox.Show("No se encontró ninguna hoja visible en el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Obtener la fila del header (asumimos fila 1 como predeterminada).
            ' Si detectas que tus headers están en otra fila, cambia thisHeaderRow a la fila correcta.
            Dim headerRowNumber As Integer = 1
            Dim headerRow = ws.Row(headerRowNumber)

            ' Encontrar última columna usada y construir lista de encabezados
            Dim lastCol = headerRow.LastCellUsed()?.Address.ColumnNumber
            If lastCol Is Nothing Then
                MessageBox.Show("No se detectaron encabezados en la fila 1 de la hoja '" & ws.Name & "'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim headers As New List(Of String)
            For c = 1 To lastCol
                headers.Add(headerRow.Cell(CInt(c)).GetString())
            Next

            ' Mapear encabezados
            Dim mapping = MapHeaders(headers)

            ' Comprobación mínima: IdProducto y NombreProducto son críticos
            If Not mapping.ContainsKey("IdProducto") OrElse Not mapping.ContainsKey("NombreProducto") Then
                ' Generar diagnóstico corto para ayudarte a corregir encabezados
                Dim sb As New StringBuilder()
                sb.AppendLine("No se detectaron columnas críticas (IdProducto y/o NombreProducto).")
                sb.AppendLine("Encabezados detectados (normalizados -> columna):")
                For Each h In headers.Select(Function(x, i) New With {Key .Header = x, .Index = i + 1})
                    sb.AppendLine($"  {NormalizeHeader(h.Header)} -> Columna {h.Index} (raw: '{h.Header}')")
                Next
                sb.AppendLine()
                sb.AppendLine("Asegúrate de que la fila 1 contenga encabezados como: IdProducto, NombreProducto, Categoría, CantidadPorUnidad, PrecioUnidad, proveedor")
                MessageBox.Show(sb.ToString(), "Mapeo incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Limpiar UI y lista
            productos.Clear()
            cmbProductos.Items.Clear()
            txtNombre.Clear()
            txtDescripcion.Clear()
            txtPrecio.Clear()
            txtStock.Clear()

            ' Leer datos desde la fila siguiente al header hasta la última fila usada
            Dim lastRow = ws.LastRowUsed().RowNumber()
            For r = headerRowNumber + 1 To lastRow
                Dim row = ws.Row(r)
                ' Leer ID
                Dim idCol = mapping("IdProducto")
                Dim idVal = row.Cell(idCol).GetString().Trim()
                If String.IsNullOrWhiteSpace(idVal) Then
                    ' Omitir filas sin ID
                    Continue For
                End If

                Dim p As New Producto()
                p.ID = idVal

                If mapping.ContainsKey("NombreProducto") Then
                    p.NombreProducto = row.Cell(mapping("NombreProducto")).GetString().Trim()
                End If
                If mapping.ContainsKey("Categoria") Then
                    p.Categoria = row.Cell(mapping("Categoria")).GetString().Trim()
                End If
                If mapping.ContainsKey("CantidadPorUnidad") Then
                    p.CantidadPorUnidad = row.Cell(mapping("CantidadPorUnidad")).GetString().Trim()
                End If
                If mapping.ContainsKey("Proveedor") Then
                    p.Proveedor = row.Cell(mapping("Proveedor")).GetString().Trim()
                End If
                If mapping.ContainsKey("PrecioUnidad") Then
                    Dim rawPrecio = row.Cell(mapping("PrecioUnidad")).GetString().Trim()
                    Dim precio As Double = 0
                    ' Intentar parsear usando InvariantCulture y la cultura actual
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
                MessageBox.Show("No se encontró ninguna fila con IdProducto válido. Revisa que la columna IdProducto exista y que las filas tengan valores.", "Sin registros", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            MessageBox.Show("Carga exitosa: " & productos.Count.ToString() & " productos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnCargar.Enabled = False

        Catch ex As Exception
            MessageBox.Show("Error al leer el archivo Excel: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Cuando el usuario selecciona un ID en el ComboBox
    Private Sub cmbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductos.SelectedIndexChanged
        Dim sel = TryCast(cmbProductos.SelectedItem, Producto)
        If sel Is Nothing Then Return

        txtNombre.Text = sel.NombreProducto
        ' Concatenar categoría y proveedor para la descripción
        Dim parts As New List(Of String)
        If Not String.IsNullOrWhiteSpace(sel.Categoria) Then parts.Add(sel.Categoria)
        If Not String.IsNullOrWhiteSpace(sel.Proveedor) Then parts.Add("Proveedor: " & sel.Proveedor)
        txtDescripcion.Text = If(parts.Count > 0, String.Join(" | ", parts), "")

        txtPrecio.Text = sel.PrecioUnidad.ToString("C2")
        txtStock.Text = sel.CantidadPorUnidad
    End Sub

    ' Configuración del formulario al cargar
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        txtNombre.ReadOnly = True
        txtDescripcion.ReadOnly = True
        txtPrecio.ReadOnly = True
        txtStock.ReadOnly = True
    End Sub

End Class
