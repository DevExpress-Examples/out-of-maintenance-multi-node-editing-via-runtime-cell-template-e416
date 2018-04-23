#Region "Using"

Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxTreeList
#End Region

Public Class MyDataCellTemplate
	Implements ITemplate
	Private tree As ASPxTreeList
	Public Sub New(ByVal tree As ASPxTreeList)
		Me.tree = tree
	End Sub

	Private Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim cellContainer As TreeListDataCellTemplateContainer = CType(container, TreeListDataCellTemplateContainer)
		If MyProvider.EditList.Contains(cellContainer.NodeKey) Then
			Dim box As New ASPxTextBox()
			container.Controls.Add(box)
			box.EnableViewState = False
			box.Value = cellContainer.Value
			box.Width = Unit.Percentage(100)
			box.ClientInstanceName = "myTreeEditor_" & cellContainer.NodeKey
		Else
			container.Controls.Add(New LiteralControl(cellContainer.Text))
		End If
	End Sub
End Class

Public Class MyItem
	Private m_id, m_parentId As Integer
	Private m_text As String

	Public Sub New()
	End Sub
	Public Sub New(ByVal id As Integer, ByVal parentId As Integer, ByVal text As String)
		m_id = id
		m_parentId = parentId
		m_text = text
	End Sub

	Public Property Id() As Integer
		Get
			Return m_id
		End Get
		Set(ByVal value As Integer)
			m_id = value
		End Set
	End Property
	Public Property ParentId() As Integer
		Get
			Return m_parentId
		End Get
		Set(ByVal value As Integer)
			m_parentId = value
		End Set
	End Property
	Public Property Text() As String
		Get
			Return m_text
		End Get
		Set(ByVal value As String)
			m_text = value
		End Set
	End Property
End Class

Public NotInheritable Class MyProvider

	Private Sub New()
	End Sub
	Public Shared ReadOnly Property EditList() As ArrayList
		Get
			Const key As String = "Q182424_1"
			If HttpContext.Current.Session(key) Is Nothing Then
				HttpContext.Current.Session(key) = New ArrayList()
			End If
			Return CType(HttpContext.Current.Session(key), ArrayList)
		End Get
	End Property


	Private Shared ReadOnly Property Data() As List(Of MyItem)
		Get
			Const key As String = "Q182424_2"
			If HttpContext.Current.Session(key) Is Nothing Then
				HttpContext.Current.Session(key) = CreateData()
			End If
			Return CType(HttpContext.Current.Session(key), List(Of MyItem))
		End Get
	End Property
	Private Shared Function CreateData() As List(Of MyItem)
		Dim data As List(Of MyItem) = New List(Of MyItem)()
		data.Add(New MyItem(0, -1, "Sales and Marketing"))
		data.Add(New MyItem(1, 0, "Field Office: Canada"))
		data.Add(New MyItem(2, 0, "Field Office: East Coast"))
		data.Add(New MyItem(3, -1, "Engineering"))
		data.Add(New MyItem(4, 3, "Consumer Electronics Div."))
		data.Add(New MyItem(5, 3, "Software Products Div."))
		Return data
	End Function

	Public Shared Function [Select]() As IEnumerable
		Return Data
	End Function
	Public Shared Sub Update(ByVal id As Integer, ByVal text As String)
		For Each item As MyItem In Data
			If item.Id = id Then
				item.Text = text
				Return
			End If
		Next item
	End Sub
End Class