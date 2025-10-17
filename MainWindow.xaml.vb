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
    Private Sub btnRunOCR_Click(sender As Object, e As RoutedEventArgs) Handles btnRunOCR.Click

        Select Case cbxLanguage.SelectedItem.ToString()
            Case "Türkçe"
                selectedLanguage = "tur"
            Case "İngilizce"
                selectedLanguage = "eng"
            Case Else
                selectedLanguage = "tur"
        End Select

        lblStatus.Content = "OCR işlemi başlatıldı..."
        txtOutput.Clear()

        If imageBox.Source IsNot Nothing Then
            txtOutput.Text = RunOCRFromImageSource(imageBox.Source)
            lblStatus.Content = "Tamamlandı ✅"
        Else
            lblStatus.Content = "Hata ❌"
        End If

    End Sub

    Private Function RunOCRFromImageSource(imageSource As ImageSource) As String
        ' OCR sonuçlarını toplamak için StringBuilder
        Dim sb As New StringBuilder()

        ' tessdata klasör yolu
        Dim tessDataPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata")

        ' Görseli ImageSource -> Bitmap -> Geçici dosya olarak kaydet (Pix doğrudan ImageSource'tan yüklenemiyor)
        Dim tempFile As String = Path.GetTempFileName()
        tempFile = Path.ChangeExtension(tempFile, ".png")

        Try
            ' ImageSource'u BitmapEncoder ile dosyaya kaydet
            Dim bitmapSource = TryCast(imageSource, BitmapSource)
            If bitmapSource Is Nothing Then
                Throw New Exception("Geçersiz görüntü formatı.")
            End If

            Dim encoder As New PngBitmapEncoder()
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource))
            Using fs As New FileStream(tempFile, FileMode.Create)
                encoder.Save(fs)
            End Using

            ' OCR işlemi
            Using engine As New TesseractEngine(tessDataPath, selectedLanguage, EngineMode.Default)
                Using img As Pix = Pix.LoadFromFile(tempFile)
                    Using page = engine.Process(img)
                        sb.AppendLine(page.GetText())
                    End Using
                End Using
            End Using

        Finally

            ' Geçici dosyayı sil
            If File.Exists(tempFile) Then
                File.Delete(tempFile)
            End If

        End Try

        Return sb.ToString()

    End Function
    Private Sub btnPasteClipboard_Click(sender As Object, e As RoutedEventArgs) Handles btnPasteClipboard.Click
        Try
            If Clipboard.ContainsImage() Then
                ' Clipboard'taki görüntüyü al
                Dim bmp As BitmapSource = Clipboard.GetImage()
                imageBox.Source = bmp
            Else
                MessageBox.Show("Clipboard'ta bir görüntü bulunamadı." & vbCrLf &
                                "Lütfen Windows+Shift+S ile bir ekran görüntüsü al ve tekrar dene.",
                                "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Görüntü yapıştırılırken hata oluştu:" & vbCrLf & ex.Message,
                            "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
    Private Sub btnSelectFromFile_Click(sender As Object, e As RoutedEventArgs) Handles btnSelectFromFile.Click
        Dim dlg As New OpenFileDialog()

        dlg.Filter = "Desteklenen Dosyalar|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff|Tüm Dosyalar|*.*"

        If dlg.ShowDialog() = True Then
            Dim filePath As String = dlg.FileName
            imageBox.Source = New BitmapImage(New Uri(filePath))
        End If
    End Sub

End Class