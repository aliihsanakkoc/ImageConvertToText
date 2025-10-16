Imports Microsoft.Win32
Imports Tesseract
Imports System.IO
Imports System.Text

Class MainWindow

    Public selectedLanguage As String

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        cbxLanguage.Items.Add("Türkçe")
        cbxLanguage.Items.Add("İngilizce")

        cbxLanguage.SelectedIndex = 0

    End Sub

    Private Async Sub btnSelectFile_Click(sender As Object, e As RoutedEventArgs)

        Dim dlg As New OpenFileDialog()

        Dim selectedLang As String = cbxLanguage.SelectedItem.ToString()
        If selectedLang = "Türkçe" Then selectedLanguage = "tur"
        If selectedLang = "İngilizce" Then selectedLanguage = "eng"

        dlg.Filter = "Desteklenen Dosyalar|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|Tüm Dosyalar|*.*"

        If dlg.ShowDialog() = True Then
            Dim filePath As String = dlg.FileName
            lblStatus.Content = "OCR işlemi başlatıldı..."
            txtOutput.Clear()

            Try
                Dim textResult As String = Await Task.Run(Function() RunOCR(filePath))
                txtOutput.Text = textResult
                lblStatus.Content = "Tamamlandı ✅"
            Catch ex As Exception
                MessageBox.Show("Hata: " & ex.Message)
                lblStatus.Content = "Hata ❌"
            End Try
        End If
    End Sub

    Private Function RunOCR(filePath As String) As String
        Dim tessDataPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata")
        Dim ext As String = Path.GetExtension(filePath).ToLower()
        Dim sb As New StringBuilder()

        Using engine As New TesseractEngine(tessDataPath, selectedLanguage, EngineMode.Default)
            Using img As Pix = Pix.LoadFromFile(filePath)
                Using page = engine.Process(img)
                    sb.AppendLine(page.GetText())
                End Using
            End Using
        End Using

        Return sb.ToString()
    End Function

End Class
