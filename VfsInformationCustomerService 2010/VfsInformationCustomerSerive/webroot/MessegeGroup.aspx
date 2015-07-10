<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MessegeGroup.aspx.cs" Inherits="MessegeGroup" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
<%--<div>         
    
        <ext:Window 
            ID="Window1" 
            runat="server"
            Collapsible="true"
            Maximizable="true"
            Icon="Lorry" 
            Title="Job List"
            Width="800"
            Height="600"
            X="230"
            Y="50"
            Layout="Fit">
            <Items>
                <ext:GridPanel
                    runat="server" 
                    Header="false"
                    Border="false">                         
                    <Store>
                        <ext:Store ID="Store1" runat="server" PageSize="10">
                            <Model>
                                <ext:Model runat="server" IDProperty="ID">
                                    <Fields>
                                        <ext:ModelField Name="ID" />
                                        <ext:ModelField Name="Name" />
                                        <ext:ModelField Name="Start" Type="Date" />
                                        <ext:ModelField Name="End" Type="Date" />
                                        <ext:ModelField Name="Completed" Type="Boolean" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel runat="server">
                        <Columns>
                            <ext:Column runat="server" 
                                Text="ID" 
                                Width="40" 
                                Sortable="true" 
                                DataIndex="ID">
                              
                            </ext:Column>
                            <ext:Column runat="server" 
                                Text="Job Name" 
                                Sortable="true" 
                                DataIndex="Name"
                                Flex="1">
                              
                            </ext:Column>
                            <ext:DateColumn runat="server" 
                                Text="Start" 
                                Width="120" 
                                Sortable="true" 
                                DataIndex="Start"
                                Format="yyyy-MM-dd">
                            
                            </ext:DateColumn>
                            <ext:DateColumn runat="server" 
                                Text="End" 
                                Width="120" 
                                Sortable="true" 
                                DataIndex="End"
                                Format="yyyy-MM-dd">
                                
                            </ext:DateColumn>
                            <ext:Column runat="server" 
                                Text="Completed" 
                                Width="80" 
                                Sortable="true" 
                                DataIndex="Completed">
                                <Renderer Handler="return (value) ? 'Yes':'No';" />
                             
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView runat="server" LoadMask="false" />
                    </View>
                
                    <BottomBar>
                        <ext:PagingToolbar 
                            runat="server" 
                            DisplayInfo="true"
                            DisplayMsg="Displaying Jobs {0} - {1} of {2}"
                            />
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Window>
</div>--%>

</asp:Content>

