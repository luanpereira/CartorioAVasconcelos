<%@ Page Language="VB" MasterPageFile="~/CARTORIO.master" AutoEventWireup="false" CodeFile="Casamento.aspx.vb" Inherits="pages_Documentacao_Casamento" %>

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
                    <h2>Emissão de Certidão de Casamento</h2>
                    <fieldset>
                        <legend>Dados Pessoais
                            <asp:ImageButton ID="btnPesquisarCLiente" runat="server" 
                                ImageUrl="~/recursos/Images/search.png" ToolTip="Pesquisar Cliente" 
                                Visible="False" />
                        </legend>
                        <asp:Panel ID="pnlFisica" runat="server" Height="450px">
                            <p>
                                <asp:Label ID="label" runat="server" CssClass="lbl" Text="Nome"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblNome1" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label2" runat="server" CssClass="lbl" Text="Sexo"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblSexo1" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label3" runat="server" CssClass="lbl" Text="Nascido em"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblNascimdoEM1" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label5" runat="server" CssClass="lbl" Text="Data Nascimento"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblDataNascimento1" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <hr>
                            <p>
                                <asp:Label ID="label12" runat="server" CssClass="lbl" Text="Dados do Cônjuge"></asp:Label>&nbsp;&nbsp;
                                <asp:ImageButton ID="imgPesqConjuge" runat="server" 
                                    ImageUrl="~/recursos/Images/search.png" ToolTip="Pesquisar Cônjuge" />
                            </p>
                            <p>
                                <asp:Label ID="label1" runat="server" CssClass="lbl" Text="Nome"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblNome2" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label6" runat="server" CssClass="lbl" Text="Sexo"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblSexo2" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label8" runat="server" CssClass="lbl" Text="Nascido em"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblNascimdoEM2" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <p>
                                <asp:Label ID="label10" runat="server" CssClass="lbl" Text="Data Nascimento"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblDataNascimento2" runat="server" CssClass="texto" Text="-"></asp:Label>
                            </p>
                            <hr>
                            <p>
                                <asp:Label ID="label4" runat="server" CssClass="lbl" Text="Data Emissão Via" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtEmissao" runat="server" CssClass="texto" MaxLength="10" 
                                    Width="120px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtEmissao">
                                </asp:MaskedEditExtender>   
                            </p>
                            <p>
                                <asp:Label ID="label18" runat="server" CssClass="lbl" Text="Data do Casamento" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtDataRegistro" runat="server" CssClass="texto" MaxLength="10" Width="120px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtDataRegistro">
                                </asp:MaskedEditExtender>   
                            </p>
                            <p>
                                <asp:Label ID="label13" runat="server" CssClass="lbl" Text="Novo nome 1" ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtNovoNome1" runat="server" CssClass="texto" MaxLength="100" Width="350px"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label14" runat="server" CssClass="lbl" Text="Novo nome 2" ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtNovoNome2" runat="server" CssClass="texto" MaxLength="100" Width="350px"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label25" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Regime"></asp:Label>&nbsp;
                                <asp:DropDownList ID="drpRegime" runat="server">
                                    <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                    <asp:ListItem Value="1">COMUNHÃO PARCIAL DE BENS</asp:ListItem>
                                    <asp:ListItem Value="2">COMUNHÃO UNIVERSAL DE BENS</asp:ListItem>
                                    <asp:ListItem Value="3">COMUNHÃO DE SEPARAÇÃO DE BENS</asp:ListItem>
                                    <asp:ListItem Value="4">COMUNHÃO DE PARTICIPAÇÃO FINAL NOS AQUESTOS</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                        </asp:Panel>
                    </fieldset>

                    <fieldset>
                        <legend>Dados Para Documentação</legend>
                        <asp:Panel ID="Panel6" runat="server" Height="160px" Width="1000px">
                            <asp:Panel ID="Panel1" runat="server" Height="104%" Width="494px" 
                                style="float:left;">
                                <p>
                                    <asp:Label ID="label15" runat="server" CssClass="lbl" Text="Serventia (CNS)" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtServentia" runat="server" CssClass="texto" MaxLength="6" 
                                        Text="031385" Width="60px"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="label16" runat="server" CssClass="lbl" Text="Acervo" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtAcervo" runat="server" CssClass="texto" MaxLength="2" Text="01" Width="30px"></asp:TextBox>
                                </p>                                
                                <p>
                                    <asp:Label ID="label17" runat="server" CssClass="lbl" ForeColor="#FC0000" Text="Atribuição"></asp:Label>
                                    <asp:TextBox ID="txtAtribuicao" runat="server" CssClass="texto" MaxLength="2" Text="55" Width="30px"></asp:TextBox>
                                </p>                 
                                <p>
                                    <asp:Label ID="label19" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Ano do Registro"></asp:Label>
                                    <asp:TextBox ID="txtAnoReg" runat="server" CssClass="texto" MaxLength="4" Width="60px"></asp:TextBox>
                                </p>                                                                                                                                                            
                            </asp:Panel>   

                            <asp:Panel ID="Panel5" runat="server" Height="104%" Width="494px" 
                                style="float:right;">
                                <p>
                                    <asp:Label ID="label20" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Tipo de Livro"></asp:Label>
                                    <asp:TextBox ID="txtTipoLivro" runat="server" CssClass="texto" MaxLength="1" 
                                        Text="2" Width="30px" Enabled="False"></asp:TextBox>
                                </p>                                
                                <p>
                                    <asp:Label ID="label21" runat="server" CssClass="lbl" ForeColor="#FC0000" Text="Número do Livro"></asp:Label>
                                    <asp:TextBox ID="txtNumeroLivro" runat="server" CssClass="texto" MaxLength="5" 
                                        Width="80px"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="label22" runat="server" CssClass="lbl" Text="Número da Folha" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtNumeroFolha" runat="server" CssClass="texto" MaxLength="3" 
                                        Width="50px"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="label23" runat="server" CssClass="lbl" Text="Número do Termo" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtNumeroTermo" runat="server" CssClass="texto" MaxLength="7" 
                                        Width="100px"></asp:TextBox>
                                </p>                                                                         
                            </asp:Panel> 

                            <asp:Panel ID="Panel7" runat="server" Height="100px" Width="100%" 
                                style="float:left;">
                                <p>
                                    <asp:Label ID="label24" runat="server" CssClass="lbl" Text="Averbação"></asp:Label>
                                    <asp:TextBox ID="txtAverbacao" runat="server" CssClass="texto" 
                                        TextMode="MultiLine" Height="80px" Width="608px"></asp:TextBox>
                                </p>                                                                                                      
                            </asp:Panel> 
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