Imports AuxIU
Imports System.Windows.Forms
Public Class ctrlCFicheros



    Public Function AbrirFichero(ByVal sender As Object, ByVal datos As Object, ByVal vm As Framework.TiposYReflexion.DN.VinculoMetodoDN) As Framework.DatosNegocio.IEntidadDN

        ' comando

        Dim control As Framework.IU.IUComun.IctrlBasicoDN = sender
        Dim fp As MotorIU.FormulariosP.IFormularioP = CType(control, System.Windows.Forms.ContainerControl).ParentForm

        Dim HuellaFicheroAlmacenadoIO As Framework.Ficheros.FicherosDN.HuellaFicheroAlmacenadoIODN = control.DN

        Using New CursorScope(Cursors.WaitCursor)
            Dim pr As Process = System.Diagnostics.Process.Start(HuellaFicheroAlmacenadoIO.RutaAbsoluta)
        End Using

        Return HuellaFicheroAlmacenadoIO
    End Function
End Class
