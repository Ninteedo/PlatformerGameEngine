﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
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
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
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
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("PlatformerGameEngine.Resources", GetType(Resources).Assembly)
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
        '''  Looks up a localized string similar to actors.
        '''</summary>
        Friend ReadOnly Property ActorTagName() As String
            Get
                Return ResourceManager.GetString("ActorTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to currentSprite.
        '''</summary>
        Friend ReadOnly Property CurrentSpriteTagName() As String
            Get
                Return ResourceManager.GetString("CurrentSpriteTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to layer.
        '''</summary>
        Friend ReadOnly Property LayerTagName() As String
            Get
                Return ResourceManager.GetString("LayerTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to location.
        '''</summary>
        Friend ReadOnly Property LocationTagName() As String
            Get
                Return ResourceManager.GetString("LocationTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to name.
        '''</summary>
        Friend ReadOnly Property NameTagName() As String
            Get
                Return ResourceManager.GetString("NameTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to opacity.
        '''</summary>
        Friend ReadOnly Property OpacityTagName() As String
            Get
                Return ResourceManager.GetString("OpacityTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to roomIndex.
        '''</summary>
        Friend ReadOnly Property RoomIndexTagName() As String
            Get
                Return ResourceManager.GetString("RoomIndexTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to rooms.
        '''</summary>
        Friend ReadOnly Property RoomsTagName() As String
            Get
                Return ResourceManager.GetString("RoomsTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to scale.
        '''</summary>
        Friend ReadOnly Property ScaleTagName() As String
            Get
                Return ResourceManager.GetString("ScaleTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Sprite file (*.sprt)|*.sprt.
        '''</summary>
        Friend ReadOnly Property SpriteFileFilter() As String
            Get
                Return ResourceManager.GetString("SpriteFileFilter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to sprites.
        '''</summary>
        Friend ReadOnly Property SpritesTagName() As String
            Get
                Return ResourceManager.GetString("SpritesTagName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to tags.
        '''</summary>
        Friend ReadOnly Property TagsTagName() As String
            Get
                Return ResourceManager.GetString("TagsTagName", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
