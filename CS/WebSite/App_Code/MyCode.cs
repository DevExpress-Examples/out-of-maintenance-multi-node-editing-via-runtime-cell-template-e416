#region Using
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTreeList;
#endregion

public class MyDataCellTemplate : ITemplate {
	ASPxTreeList tree;
	public MyDataCellTemplate(ASPxTreeList tree) {
		this.tree = tree;
	}

	void ITemplate.InstantiateIn(Control container) {
		TreeListDataCellTemplateContainer cellContainer = (TreeListDataCellTemplateContainer)container;
		if(MyProvider.EditList.Contains(cellContainer.NodeKey)) {		
			ASPxTextBox box = new ASPxTextBox();
			container.Controls.Add(box);
			box.EnableViewState = false;
			box.Value = cellContainer.Value;
			box.Width = Unit.Percentage(100);
			box.ClientInstanceName = "myTreeEditor_" + cellContainer.NodeKey;
		} else {
			container.Controls.Add(new LiteralControl(cellContainer.Text));
		}
	}
}

public class MyItem {
	int m_id, m_parentId;
	string m_text;

	public MyItem() {
	}
	public MyItem(int id, int parentId, string text) {
		m_id = id;
		m_parentId = parentId;
		m_text = text;
	}

	public int Id { get { return m_id; } set { m_id = value; } }
	public int ParentId { get { return m_parentId; } set { m_parentId = value; } }
	public string Text { get { return m_text; } set { m_text = value; } }
}

public static class MyProvider {

	public static ArrayList EditList {
		get {
			const string key = "Q182424_1";
			if(HttpContext.Current.Session[key] == null)
				HttpContext.Current.Session[key] = new ArrayList();
			return (ArrayList)HttpContext.Current.Session[key];
		}
	}


	static List<MyItem> Data {
		get {
			const string key = "Q182424_2";
			if(HttpContext.Current.Session[key] == null)
				HttpContext.Current.Session[key] = CreateData();
			return (List<MyItem>)HttpContext.Current.Session[key];
		}
	}
	static List<MyItem> CreateData() {
		List<MyItem> data = new List<MyItem>();
		data.Add(new MyItem(0, -1, "Sales and Marketing"));
		data.Add(new MyItem(1, 0, "Field Office: Canada"));
		data.Add(new MyItem(2, 0, "Field Office: East Coast"));
		data.Add(new MyItem(3, -1, "Engineering"));
		data.Add(new MyItem(4, 3, "Consumer Electronics Div."));
		data.Add(new MyItem(5, 3, "Software Products Div."));
		return data;
	}

	public static IEnumerable Select() {
		return Data;
	}
	public static void Update(int id, string text) {
		foreach(MyItem item in Data) {
			if(item.Id == id) {
				item.Text = text;
				return;
			}
		}
	}
}