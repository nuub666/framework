﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.42
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.42.
'
Namespace ProcesosWS
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="ProcesosWSSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ProcesosWS
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private RecuperarEjecutorClienteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private EjecutarOperacionOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RecuperarTransicionesAutorizadasSobreOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RecuperarTransicionesDeInicioOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.Framework.Procesos.ProcesosAS.My.MySettings.Default.ProcesosAS_ProcesosWS_ProcesosWS
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event RecuperarEjecutorClienteCompleted As RecuperarEjecutorClienteCompletedEventHandler
        
        '''<remarks/>
        Public Event EjecutarOperacionCompleted As EjecutarOperacionCompletedEventHandler
        
        '''<remarks/>
        Public Event RecuperarTransicionesAutorizadasSobreCompleted As RecuperarTransicionesAutorizadasSobreCompletedEventHandler
        
        '''<remarks/>
        Public Event RecuperarTransicionesDeInicioCompleted As RecuperarTransicionesDeInicioCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RecuperarEjecutorCliente", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RecuperarEjecutorCliente(ByVal nombreCliente As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("RecuperarEjecutorCliente", New Object() {nombreCliente})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub RecuperarEjecutorClienteAsync(ByVal nombreCliente As String)
            Me.RecuperarEjecutorClienteAsync(nombreCliente, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RecuperarEjecutorClienteAsync(ByVal nombreCliente As String, ByVal userState As Object)
            If (Me.RecuperarEjecutorClienteOperationCompleted Is Nothing) Then
                Me.RecuperarEjecutorClienteOperationCompleted = AddressOf Me.OnRecuperarEjecutorClienteOperationCompleted
            End If
            Me.InvokeAsync("RecuperarEjecutorCliente", New Object() {nombreCliente}, Me.RecuperarEjecutorClienteOperationCompleted, userState)
        End Sub
        
        Private Sub OnRecuperarEjecutorClienteOperationCompleted(ByVal arg As Object)
            If (Not (Me.RecuperarEjecutorClienteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RecuperarEjecutorClienteCompleted(Me, New RecuperarEjecutorClienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EjecutarOperacion", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function EjecutarOperacion(<System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> ByVal pParametroOperacionPr() As Byte) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("EjecutarOperacion", New Object() {pParametroOperacionPr})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub EjecutarOperacionAsync(ByVal pParametroOperacionPr() As Byte)
            Me.EjecutarOperacionAsync(pParametroOperacionPr, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub EjecutarOperacionAsync(ByVal pParametroOperacionPr() As Byte, ByVal userState As Object)
            If (Me.EjecutarOperacionOperationCompleted Is Nothing) Then
                Me.EjecutarOperacionOperationCompleted = AddressOf Me.OnEjecutarOperacionOperationCompleted
            End If
            Me.InvokeAsync("EjecutarOperacion", New Object() {pParametroOperacionPr}, Me.EjecutarOperacionOperationCompleted, userState)
        End Sub
        
        Private Sub OnEjecutarOperacionOperationCompleted(ByVal arg As Object)
            If (Not (Me.EjecutarOperacionCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent EjecutarOperacionCompleted(Me, New EjecutarOperacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RecuperarTransicionesAutorizadasSobre", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RecuperarTransicionesAutorizadasSobre(<System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> ByVal pHuellaEntidadDN() As Byte) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("RecuperarTransicionesAutorizadasSobre", New Object() {pHuellaEntidadDN})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub RecuperarTransicionesAutorizadasSobreAsync(ByVal pHuellaEntidadDN() As Byte)
            Me.RecuperarTransicionesAutorizadasSobreAsync(pHuellaEntidadDN, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RecuperarTransicionesAutorizadasSobreAsync(ByVal pHuellaEntidadDN() As Byte, ByVal userState As Object)
            If (Me.RecuperarTransicionesAutorizadasSobreOperationCompleted Is Nothing) Then
                Me.RecuperarTransicionesAutorizadasSobreOperationCompleted = AddressOf Me.OnRecuperarTransicionesAutorizadasSobreOperationCompleted
            End If
            Me.InvokeAsync("RecuperarTransicionesAutorizadasSobre", New Object() {pHuellaEntidadDN}, Me.RecuperarTransicionesAutorizadasSobreOperationCompleted, userState)
        End Sub
        
        Private Sub OnRecuperarTransicionesAutorizadasSobreOperationCompleted(ByVal arg As Object)
            If (Not (Me.RecuperarTransicionesAutorizadasSobreCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RecuperarTransicionesAutorizadasSobreCompleted(Me, New RecuperarTransicionesAutorizadasSobreCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RecuperarTransicionesDeInicio", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RecuperarTransicionesDeInicio(<System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> ByVal pTipoDN() As Byte) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("RecuperarTransicionesDeInicio", New Object() {pTipoDN})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Overloads Sub RecuperarTransicionesDeInicioAsync(ByVal pTipoDN() As Byte)
            Me.RecuperarTransicionesDeInicioAsync(pTipoDN, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RecuperarTransicionesDeInicioAsync(ByVal pTipoDN() As Byte, ByVal userState As Object)
            If (Me.RecuperarTransicionesDeInicioOperationCompleted Is Nothing) Then
                Me.RecuperarTransicionesDeInicioOperationCompleted = AddressOf Me.OnRecuperarTransicionesDeInicioOperationCompleted
            End If
            Me.InvokeAsync("RecuperarTransicionesDeInicio", New Object() {pTipoDN}, Me.RecuperarTransicionesDeInicioOperationCompleted, userState)
        End Sub
        
        Private Sub OnRecuperarTransicionesDeInicioOperationCompleted(ByVal arg As Object)
            If (Not (Me.RecuperarTransicionesDeInicioCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RecuperarTransicionesDeInicioCompleted(Me, New RecuperarTransicionesDeInicioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")>  _
    Public Delegate Sub RecuperarEjecutorClienteCompletedEventHandler(ByVal sender As Object, ByVal e As RecuperarEjecutorClienteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class RecuperarEjecutorClienteCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")>  _
    Public Delegate Sub EjecutarOperacionCompletedEventHandler(ByVal sender As Object, ByVal e As EjecutarOperacionCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class EjecutarOperacionCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")>  _
    Public Delegate Sub RecuperarTransicionesAutorizadasSobreCompletedEventHandler(ByVal sender As Object, ByVal e As RecuperarTransicionesAutorizadasSobreCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class RecuperarTransicionesAutorizadasSobreCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")>  _
    Public Delegate Sub RecuperarTransicionesDeInicioCompletedEventHandler(ByVal sender As Object, ByVal e As RecuperarTransicionesDeInicioCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class RecuperarTransicionesDeInicioCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Byte()
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Byte())
            End Get
        End Property
    End Class
End Namespace
