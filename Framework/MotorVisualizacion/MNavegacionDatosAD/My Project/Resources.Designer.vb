﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.42
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("MNavegacionDatosAD.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to create view uwNavDatRutas as SELECT     dbo.tlEntidadNavDN.ID, tlVinculoClaseDN_1.NombreClase, dbo.tlRelacionEntidadesNavDN.ID AS Expr1, 
        '''                      dbo.tlRelacionEntidadesNavDN.NombrePropiedad, tlEntidadNavDN_1.ID AS Expr2, dbo.tlVinculoClaseDN.NombreClase AS Expr3
        '''FROM         dbo.tlEntidadNavDN INNER JOIN
        '''                      dbo.tlRelacionEntidadesNavDN ON dbo.tlEntidadNavDN.ID = dbo.tlRelacionEntidadesNavDN.idEntidadDatosOrigen INNER JOIN
        '''                      dbo.tlEntidadNavDN AS tlEn [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property uwNavDatRutas() As String
            Get
                Return ResourceManager.GetString("uwNavDatRutas", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to CREATE VIEW dbo.vwEntidadesMNavD AS SELECT     dbo.tlEntidadNavDN.ID, dbo.tlVinculoClaseDN.NombreEnsamblado, dbo.tlVinculoClaseDN.NombreClase FROM         dbo.tlEntidadNavDN INNER JOIN     dbo.tlVinculoClaseDN ON dbo.tlEntidadNavDN.idVinculoClase = dbo.tlVinculoClaseDN.ID.
        '''</summary>
        Friend ReadOnly Property vwEntidadesMNavD() As String
            Get
                Return ResourceManager.GetString("vwEntidadesMNavD", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to CREATE VIEW dbo.vwRelacionesxTipo AS SELECT     dbo.tlRelacionEntidadesNavDN.ID, dbo.tlRelacionEntidadesNavDN.idEntidadDatosOrigen, dbo.tlRelacionEntidadesNavDN.idEntidadDatosDestino,  vwEntidadesMNavD_1.NombreEnsamblado AS NEO, vwEntidadesMNavD_1.NombreClase AS NCO,   dbo.vwEntidadesMNavD.NombreEnsamblado AS NED, dbo.vwEntidadesMNavD.NombreClase AS NCD FROM         dbo.tlRelacionEntidadesNavDN INNER JOIN   dbo.vwEntidadesMNavD vwEntidadesMNavD_1 ON dbo.tlRelacionEntidadesNavDN.idEntidadDatosOrigen = vwEnti [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property vwRelacionesxTipo() As String
            Get
                Return ResourceManager.GetString("vwRelacionesxTipo", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
