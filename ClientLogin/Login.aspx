<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClientLogin.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //Highlight textbox border red
        function validateAndHighlight() {
            for (var i = 0; i < Page_Validators.length; i++) {
                var val = Page_Validators[i];
                var ctrl = document.getElementById(val.controltovalidate);
                if (ctrl != null && ctrl.style != null) {
                    if (!val.isvalid) {
                        ctrl.style.borderColor = '#FF0000';
                        ctrl.style.borderWidth = '3px'
                        ctrl.style.backgroundColor = '#FBE3E4';
                    }
                    else {
                        ctrl.style.borderColor = '';
                        ctrl.style.backgroundColor = '';
                    }
                }
            }
        };

        $(document).ready(function () {
            var the_terms = $("#chkargee");

            $('#<%= this.btnlogin.ClientID %>').attr("disabled", "disabled");

            the_terms.click(function () {
                if ($(this).is(":checked")) {
                    $('#<%= this.btnlogin.ClientID %>').removeAttr("disabled");
                } else {
                    $('#<%= this.btnlogin.ClientID %>').attr("disabled", "disabled");
                }
            });

        });
    </script>
    <div class="container">
        <div class="row">
            <div class="Absolute-Center is-Responsive">
                <div id="logo-container"></div>
                <div class="col-sm-12 col-md-10 col-md-offset-1">
                    <div class="form-group input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox runat="server" ID="txtusername" class="form-control" placeholder="Username"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="requsernamae" ControlToValidate="txtusername" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valcontrols"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                        <asp:TextBox runat="server" ID="txtpassword" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="reqpassword" ControlToValidate="txtpassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valcontrols"></asp:RequiredFieldValidator>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input type="checkbox" id="chkargee">
                            I agree to the <a href="#">Terms and Conditions</a>
                        </label>
                    </div>
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnlogin" class="btn btn-def btn-block" Text="Login" OnClick="btnlogin_Click" ValidationGroup="valcontrols" />
                    </div>
                    <div class="form-group">
                        <div style="text-align: center">
                            <label>or</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnregister" class="btn btn-def btn-block" Text="Register" OnClick="btnregister_Click" />
                    </div>
                    <div class="form-group text-center">
                        <a href="#">Forgot Password</a>&nbsp;|&nbsp;<a href="#">Support</a>
                    </div>
                    <div class="alert alert-danger" runat="server" id="divloginerror" style="text-align: center">
                        <p><strong>Login Not Found</strong></p>
                        <p>Please check your username and password and try again</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:GridView runat="server" ID="gridview1" AutoGenerateColumns="true"></asp:GridView>
        </div>
    </div>
    <asp:LinkButton ID="lnkError" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lnkRegister" runat="server"></asp:LinkButton>

    <!-- ModalPopupExtender -->
    <asp:ModalPopupExtender ID="ModalPopupError" BehaviorID="mpe" runat="server"
        PopupControlID="pnlerror" TargetControlID="lnkError" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:ModalPopupExtender ID="ModalPopupRegister" BehaviorID="mpr" runat="server"
        PopupControlID="pnlRegister" TargetControlID="lnkRegister" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlerror" runat="server" CssClass="modalPopup" Style="display: none">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                        </div>
                        <div class="modal-body">
                            <asp:Label runat="server" ID="lblerrormessage"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <div style="text-align: center;">
                                <div>
                                    <asp:Button runat="server" ID="btncloseerror" class="btn btn-def btn-block" Text="Close" OnClick="btncloseerror_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlRegister" runat="server" CssClass="modalPopup" Style="display: none">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Register
                            </h4>
                        </div>
                        <div class="modal-body">
                            <%--<div class="form-group input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox runat="server" ID="txtregClientID" class="form-control" placeholder="ClientID"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="regtxtregClientID" ControlToValidate="txtregClientID" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valregcontrols"></asp:RequiredFieldValidator>
                    </div>--%>
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:TextBox runat="server" ID="txtregusername" class="form-control" placeholder="Username"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="regtxtregusername" ControlToValidate="txtregusername" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valregcontrols"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                <asp:TextBox runat="server" ID="txtregpassword" class="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="regtxtregpassword" ControlToValidate="txtregpassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valregcontrols"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group input-group">
                                    <%--<input type="checkbox" id="chkregargee" runat="server">--%>
                                <asp:CheckBox runat="server" id="chkregargee" Text="I agree to the Terms and Conditions" OnCheckedChanged="chkregargee_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div style="text-align: center;">
                                <div>
                                    <asp:Button runat="server" ID="btnRegisterAccount" class="btn btn-def btn-block" Text="Register" OnClick="btnRegisterAccount_Click" ValidationGroup="valregcontrols" />
                                </div>
                            </div>
                            <div style="text-align: center; margin-top: 10px">
                                <div>
                                    <asp:Button runat="server" ID="btnCancelRegister" class="btn btn-def btn-block" Text="Cancel" OnClick="btnCancelRegister_Click" />
                                </div>
                            </div>
                            <div class="alert alert-danger" runat="server" id="divusernameunavailable" style="text-align: center; margin-top: 10px">
                                <p><strong>Username is unavailable</strong></p>
                                <p>The chosen username has already taken, please try another</p>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            $(document).ready(function () {
                var reg_terms = $("#chkregargee");

                $('#<%= this.btnRegisterAccount.ClientID %>').attr("disabled", "disabled");

                                reg_terms.click(function () {
                                    if ($(this).is(":checked")) {
                                        $('#<%= this.btnRegisterAccount.ClientID %>').removeAttr("disabled");
                                } else {
                                    $('#<%= this.btnRegisterAccount.ClientID %>').attr("disabled", "disabled");
                                    }
                                });

            });
        </script>
    </asp:Panel>


</asp:Content>
