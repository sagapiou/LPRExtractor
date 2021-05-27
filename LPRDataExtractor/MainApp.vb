Module MainApp

    Sub Main(ByVal AppArgs As String())
        Try
            Dim LPRDataTable As DataTable
            Dim myLPRTableAdapter As LPRDataSetTableAdapters.LPRTableAdapter
            Dim i As Integer
            Console.Title = "LPR Export Application"
            myLPRTableAdapter = New LPRDataSetTableAdapters.LPRTableAdapter
            For i = 0 To AppArgs.Length - 1
                Console.WriteLine(AppArgs(i))
            Next
            If Not AppArgs.Length = 0 Then
                If Len(Trim(AppArgs(0))) <> 8 Then
                    Throw New Exception("Wrong value for Date. Date must be of the form : YYYYMMDD. e.g. 20110101")
                End If
                LPRDataTable = myLPRTableAdapter.GetData(AppArgs(0))
                ExportDatasetToCsv(LPRDataTable, "c:/LPRData/LPR" & AppArgs(0) & ".csv")
            Else
                LPRDataTable = myLPRTableAdapter.GetData(Now.Year & Return2DigitMonth(Now.Month) & Return2DigitDay(Now.Day))
                ExportDatasetToCsv(LPRDataTable, "c:/LPRData/LPR" & Now.Year & Return2DigitMonth(Now.Month) & Return2DigitDay(Now.Day) & ".csv")
            End If

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            System.Threading.Thread.Sleep(10000)
        End Try
    End Sub


    Public Sub ExportDatasetToCsv(ByVal MyDataTable As DataTable, ByVal vOutputFile As String)
        Dim dr As DataRow
        Dim myString As String
        Dim bFirstRecord As Boolean = True
        myString = ""
        Try
            Dim myWriter As New System.IO.StreamWriter(vOutputFile, False, System.Text.Encoding.UTF8)
            myString = "Country;Date;Time;CameraName;License Plate;Confidence;Onlist"
            myWriter.WriteLine(myString)
            Console.WriteLine(myString)
            myString = ""
            For Each dr In MyDataTable.Rows
                myString = dr.Item("LPRCountry") & ";" & dr.Item("LPRDate") & ";" & dr.Item("LPRTime") & ";" & _
                 dr.Item("LPRCameraName") & ";" & dr.Item("LprLicense") & ";" & dr.Item("LPRConfidence") & ";" & _
                dr.Item("LPROnList")
                'New Line to differentiate next row       
                myWriter.WriteLine(myString)
                Console.WriteLine(myString)
                myString = ""
            Next
            myWriter.Close()
            Console.WriteLine("Export Completed successfully")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        Finally
            System.Threading.Thread.Sleep(10000)
        End Try 'Write the String to the Csv FilemyWriter.WriteLine(myString)'Clean upmyWriter.Close()
    End Sub

    Private Function Return2DigitDay(ByVal vDay As String) As String
        Dim VFinalDay As String = ""
        Dim vDayInt As Integer
        Try
            Integer.TryParse(vDay, vDayInt)
            If vDayInt < 0 Or vDayInt > 32 Then
                Throw New Exception("Wrong Date Value")
            End If

            If vDayInt < 10 Then
                VFinalDay = "0" & vDayInt.ToString
            Else
                VFinalDay = vDayInt.ToString
            End If
            Return VFinalDay
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function Return2DigitMonth(ByVal vMonth As String) As String
        Dim VFinalMonth As String = ""
        Dim vMonthInt As Integer
        Try
            Integer.TryParse(vMonth, vMonthInt)
            If vMonthInt < 0 Or vMonthInt > 13 Then
                Throw New Exception("Wrong Date Value")
            End If

            If vMonthInt < 10 Then
                VFinalMonth = "0" & vMonthInt.ToString
            Else
                VFinalMonth = vMonthInt.ToString
            End If
            Return VFinalMonth
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Module
