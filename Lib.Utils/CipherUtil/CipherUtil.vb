Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public NotInheritable Class CipherUtil
    Private Sub New()
    End Sub

#Region "Metni Şifreleme"
    Public Shared Function EncryptString(ByVal key As String, ByVal plainText As String) As String
        Dim iv As Byte() = New Byte(15) {}
        Dim array As Byte()

        Using aes As Aes = Aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(key)
            aes.IV = iv

            Dim encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)

            Using memoryStream As New MemoryStream()
                Using cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                    Using streamWriter As New StreamWriter(cryptoStream)
                        streamWriter.Write(plainText)
                    End Using

                    array = memoryStream.ToArray()
                End Using
            End Using
        End Using

        Return Convert.ToBase64String(array)
    End Function
#End Region

#Region "Metnin Şifresini Çözme"
    Public Shared Function DecryptString(ByVal key As String, ByVal cipherText As String) As String
        Dim iv As Byte() = New Byte(15) {}
        Dim buffer As Byte() = Convert.FromBase64String(cipherText)

        Using aes As Aes = Aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(key)
            aes.IV = iv
            Dim decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)

            Using memoryStream As New MemoryStream(buffer)
                Using cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                    Using streamReader As New StreamReader(cryptoStream)
                        Return streamReader.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
    End Function
#End Region
End Class
