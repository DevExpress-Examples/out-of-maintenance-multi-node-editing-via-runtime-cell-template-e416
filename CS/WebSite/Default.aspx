<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v13.1" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v13.1" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Example</title>
<script type="text/javascript">
function myUpdate() {					
	var list = [ ];		
	for(var i = 0; i < tree.cpMyEditors.length; i++) {
		var key = tree.cpMyEditors[i];
		var name = "myTreeEditor_" + key;
		list.push(key);
		list.push(window[name].GetValue());		
	}
	tree.PerformCustomCallback("update" + list.join("\n"));
}
</script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<dx:ASPxTreeList runat="server" ID="tree" DataSourceID="ObjectDataSource1" Width="500px"
			ClientInstanceName="tree" 
			KeyFieldName="Id" ParentFieldName="ParentId" OnHtmlDataCellPrepared="tree_HtmlDataCellPrepared" OnCustomCallback="tree_CustomCallback" OnCustomJSProperties="tree_CustomJSProperties">			
			<Settings ShowFooter="true" />
			<SettingsBehavior AutoExpandAllNodes="true" />
			<SettingsSelection Enabled="true" />
			<Columns>
				<dx:TreeListTextColumn FieldName="Text">
					<FooterCellTemplate>
						<dxe:ASPxButton runat="server" ID="btnEdit" Text="Edit selected"
							ClientEnabled='<%# MyProvider.EditList.Count == 0 %>'
							UseSubmitBehavior="false" AutoPostBack="false" Native="true">
							<ClientSideEvents Click="function(s) { s.SetEnabled(false); tree.PerformCustomCallback('edit'); }" />
						</dxe:ASPxButton>
						<dxe:ASPxButton runat="server" ID="btnUpdate" Text="Update"
							ClientEnabled='<%# MyProvider.EditList.Count > 0 %>'
							UseSubmitBehavior="false" AutoPostBack="false" Native="true">							
							<ClientSideEvents Click="myUpdate" />
						</dxe:ASPxButton>						
						<dxe:ASPxButton runat="server" ID="btnCancel" Text="Cancel"
							ClientEnabled='<%# MyProvider.EditList.Count > 0 %>'
							UseSubmitBehavior="false" AutoPostBack="false" Native="true">
							<ClientSideEvents Click="function(s) { tree.PerformCustomCallback('cancel'); }" />
						</dxe:ASPxButton>
					</FooterCellTemplate>
				</dx:TreeListTextColumn>
			</Columns>
		</dx:ASPxTreeList>
		
    		
		<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
			TypeName="MyProvider" SelectMethod="Select" UpdateMethod="Update">
			<UpdateParameters>
				<asp:Parameter Name="Id" />
				<asp:Parameter Name="Text" />
			</UpdateParameters>
		</asp:ObjectDataSource>			    
    </div>
    </form>
</body>
</html>
