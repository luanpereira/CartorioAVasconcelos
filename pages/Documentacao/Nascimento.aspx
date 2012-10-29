<%@ Page Language="VB" MasterPageFile="~/CARTORIO.master" AutoEventWireup="false" CodeFile="Nascimento.aspx.vb" Inherits="pages_Documentacao_Nascimento" %>

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
                    <h2>Emissão de Certidão de Nascimento</h2>
                    <fieldset>
                        <legend>Dados Pessoais
                            <asp:ImageButton ID="btnPesquisarCLiente" runat="server" 
                                ImageUrl="~/recursos/Images/search.png" ToolTip="Pesquisar Cliente" 
                                Visible="False" />
                        </legend>
                        <asp:Panel ID="pnlFisica" runat="server" Height="225px">
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
                                <asp:Label ID="label18" runat="server" CssClass="lbl" Text="Data do Registro" ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtDataRegistro" runat="server" CssClass="texto" MaxLength="10" Width="120px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="txtDataRegistro">
                                </asp:MaskedEditExtender>   
                            </p>
                            <p>
                                <asp:Label ID="label7" runat="server" CssClass="lbl" Text="Horário Nascimento" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtHorario" runat="server" CssClass="texto" MaxLength="5" Width="60px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99:99"
                                    MaskType="Time" TargetControlID="txtHorario">
                                </asp:MaskedEditExtender>   
                            </p>
                            <p>
                                <asp:Label ID="label12" runat="server" CssClass="lbl" ForeColor="#FC0000" Text="Local Nascimento"></asp:Label>
                                <asp:TextBox ID="txtLocal" runat="server" CssClass="texto" MaxLength="45" Width="360px"></asp:TextBox>
                            </p>
                            <p>
                            </p>
                        </asp:Panel>
                    </fieldset>

                    <fieldset>
                        <legend>Dados Filiação</legend>
                        <asp:Panel ID="Panel2" runat="server" Height="155px" Width="1000px">
                            <asp:Panel ID="Panel3" runat="server" Height="100%" Width="494px" 
                                style="float:left;">
                                <p>
                                    <asp:Label ID="label8" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Declarante"></asp:Label>&nbsp;
                                    <asp:DropDownList ID="drpDeclarante" runat="server">
                                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                        <asp:ListItem Value="P">O Pai</asp:ListItem>
                                        <asp:ListItem Value="M">A Mãe</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <asp:Label ID="label120" runat="server" CssClass="lbl" Text="Nome do Pai"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPai" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="label21" runat="server" CssClass="lbl" Text="Avó Paterno"></asp:Label>&nbsp;
                                    <asp:Label ID="lblAvoPaterno1" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>                                
                                <p>
                                    <asp:Label ID="label22" runat="server" CssClass="lbl" Text="Avô Paterno"></asp:Label>&nbsp;
                                    <asp:Label ID="lblAvoPaterno2" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>                                                                                                                                      
                            </asp:Panel>   

                            <asp:Panel ID="Panel4" runat="server" Height="100%" Width="494px" 
                                style="float:right;">
                                <p>
                                    <asp:Label ID="label1" runat="server" CssClass="lbl" Text="Nome da Mãe"></asp:Label>&nbsp;
                                    <asp:Label ID="lblMae" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>       
                                <p>
                                    <asp:Label ID="label4" runat="server" CssClass="lbl" Text="Avó Materno"></asp:Label>&nbsp;
                                    <asp:Label ID="lblAvoMaterno1" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>                                
                                <p>
                                    <asp:Label ID="label9" runat="server" CssClass="lbl" Text="Avô Materno"></asp:Label>&nbsp;
                                    <asp:Label ID="lblAvoMaterno2" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>         
                                <p>
                                    <asp:Label ID="label14" runat="server" CssClass="lbl" Text="Gêmeo"></asp:Label>&nbsp;
                                    <asp:Label ID="lblGemeo" runat="server" CssClass="texto" Text="-"></asp:Label>
                                </p>
                            </asp:Panel> 
                        </asp:Panel> 
                    </fieldset>

                    <fieldset>
                        <legend>Dados Para Documentação</legend>
                        <asp:Panel ID="Panel6" runat="server" Height="160px" Width="1000px">
                            <asp:Panel ID="Panel1" runat="server" Height="104%" Width="494px" 
                                style="float:left;">
                                <p>
                                    <asp:Label ID="label6" runat="server" CssClass="lbl" Text="Serventia (CNS)" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtServentia" runat="server" CssClass="texto" MaxLength="6" 
                                        Text="031385" Width="60px"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="label11" runat="server" CssClass="lbl" Text="Acervo" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtAcervo" runat="server" CssClass="texto" MaxLength="2" Text="01" Width="30px"></asp:TextBox>
                                </p>                                
                                <p>
                                    <asp:Label ID="label13" runat="server" CssClass="lbl" ForeColor="#FC0000" Text="Atribuição"></asp:Label>
                                    <asp:TextBox ID="txtAtribuicao" runat="server" CssClass="texto" MaxLength="2" Text="55" Width="30px"></asp:TextBox>
                                </p>                 
                                <p>
                                    <asp:Label ID="label15" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Ano do Registro"></asp:Label>
                                    <asp:TextBox ID="txtAnoReg" runat="server" CssClass="texto" MaxLength="4" Width="60px"></asp:TextBox>
                                </p>                                                                                                                                                            
                            </asp:Panel>   

                            <asp:Panel ID="Panel5" runat="server" Height="104%" Width="494px" 
                                style="float:right;">
                                <p>
                                    <asp:Label ID="label17" runat="server" ForeColor="#FC0000" CssClass="lbl" Text="Tipo de Livro"></asp:Label>
                                    <asp:TextBox ID="txtTipoLivro" runat="server" CssClass="texto" MaxLength="1" 
                                        Text="1" Width="30px"></asp:TextBox>
                                </p>                                
                                <p>
                                    <asp:Label ID="label19" runat="server" CssClass="lbl" ForeColor="#FC0000" Text="Número do Livro"></asp:Label>
                                    <asp:TextBox ID="txtNumeroLivro" runat="server" CssClass="texto" MaxLength="5" 
                                        Width="80px"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="label16" runat="server" CssClass="lbl" Text="Número da Folha" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtNumeroFolha" runat="server" CssClass="texto" MaxLength="3" 
                                        Width="50px"></asp:TextBox>
                                </p>
                                <p>
                                    <asp:Label ID="label20" runat="server" CssClass="lbl" Text="Número do Termo" ForeColor="#FC0000"></asp:Label>
                                    <asp:TextBox ID="txtNumeroTermo" runat="server" CssClass="texto" MaxLength="7" 
                                        Width="100px"></asp:TextBox>
                                </p>                                                                         
                            </asp:Panel> 

                            <asp:Panel ID="Panel7" runat="server" Height="100px" Width="100%" 
                                style="float:left;">
                                <p>
                                    <asp:Label ID="label10" runat="server" CssClass="lbl" Text="Averbação"></asp:Label>
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
                            Text="Salvar e imprimir documento" />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="botaoForm" 
                            Text="Cancelar" />
                            
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel> 
        </div> 
    </div> 
</asp:Content>
