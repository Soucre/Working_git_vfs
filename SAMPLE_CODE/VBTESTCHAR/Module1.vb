Module Module1

    Sub Main()

    End Sub
    Function URLEncode(EncodeStr As String) As String
        Dim i As Integer
        Dim erg As String
        erg = EncodeStr
        erg = Replace(erg, "%", Chr(1))
        erg = Replace(erg, "+", Chr(2))
        For i = 0 To 255
            Select Case i
                ' *** Allowed 'regular' characters
                Case 37, 43, 45, 46, 48 To 57, 65 To 90, 95, 97 To 122, 126
                Case 1 ' *** Replace original %
                    erg = Replace(erg, Chr(i), "%25")
                Case 2 ' *** Replace original +
                    erg = Replace(erg, Chr(i), "%2B")
                Case 32
                    erg = Replace(erg, Chr(i), "+")
                Case 3 To 15
                    erg = Replace(erg, Chr(i), "%0" & Hex(i))
                Case Else
                    erg = Replace(erg, Chr(i), "%" & Hex(i))
            End Select
        Next
        Return erg
    End Function
    Function HashString(ByVal StringToHash As String, ByVal HachKey As String) As String
        Dim myEncoder As New System.Text.UTF8Encoding
        Dim Key() As Byte = myEncoder.GetBytes(HachKey)
        Dim Text() As Byte = myEncoder.GetBytes(StringToHash)
        Dim myHMACSHA256 As New System.Security.Cryptography.HMACSHA256(Key)
        Dim HashCode As Byte() = myHMACSHA256.ComputeHash(Text)
        Dim hash As String = Replace(BitConverter.ToString(HashCode), "-", "")
        Return hash.ToLower
    End Function
End Module
