<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function OnSelectionChanged(s, e) {
            var firstIndex = s.GetTopVisibleIndex();
            var numberOfRows = s.GetVisibleRowsOnPage();
            var array = [];
            for (var i = firstIndex; i < firstIndex + numberOfRows; i++) {
                array.push({ id: s.GetRowKey(i), status: s.IsRowSelectedOnPage(i) });
            }
            PageMethods.UpdateDB(array);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings: NorthwindConnectionString %>"
                SelectCommand="SELECT * FROM Products">
            </asp:SqlDataSource>
            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource1"
                KeyFieldName="ProductID" SettingsBehavior-AllowSelectByRowClick="true">
                <ClientSideEvents SelectionChanged="OnSelectionChanged" />
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn FieldName="ProductID" VisibleIndex="1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ProductName" VisibleIndex="2"></dx:GridViewDataColumn>
                </Columns>
                <SettingsBehavior AllowClientEventsOnLoad="false" />
            </dx:ASPxGridView>
        </div>
    </form>
</body>
</html>