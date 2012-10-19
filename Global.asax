<%@ Application Language="VB" %>
<%@ Import Namespace="System.Web.Mail" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        'Dim mail As New MailMessage()
        'mail.To = "contato@luanpereira.com"
        'mail.From = "luanpereira_c@hotmail.com"
        'mail.Subject = "Iniciando Sistema Cartorio."
        'mail.Body = "Iniciando sistema cartório a vasconcelos"
        'SmtpMail.SmtpServer = "localhost" 'your real server goes here
        'SmtpMail.Send(mail)
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
        
        'Dim mail As New MailMessage()
        'Dim ex As Exception = Server.GetLastError()
        'mail.To = "contato@luanpereira.com"
        'mail.From = "luanpereira_c@hotmail.com"
        'mail.Subject = "Erro Sistema Cartorio."
        'mail.Body = ex.ToString
        'SmtpMail.SmtpServer = "localhost" 'your real server goes here
        'SmtpMail.Send(mail)
        
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>