<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadDatFile.aspx.cs" Inherits="InternalCargoWiseReport.Dat.UploadDatFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dat File</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="../Scripts/js/sa.js"></script>
    <script>
        function alertme(Type, Message) {
            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 5000,
                timerProgressBar: true,
                onOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
        })

        Toast.fire({
            //icon: 'success',
            //title: '<asp:Literal ID="ltr" runat="server"></asp:Literal>'
            icon: Type,
            title: Message

        })
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <div class="container py-3">
            <h4 class="text-center text-uppercase">Upload Excel File For Employee Attendance </h4>
            <div class="card">
                <div class="card-header bg-danger text-white">
                    <h5 class="card-title text-uppercase">Upload</h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Upload File:(Excel Format)</label><asp:Label ID="lblStarr" runat="server" Text="*" ForeColor="Red" Font-Bold="true"></asp:Label>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                    </div>
                                    <asp:FileUpload ID="FileUpload1" runat="server" accept=".xlsx" />
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-danger rounded-0" OnClick="btnUpload_Click"
                                        UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please Wait...';"  /><br />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
