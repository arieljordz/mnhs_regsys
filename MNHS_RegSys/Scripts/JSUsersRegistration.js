$(document).ready(function () {
    var GlobalUserID = 0;

    $("#btnSaveUser").on('click', function (e) {
        e.preventDefault();
        debugger;
        if (fnValidateForm("#frmUsersReg") == true) {
            fnSaveUser();
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }
    });

    function fnSaveUser() {
        $('#frmUsersReg [name=CompleteName]').val(function () {
            return this.value.toUpperCase();
        });
        var frmUsersReg = $("#frmUsersReg");
        $.ajax({
            url: "/UsersRegistration/fnSaveUser",
            data: frmUsersReg.serialize(),
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved User Account!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmUsersReg").trigger("reset");
                    fnLoadUsersTable();
                }
                else {
                    alert("Error while saving User!");
                }
            }
        });
    }
    fnLoadUsersTable();
    function fnLoadUsersTable() {
        debugger;
        var UsersTable = $('#UsersTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/UsersRegistration/fnLoadUsersTable" },
            columns:
                [
                    { data: "UserID", title: "", visible: false, searchable: false },
                    { data: "CompleteName", title: "Complete Name", orderable: false },
                    { data: "UserName", title: "Username", orderable: false },
                    { data: "AccessType", title: "Access Type", orderable: false },
                    { data: "DateCreated", title: "Date Created", orderable: false },
                    {
                        "data": "UserID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnEdit btn btn-primary btn-sm ml-3/*fa fa-trash*/'>Update</button></div><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnDelete btn btn-danger btn-sm ml-3/*fa fa-trash*/'>Delete</button></div></div>"
                        }
                    },
                ]
        });

        $('#UsersTable tbody').off().on('click', 'tr', function (e) {
            //GlobalUserID = UsersTable.row(this).data().UserID;
            if (!$(e.target).is(".tbl_btnDelete") && !$(e.target).is(".tbl_btnEdit")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    GlobalUserID = fnGetRowData("UsersTable", "UserID");
                }
                else {
                    $(this).removeClass("dtactive");
                    GlobalUserID = 0;
                    $("#frmUsersReg").trigger("reset");
                    $("#btnSaveUser").removeClass("hide");
                    $("#btnUpdateUser").addClass("hide");
                }
            }
        }).click();
    }

    $(document).on('click', '.tbl_btnDelete', function () {
        if (GlobalUserID != 0) {
            MessageBoxYesNo("Are you sure you want to delete this record?", "Warning", "btn btn-primarys", "btn btn-danger", "Yes", "No");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYes").on('click', function () {
        fnDeleteUser(GlobalUserID);
    })

    function fnDeleteUser(GlobalUserID) {
        debugger;
        //var GlobalUserID = fnGetRowData("UsersTable", "UserID");
        if (GlobalUserID != 0) {
            $.ajax({
                url: "/UsersRegistration/fnDeleteUser",
                type: "POST",
                data: "&UserID=" + GlobalUserID,
                success: function (result) {
                    if (result.success) {
                        MessageBox("Successfully Deleted User!", "Information", "btn btn-danger", "Close", "bg-primarys");
                        $("#frmUsersReg").trigger("reset");
                        fnLoadUsersTable();
                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }

    $(document).on('click', '.tbl_btnEdit', function () {
        fnRetrieveUser();
        debugger;
        $("#btnSaveUser").addClass("hide");
        $("#btnUpdateUser").removeClass("hide");
    });

    $("#btnUpdateUser").on('click', function (e) {
        e.preventDefault();
        debugger;
        fnUpdateUser(GlobalUserID);
    });

    function fnRetrieveUser() {
        debugger;
        //var UserID = fnGetRowData("UsersTable", "UserID");
        if (GlobalUserID != 0) {
            $.ajax({
                url: "/UsersRegistration/fnRetrieveUser",
                type: "POST",
                data: "&UserID=" + GlobalUserID,
                success: function (result) {
                    if (result.success) {
                        debugger;
                        $("[name=CompleteName]").val(result.CompleteName);
                        $("[name=UserName]").val(result.UserName);
                        $("[name=Password]").val(result.Password);
                        $("[name=AccessType]").val(result.AccessType);
                        $("[name=DateCreated]").val(getStringFormatDateForDateField(result.DateCreated));
                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }

    function fnUpdateUser(GlobalUserID) {
        debugger;
        $('#frmUsersReg [name=CompleteName]').val(function () {
            return this.value.toUpperCase();
        });
        var frmUsersReg = $("#frmUsersReg");
        $.ajax({
            url: "/UsersRegistration/fnUpdateUser",
            data: frmUsersReg.serialize() + "&UserID=" + GlobalUserID,
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Updated User!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmUsersReg").trigger("reset");
                    $("#btnSaveUser").removeClass("hide");
                    $("#btnUpdateUser").addClass("hide");
                    fnLoadUsersTable();
                }
                else {
                    alert("Error while saving User!");
                }
            }
        });
    }

///////////////////////////////////////
    //function fnValidateForm(formIDs) {
    //    if (!$(formIDs).parsley().validate()) {
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //}
});      //end of document

