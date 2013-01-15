<%@ Page Language="VB" MasterPageFile="~/CARTORIO.master" AutoEventWireup="false" CodeFile="CancelamentoNascimento.aspx.vb" Inherits="pages_Documentacao_CancelamentoNascimento" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="formulario">
        <div id="FormNascimento" class="boxes">            
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            
                            <asp:Panel ID="Panel11" runat="server" CssClass="loading" Height="47px">
                            </asp:Panel>
                            
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <h2>Cancelamento de Registro de Nascimento</h2>
                    <fieldset>
                        <legend>Dados Pessoais
                            <asp:ImageButton ID="btnPesquisarCLiente" runat="server" 
                                ImageUrl="~/recursos/Images/search.png" ToolTip="Pesquisar Cliente" 
                                Visible="False" />
                        </legend>
                        <asp:Panel ID="pnlFisica" runat="server" Height="550px">
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
                            <p>
                                <asp:Label ID="label18" runat="server" CssClass="lbl" Text="Folhas" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtNumeroFolha" runat="server" CssClass="texto" 
                                    MaxLength="3" Width="100px"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label7" runat="server" CssClass="lbl" Text="Livro" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtNumeroLivro" runat="server" CssClass="texto" MaxLength="5" 
                                    Width="100px"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label12" runat="server" CssClass="lbl" ForeColor="#FC0000" 
                                    Text="Termo"></asp:Label>
                                <asp:TextBox ID="txtNumeroTermo" runat="server" CssClass="texto" MaxLength="7" 
                                    Width="100px"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label4" runat="server" CssClass="lbl" Text="Data Emissão Via" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtEmissao" runat="server" CssClass="texto" MaxLength="10" 
                                    Width="120px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtEmissao">
                                </asp:MaskedEditExtender>   
                            </p>
                            <p>
                                <asp:Label ID="label1" runat="server" CssClass="lbl" Text="Data Registro" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtDatado" runat="server" CssClass="texto" MaxLength="10" 
                                    Width="120px"></asp:TextBox>
                                <asp:LinkButton ID="lnkData" runat="server">Atualizar data no texto</asp:LinkButton>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtDatado">
                                </asp:MaskedEditExtender>   
                            </p>
                            <p>
                                <asp:Label ID="label10" runat="server" CssClass="lbl" ForeColor="#FC0000" Text="Motivo"></asp:Label>
                                <asp:TextBox ID="txtMotivo" runat="server" CssClass="texto" 
                                    TextMode="MultiLine" Height="150px" Width="608px"></asp:TextBox>
                            </p>   
                        </asp:Panel>
                    </fieldset>
                    &nbsp; 
                    <p style="float: right">

                        <asp:Button ID="btnSalvar2" runat="server" CssClass="botaoForm save" 
                            Text="Salvar" />                
                        <asp:Button ID="btnSalvar" runat="server" CssClass="botaoForm save" 
                            Text="Salvar e imprimir documento" Visible="False" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="botaoForm" 
                            Text="Cancelar" />
                            
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
    </div>
</asp:Content>
