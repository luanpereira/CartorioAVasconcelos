<%@ Page Language="VB" MasterPageFile="~/CARTORIO.master" AutoEventWireup="false" CodeFile="CadastroCliente.aspx.vb" Inherits="pages_administrativo_CadastroCliente" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="formulario">
        <div id="FormCliente" class="boxes">            
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <h2>Cadastrar novo Cliente</h2>
                &nbsp;<fieldset>
                    <legend>Dados Pessoais</legend>
                    <asp:Panel ID="pnlFisica" runat="server" Height="124px">
                        <p>
                            <asp:Label ID="label" runat="server" Text="Nome" CssClass="lbl" 
                                ForeColor="#FC0000"></asp:Label>
                            <asp:TextBox ID="txtNome" runat="server" CssClass="texto" MaxLength="45"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="label2" runat="server" CssClass="lbl" Text="CPF"></asp:Label>
                            <asp:TextBox ID="txtCPF" runat="server" CssClass="texto" Width="150px" 
                                MaxLength="14"></asp:TextBox>
                        </p>
                            <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="999,999,999-99"
                                MaskType="None" TargetControlID="txtCPF">
                            </asp:MaskedEditExtender>     
                        <p>
                            <asp:Label ID="label3" runat="server" CssClass="lbl" Text="RG"></asp:Label>
                            <asp:TextBox ID="txtRg" runat="server" CssClass="texto" Width="150px" 
                                MaxLength="20"></asp:TextBox>
                        </p>                    
                    </asp:Panel>                
            
                    <asp:Panel ID="pnlComum" runat="server" Height="170px" Width="1000px">
                        <asp:Panel ID="pnlComum2" runat="server" Height="100%" Width="494px" 
                            style="float:left;">
                            <p>
                                <asp:Label ID="label4" runat="server" CssClass="lbl" Text="Endereço"></asp:Label>
                                <asp:TextBox ID="txtEndereco" runat="server" CssClass="texto" MaxLength="100"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label5" runat="server" CssClass="lbl" Text="UF"></asp:Label>
                                <asp:DropDownList ID="drpUF" runat="server" AutoPostBack="True" ></asp:DropDownList>
                            </p>                    
                            <p>
                                <asp:Label ID="label6" runat="server" CssClass="lbl" Text="Cidade"></asp:Label>
                                <asp:DropDownList ID="drpCidade" runat="server" AutoPostBack="True" ></asp:DropDownList>
                            </p>    
                            <p>
                                <asp:Label ID="label19" runat="server" CssClass="lbl" Text="Data Nascimento" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="texto" Width="115px" 
                                    MaxLength="9"></asp:TextBox>
                            </p>                                     
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDataNascimento">
                            </asp:MaskedEditExtender>                                                                                              
                        </asp:Panel>   

                        <asp:Panel ID="Panel1" runat="server" Height="100%" Width="494px" 
                            style="float:right;">
                            <p>
                                <asp:Label ID="label12" runat="server" CssClass="lbl" Text="Telefone Fixo"></asp:Label>
                                <asp:TextBox ID="txtTelefoneFixo" runat="server" CssClass="texto" Width="150px" 
                                    MaxLength="14"></asp:TextBox>
                            </p>                                
                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="(99) 9999-9999"
                                MaskType="None" TargetControlID="txtTelefoneFixo">
                            </asp:MaskedEditExtender>    
                            <p>
                                <asp:Label ID="label13" runat="server" CssClass="lbl" Text="Celular"></asp:Label>
                                <asp:TextBox ID="txtCelular" runat="server" CssClass="texto" Width="150px" 
                                    MaxLength="14"></asp:TextBox>
                            </p>                 
                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" ErrorTooltipEnabled="True" InputDirection="LeftToRight" Mask="(99) 9999-9999"
                                MaskType="None" TargetControlID="txtCelular">
                            </asp:MaskedEditExtender>                                          
                            <p>
                                <asp:Label ID="label11" runat="server" CssClass="lbl" Text="E-mail"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="texto" style="text-transform: lowercase;" Width="150px" 
                                    MaxLength="45"></asp:TextBox>
                            </p>                                               
                        </asp:Panel> 
                    </asp:Panel>         
                    
                    <asp:Panel ID="Panel5" runat="server" Height="125px" Width="1000px">
                        <asp:Panel ID="Panel6" runat="server" Height="100%" Width="494px" 
                            style="float:left;">
                            <p>
                                <asp:Label ID="label15" runat="server" CssClass="lbl" Text="Estado Civil"></asp:Label>
                                <asp:DropDownList ID="drpEstadoCivil" runat="server" AutoPostBack="True" >
                                    <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                    <asp:ListItem Value="C">Casado(a)</asp:ListItem>
                                    <asp:ListItem Value="S">Solteiro(a)</asp:ListItem>
                                    <asp:ListItem Value="U">União Estável</asp:ListItem>
                                    <asp:ListItem Value="V">Viúvo(a)</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                            <p>
                                <asp:Label ID="label16" runat="server" CssClass="lbl" Text="Sexo" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:DropDownList ID="drpSexo" runat="server" AutoPostBack="True" >
                                    <asp:ListItem Value="0">Selecione...</asp:ListItem>
                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                </asp:DropDownList>
                            </p>       
                            <p>
                                <asp:Label ID="label17" runat="server" CssClass="lbl" Text="Profissão"></asp:Label>
                                <asp:TextBox ID="txtProfissão" runat="server" CssClass="texto" MaxLength="100"></asp:TextBox>
                            </p>                                                                                                                                                              
                        </asp:Panel>   

                        <asp:Panel ID="Panel7" runat="server" Height="100%" Width="494px" 
                            style="float:right;">                             
                            <p>
                                <asp:Label ID="label18" runat="server" CssClass="lbl" Text="UF Natural"></asp:Label>
                                <asp:DropDownList ID="drpUfNatural" runat="server" AutoPostBack="True" ></asp:DropDownList>
                            </p>                    
                            <p>
                                <asp:Label ID="label20" runat="server" CssClass="lbl" Text="Cidade Natural"></asp:Label>
                                <asp:DropDownList ID="drpCidadeNatural" runat="server" AutoPostBack="True" ></asp:DropDownList>
                            </p>     
                            <p>
                                <asp:Label ID="label10" runat="server" CssClass="lbl" Text="Gêmeo"></asp:Label>
                                <asp:DropDownList ID="drpGemeo" runat="server" AutoPostBack="True" ></asp:DropDownList>
                            </p>   
                        </asp:Panel> 
                    </asp:Panel> 

                    <asp:Panel ID="Panel2" runat="server" Height="150px" Width="1000px">
                        <asp:Panel ID="Panel3" runat="server" Height="100%" Width="494px" 
                            style="float:left;">
                            <p>
                                <asp:Label ID="label1" runat="server" CssClass="lbl" Text="Nome do Pai" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtPai" runat="server" CssClass="texto" MaxLength="100"></asp:TextBox>
                            </p>
                            <p>
                                <asp:Label ID="label21" runat="server" CssClass="lbl" Text="Avó Paterno"></asp:Label>
                                <asp:TextBox ID="txtAvoPaterno1" runat="server" CssClass="texto" 
                                    MaxLength="100"></asp:TextBox>
                            </p>                                
                            <p>
                                <asp:Label ID="label22" runat="server" CssClass="lbl" Text="Avô Paterno"></asp:Label>
                                <asp:TextBox ID="txtAvoPaterno2" runat="server" CssClass="texto" 
                                    MaxLength="100"></asp:TextBox>
                            </p>                                                                                                                                      
                        </asp:Panel>   

                        <asp:Panel ID="Panel4" runat="server" Height="100%" Width="494px" 
                            style="float:right;">
                            <p>
                                <asp:Label ID="label7" runat="server" CssClass="lbl" Text="Nome da Mãe" 
                                    ForeColor="#FC0000"></asp:Label>
                                <asp:TextBox ID="txtMae" runat="server" CssClass="texto" MaxLength="100"></asp:TextBox>
                            </p>       
                            <p>
                                <asp:Label ID="label8" runat="server" CssClass="lbl" Text="Avó Materno"></asp:Label>
                                <asp:TextBox ID="txtAvoMaterno1" runat="server" CssClass="texto" 
                                    MaxLength="100"></asp:TextBox>
                            </p>                                
                            <p>
                                <asp:Label ID="label9" runat="server" CssClass="lbl" Text="Avô Materno"></asp:Label>
                                <asp:TextBox ID="txtAvoMaterno2" runat="server" CssClass="texto" 
                                    MaxLength="100"></asp:TextBox>
                            </p>         
                        </asp:Panel> 
                    </asp:Panel>        
                                                                                 
                </fieldset>
                &nbsp; 
                <p style="float: right">
                
                    <asp:Button ID="btnSalvar" runat="server" CssClass="botaoForm save" 
                        Text="Salvar" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="botaoForm" 
                        Text="Cancelar" />
                            
                </p>
                </ContentTemplate> 
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>