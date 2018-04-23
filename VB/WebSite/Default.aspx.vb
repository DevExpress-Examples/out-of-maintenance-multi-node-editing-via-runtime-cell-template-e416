Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxTreeList

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		tree.Templates.DataCell = New MyDataCellTemplate(tree)
	End Sub
	Protected Sub tree_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs)
		If MyProvider.EditList.Contains(e.NodeKey) Then
			e.Cell.Style("padding") = "1px"
		End If
	End Sub
	Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
		ResetEditing()
	End Sub
	Protected Sub tree_CustomCallback(ByVal sender As Object, ByVal e As TreeListCustomCallbackEventArgs)
		If e.Argument = "edit" Then
			Dim selection As List(Of TreeListNode) = tree.GetSelectedNodes()
			If selection.Count = 0 Then
				Return
			End If
			For Each node As TreeListNode In selection
				MyProvider.EditList.Add(node.Key)
			Next node
			tree.UnselectAll()
			tree.DataBind()
		ElseIf e.Argument = "cancel" Then
			ResetEditing()
		ElseIf e.Argument.StartsWith("update") Then
			Dim data() As String = e.Argument.Substring("update".Length).Split(ControlChars.Lf)
			If data.Length > 1 Then
				For i As Integer = 0 To data.Length - 1 Step 2
					ObjectDataSource1.UpdateParameters("Id").DefaultValue = data(i)
					ObjectDataSource1.UpdateParameters("Text").DefaultValue = data(i + 1)
					ObjectDataSource1.Update()
				Next i
			End If
			ResetEditing()
		End If
	End Sub

	Private Sub ResetEditing()
		MyProvider.EditList.Clear()
		tree.DataBind()
	End Sub
	Protected Sub tree_CustomJSProperties(ByVal sender As Object, ByVal e As TreeListCustomJSPropertiesEventArgs)
		e.Properties("cpMyEditors") = MyProvider.EditList
	End Sub
End Class
