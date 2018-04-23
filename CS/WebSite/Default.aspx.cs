using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxTreeList;

public partial class _Default : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		tree.Templates.DataCell = new MyDataCellTemplate(tree);		
	}
	protected void tree_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e) {
		if(MyProvider.EditList.Contains(e.NodeKey))
			e.Cell.Style["padding"] = "1px";
	}
	protected void btnCancel_Click(object sender, EventArgs e) {
		ResetEditing();
	}
	protected void tree_CustomCallback(object sender, TreeListCustomCallbackEventArgs e) {
		if(e.Argument == "edit") {
			List<TreeListNode> selection = tree.GetSelectedNodes();
			if(selection.Count == 0) return;
			foreach(TreeListNode node in selection)
				MyProvider.EditList.Add(node.Key);
			tree.UnselectAll();
			tree.DataBind();			
		} else if(e.Argument == "cancel") {
			ResetEditing();
		} else if(e.Argument.StartsWith("update")) {
			string[] data = e.Argument.Substring("update".Length).Split('\n');
			if(data.Length > 1) {
				for(int i = 0; i < data.Length; i += 2) {
					ObjectDataSource1.UpdateParameters["Id"].DefaultValue = data[i];
					ObjectDataSource1.UpdateParameters["Text"].DefaultValue = data[i + 1];
					ObjectDataSource1.Update();
				}
			}
			ResetEditing();
		}		
	}

	void ResetEditing() {
		MyProvider.EditList.Clear();		
		tree.DataBind();
	}
	protected void tree_CustomJSProperties(object sender, TreeListCustomJSPropertiesEventArgs e) {
		e.Properties["cpMyEditors"] = MyProvider.EditList;
	}
}
