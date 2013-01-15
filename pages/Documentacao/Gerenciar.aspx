<%@ Page Language="VB" MasterPageFile="~/CARTORIO.master" AutoEventWireup="false" CodeFile="Gerenciar.aspx.vb" Inherits="pages_Documentacao_Gerenciar" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="formulario">
        <div id="FormNascimento" class="boxes">            
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <h2>Documentação</h2>
                    <fieldset>
                        <legend>Dados Pessoais -&nbsp;
                            <asp:ImageButton ID="btnPesquisarCLiente" runat="server" 
                                ImageUrl="~/recursos/Images/search.png" ToolTip="Pesquisar Cliente" />
                            &nbsp;&lt;&lt; Clique na lupa para selecionar ou cadastrar o cliente</legend>
                        <asp:Panel ID="pnlFisica" runat="server" Height="140px">
                            <p>
                                <asp:Label ID="label" runat="server" CssClass="lbl" Text="Nome"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblNome" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label2" runat="server" CssClass="lbl" Text="Sexo"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblSexo" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label3" runat="server" CssClass="lbl" Text="Nascido em"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblNascimdoEM" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label5" runat="server" CssClass="lbl" Text="Data Nascimento"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblDataNascimento" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                        </asp:Panel>
                    </fieldset>

                    <fieldset>
                        <legend>Dados de Documentos Emitidos</legend>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <asp:Panel ID="Panel11" runat="server" CssClass="loading" Height="47px">
                                </asp:Panel>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:Panel ID="Panel1" runat="server" Height="400px">
                            <asp:GridView ID="gvDocumento" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" 
                                Width="936px" DataKeyNames="CT03CODIGO,CT02TIPOLIVRO,CT01CODIGO" 
                                EmptyDataText="O CLIENTE AINDA NÃO SOLICITOU DOCUMENTOS" 
                                AllowPaging="True" AllowSorting="True" EnableTheming="True" PageSize="8" 
                                ShowHeaderWhenEmpty="True" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" CommandName="Editar" 
                                        DataTextField="CT03CODIGO" ImageUrl="~/recursos/Images/b_edit.png">
                                    <ItemStyle Width="10px" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Imprimir" 
                                        DataTextField="CT03CODIGO" ImageUrl="~/recursos/Images/impressora.png">
                                    <HeaderStyle Width="10px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="CT01NOME" HeaderText="Nome" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CT01CPF" HeaderText="CPF" Visible="False" >
                                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CT02NOME" HeaderText="Tipo Documento" 
                                        NullDisplayText="-">
                                    <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CT03VIA" HeaderText="Via">
                                    <HeaderStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CT03DATACRIACAO" HeaderText="Data">
                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" CommandName="Excluir" 
                                        DataTextField="CT03CODIGO" ImageUrl="~/recursos/Images/cancel.png">
                                    <HeaderStyle Width="10px" />
                                    </asp:ButtonField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <EmptyDataRowStyle BorderColor="Black" BorderStyle="Dotted" BorderWidth="2px" 
                                    Font-Bold="True" Font-Size="Small" ForeColor="Maroon" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </asp:Panel>
                    </fieldset>

                    <fieldset>
                        <legend>Novo documento</legend>
                        <asp:Panel ID="Panel2" runat="server" Height="50px">
                            <p>
                                <asp:Label ID="label8" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Tipo de Documento"></asp:Label>
                                <asp:DropDownList ID="drpDoc" runat="server">
                                </asp:DropDownList>
                                <asp:Button ID="btnNovoDoc" runat="server" CssClass="botaoForm add" Text="Solicitar Novo Documento" />
                            </p>
                        </asp:Panel>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div> 
    </div>
</asp:Content>
