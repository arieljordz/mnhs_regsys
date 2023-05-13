$(document).ready(function () {
    GlobalEncodedDataID = 0;
    GlobalGradeLevelID = 0;
    GlobalSchoolYearID = 0;
    GlobalEthnicityID = 0;
    GlobalReligionID = 0;


    $("#btnSaveEncodedData").on('click', function (e) {
        e.preventDefault();
        debugger;
        if (fnValidateForm("#frmEncodedData") == true) {
            fnSaveEncodedData();
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }
    });

    function fnSaveEncodedData() {
        var frmAdviser = $("#frmEncodedData");
        $.ajax({
            url: "/FileMaintenance/fnSaveEncodedData",
            data: frmAdviser.serialize(),
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmEncodedData").trigger("reset");
                    fnLoadEncodedDataTable();
                }
                else {
                    MessageBox("There is a record that already exist!.", "Warning", "btn btn-danger", "Ok", "bg-warning");
                }
            }
        });
    }

    fnLoadEncodedDataTable();
    function fnLoadEncodedDataTable() {
        debugger;
        var EncodedDataTable = $('#EncodedDataTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/FileMaintenance/fnLoadEncodedDataTable" },
            columns:
                [
                    { data: "EncodedDataID", title: "", visible: false, searchable: false },
                    { data: "NoOfTeacher", title: "No Of Teachers", sClass: "dt-body-left", orderable: false },
                    { data: "NoOfClassRoom", title: "No Of Class Rooms", sClass: "dt-body-left", orderable: false },
                    { data: "NoOfComfortRoom", title: "No Of Toilets", sClass: "dt-body-left", orderable: false },
                    { data: "NoOfComputer", title: "No Of Computers", sClass: "dt-body-left", orderable: false },
                    { data: "NoOfSeat", title: "No Of Seats", sClass: "dt-body-left", orderable: false },
                    {
                        "data": "EncodedDataID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnEdit btn btn-primary btn-sm ml-3/*fa fa-trash*/'>Update</button></div></div>"
                        }
                    },
                ]
        });

        $('#EncodedDataTable tbody').off().on('click', 'tr', function (e) {
            //GlobalEncodedDataID = EncodedDataTable.row(this).data().EncodedDataID;
            if (!$(e.target).is(".tbl_btnEdit")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    debugger;
                    GlobalEncodedDataID = fnGetRowData("EncodedDataTable", "EncodedDataID");
                }
                else {
                    $(this).removeClass("dtactive");
                    GlobalEncodedDataID = 0;
                    $("#frmEncodedData").trigger("reset");
                    $("#btnSaveEncodedData").removeClass("hide");
                    $("#btnUpdateEncodedData").addClass("hide");
                }
            }
        }).click();
    }

    $(document).on('click', '.tbl_btnDelete', function () {
        if (GlobalEncodedDataID != 0) {
            $("#MessageBoxYesNoED").modal("show");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYesED").on('click', function (e) {
        e.preventDefault();
        fnDeleteEncodedData(GlobalEncodedDataID);
    });

    function fnDeleteEncodedData(GlobalEncodedDataID) {
        debugger;
        $.ajax({
            url: "/FileMaintenance/fnDeleteEncodedData",
            type: "POST",
            data: "&EncodedDataID=" + GlobalEncodedDataID,
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Deleted!", "Information", "btn btn-danger", "Close", "bg-primarys");
                    $("#frmEncodedData").trigger("reset");
                    fnLoadEncodedDataTable();
                }
            }
        });
    }


    $(document).on('click', '.tbl_btnEdit', function () {
        fnRetrieveEncodedData();
        debugger;
        $("#btnSaveEncodedData").addClass("hide");
        $("#btnUpdateEncodedData").removeClass("hide");
    });

    $("#btnUpdateEncodedData").on('click', function (e) {
        e.preventDefault();
        debugger;
        fnUpdateEncodedData(GlobalEncodedDataID);
    });

    function fnRetrieveEncodedData() {
        debugger;
        //var EncodedDataID = fnGetRowData("EncodedDataTable", "EncodedDataID");
        if (GlobalEncodedDataID != 0) {
            $.ajax({
                url: "/FileMaintenance/fnRetrieveEncodedData",
                type: "POST",
                data: "&EncodedDataID=" + GlobalEncodedDataID,
                success: function (result) {
                    if (result.success) {
                        debugger;
                        $("[name=NoOfTeacher]").val(result.NoOfTeacher);
                        $("[name=NoOfClassRoom]").val(result.NoOfClassRoom);
                        $("[name=NoOfComputer]").val(result.NoOfComputer);
                        $("[name=NoOfComfortRoom]").val(result.NoOfComfortRoom);
                        $("[name=NoOfSeat]").val(result.NoOfSeat);
                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }

    function fnUpdateEncodedData(GlobalEncodedDataID) {
        debugger;
        var frmEncodedData = $("#frmEncodedData");
        $.ajax({
            url: "/FileMaintenance/fnUpdateEncodedData",
            data: frmEncodedData.serialize() + "&EncodedDataID=" + GlobalEncodedDataID,
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Updated Encoded Data!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmEncodedData").trigger("reset");
                    $("#btnSaveEncodedData").removeClass("hide");
                    $("#btnUpdateEncodedData").addClass("hide");
                    fnLoadEncodedDataTable();
                }
                else {
                    alert("Error while saving Encoded Data!");
                }
            }
        });
    }

    //////////////////////////////////////////////////////////////////

    $("#btnSaveGradeLevel").on('click', function (e) {
        e.preventDefault();
        debugger;
        if (fnValidateForm("#frmGradeLevel") == true) {
            fnSaveGradeLevel();
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }
    });

    function fnSaveGradeLevel() {
        var frmGradeLevel = $("#frmGradeLevel");
        $.ajax({
            url: "/FileMaintenance/fnSaveGradeLevel",
            data: frmGradeLevel.serialize(),
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved Grade Level!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmGradeLevel").trigger("reset");
                    fnLoadGradeLevelsTable();
                }
                else {
                    alert("Error while saving User!");
                }
            }
        });
    }

    fnLoadGradeLevelsTable();
    function fnLoadGradeLevelsTable() {
        debugger;
        var GradeLevelsTable = $('#GradeLevelsTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/FileMaintenance/fnLoadGradeLevelsTable" },
            columns:
                [
                    { data: "GradeLevelID", title: "", visible: false, searchable: false },
                    { data: "Description", title: "Grade Level", sClass: "dt-body-left", orderable: false },
                    {
                        "data": "GradeLevelID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnDeleteGL btn btn-danger btn-sm ml-3/*fa fa-trash*/'>Delete</button></div></div>"
                        }
                    },
                ]
        });

        $('#GradeLevelsTable tbody').off().on('click', 'tr', function (e) {
            //GlobalGradeLevelID = GradeLevelsTable.row(this).data().GradeLevelID;
            if (!$(e.target).is(".tbl_btnDeleteGL")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    GlobalGradeLevelID = fnGetRowData("GradeLevelsTable", "GradeLevelID");
                }
                else {
                    $(this).removeClass("dtactive");
                    GlobalGradeLevelID = 0;
                    $("#frmGradeLevel").trigger("reset");
                }
            }
        }).click();
    }

    $(document).on('click', '.tbl_btnDeleteGL', function () {
        if (GlobalGradeLevelID != 0) {
            $("#MessageBoxYesNoGL").modal("show");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYesGL").on('click', function (e) {
        e.preventDefault();
        fnDeleteGradeLevel(GlobalGradeLevelID);
    });

    function fnDeleteGradeLevel(GlobalGradeLevelID) {
        debugger;
        $.ajax({
            url: "/FileMaintenance/fnDeleteGradeLevel",
            type: "POST",
            data: "&GradeLevelID=" + GlobalGradeLevelID,
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Deleted!", "Information", "btn btn-danger", "Close", "bg-primarys");
                    $("#frmGradeLevel").trigger("reset");
                    fnLoadGradeLevelsTable();
                }
            }
        });
    }

    /////////////////////////////////////////////////////////////////

    $("#btnSaveSchoolYear").on('click', function (e) {
        e.preventDefault();
        debugger;
        if (fnValidateForm("#frmSchoolYear") == true) {
            fnSaveSchoolYear();
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }
    });

    function fnSaveSchoolYear() {
        var frmSchoolYear = $("#frmSchoolYear");
        $.ajax({
            url: "/FileMaintenance/fnSaveSchoolYear",
            data: frmSchoolYear.serialize(),
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved School Year!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmSchoolYear").trigger("reset");
                    fnLoadSchoolYearsTable();
                }
                else {
                    alert("Error while saving User!");
                }
            }
        });
    }
    fnLoadSchoolYearsTable();
    function fnLoadSchoolYearsTable() {
        debugger;
        var SchoolYearsTable = $('#SchoolYearsTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/FileMaintenance/fnLoadSchoolYearsTable" },
            columns:
                [
                    { data: "SchoolYearID", title: "", visible: false, searchable: false },
                    {
                        data: "IsActive", title: "ACTIVE", searchable: false, orderable: false, sClass: "alignCenter",
                        "mRender": function (data, type, full, meta) {
                            if (data == undefined) return '';
                            if (data) {
                                return '<input id="chkActiveSY_' + meta.row + '"style="height:20px" class="form-control flat mt-1 ml-2 action" type="checkbox" checked="checked" value="' + data + ' " />'
                            }
                            else {
                                return '<input id="chkActiveSY_' + meta.row + '"style="height:20px" class="form-control flat mt-1 ml-2 action" type="checkbox" value="' + data + ' "/>'
                            }
                        },
                    },
                    { data: "SchoolYear", title: "School Year", sClass: "dt-body-center", orderable: false },
                    {
                        "data": "SchoolYearID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnDeleteSY btn btn-danger btn-sm ml-3/*fa fa-trash*/'>Delete</button></div></div>"
                        }
                    },
                ]
        });



        $('#SchoolYearsTable tbody').off().on('click', 'tr', function (e) {
            //GlobalSchoolYearID = SchoolYearsTable.row(this).data().SchoolYearID;
            if (!$(e.target).is(".tbl_btnDeleteSY")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    var Index = SchoolYearsTable.row(this).index();
                    GlobalSchoolYearID = fnGetRowData("SchoolYearsTable", "SchoolYearID");
                    if ($(e.target).is(".action")) {
                        if (GlobalSchoolYearID != 0) {
                            if ($(e.target).is("[id^=chkActiveSY_],[id^=chkActiveSY_] i")) {
                                var ids = $("#SchoolYearsTable").DataTable().context[0].aoData.length;
                                for (var i = 0; i < ids; i++) {
                                    $("#chkActiveSY_" + i).prop('checked', false)
                                }
                                $("#chkActiveSY_" + Index).prop('checked', true)
                                if ($("#chkActiveSY_" + Index).prop('checked')) {
                                    AjaxUpdateActiveSY(GlobalSchoolYearID);
                                } else {
                                }
                                return;
                            }
                        }
                        else {
                            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
                        }
                    }
                }
                else {
                    $(this).removeClass("dtactive");
                    GlobalSchoolYearID = 0;
                    $("#frmSchoolYear").trigger("reset");
                }
            }
        }).click();
    }

    function AjaxUpdateActiveSY(GlobalSchoolYearID) {
        var url = '../FileMaintenance/AjaxUpdateActiveSY?&SY_ID=' + GlobalSchoolYearID;
        $.ajax({
            url: url,
            type: 'post',
            success: function (result) {
                if (result != null) {
                    var data = result.Data;
                    $("#SchoolYearsTable, #SchoolYearsTable_paginate").empty();
                    fnLoadSchoolYearsTable();
                }
                else {
                }
            },
            error: function () {
                alert("Client/Server error, please contact system administrator.");
            }
        });
    }

    $(document).on('click', '.tbl_btnDeleteSY', function () {
        if (GlobalSchoolYearID != 0) {
            $("#MessageBoxYesNoSY").modal("show");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYesSY").on('click', function (e) {
        e.preventDefault();
        fnDeleteSchoolYear(GlobalSchoolYearID);
    });

    function fnDeleteSchoolYear() {
        debugger;
        if (GlobalSchoolYearID != 0) {
            $.ajax({
                url: "/FileMaintenance/fnDeleteSchoolYear",
                type: "POST",
                data: "&SchoolYearID=" + GlobalSchoolYearID,
                success: function (result) {
                    if (result.success) {
                        MessageBox("Successfully Deleted!", "Information", "btn btn-danger", "Close", "bg-primarys");
                        $("#frmSchoolYear").trigger("reset");
                        fnLoadSchoolYearsTable();
                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }

    //////////////////////////////////////////////////////////////////

    $("#btnSaveEthnicity").on('click', function (e) {
        e.preventDefault();
        debugger;
        if (fnValidateForm("#frmEthnicity") == true) {
            fnSaveEthnicity();
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }
    });

    function fnSaveEthnicity() {
        var frmEthnicity = $("#frmEthnicity");
        $.ajax({
            url: "/FileMaintenance/fnSaveEthnicity",
            data: frmEthnicity.serialize(),
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved Ethnicity!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmEthnicity").trigger("reset");
                    fnLoadEthnicityTable();
                }
                else {
                    alert("Error while saving Ethnicity!");
                }
            }
        });
    }

    fnLoadEthnicityTable();
    function fnLoadEthnicityTable() {
        debugger;
        var EthnicityTable = $('#EthnicitysTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/FileMaintenance/fnLoadEthnicityTable" },
            columns:
                [
                    { data: "EthnicityID", title: "", visible: false, searchable: false },
                    { data: "Description", title: "Grade Level", sClass: "dt-body-left", orderable: false },
                    {
                        "data": "EthnicityID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnDeleteEth btn btn-danger btn-sm ml-3/*fa fa-trash*/'>Delete</button></div></div>"
                        }
                    },
                ]
        });

        $('#EthnicitysTable tbody').off().on('click', 'tr', function (e) {
            //GlobalEthnicityID = EthnicityTable.row(this).data().EthnicityID;
            if (!$(e.target).is(".tbl_btnDeleteEth")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    GlobalEthnicityID = fnGetRowData("EthnicitysTable", "EthnicityID");
                }
                else {
                    $(this).removeClass("dtactive");
                    GlobalEthnicityID = 0;
                    $("#frmEthnicity").trigger("reset");
                }
            }
        }).click();
    }

    $(document).on('click', '.tbl_btnDeleteEth', function () {
        if (GlobalEthnicityID != 0) {
            $("#MessageBoxYesNoET").modal("show");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYesET").on('click', function (e) {
        e.preventDefault();
        fnDeleteEthnicity(GlobalEthnicityID);
    });

    function fnDeleteEthnicity() {
        debugger;
        if (GlobalEthnicityID != 0) {
            $.ajax({
                url: "/FileMaintenance/fnDeleteEthnicity",
                type: "POST",
                data: "&EthnicityID=" + GlobalEthnicityID,
                success: function (result) {
                    if (result.success) {
                        MessageBox("Successfully Deleted!", "Information", "btn btn-danger", "Close", "bg-primarys");
                        $("#frmEthnicity").trigger("reset");
                        fnLoadEthnicityTable();
                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }

    //////////////////////////////////////////////////////////////////

    $("#btnSaveReligion").on('click', function (e) {
        e.preventDefault();
        debugger;
        if (fnValidateForm("#frmReligion") == true) {
            fnSaveReligion();
        } else {
            MessageBox("Please fill the required fields.", "Information", "btn btn-danger", "Close", "bg-primarys");
        }
    });

    function fnSaveReligion() {
        var frmReligion = $("#frmReligion");
        $.ajax({
            url: "/FileMaintenance/fnSaveReligion",
            data: frmReligion.serialize(),
            type: "POST",
            success: function (result) {
                if (result.success) {
                    MessageBox("Successfully Saved Religion!", "Information", "btn btn-danger", "Ok", "bg-primarys");
                    $("#frmReligion").trigger("reset");
                    fnLoadReligionTable();
                }
                else {
                    alert("Error while saving Religion!");
                }
            }
        });
    }
    fnLoadReligionTable();
    function fnLoadReligionTable() {
        debugger;
        var ReligionsTable = $('#ReligionTable').DataTable({
            responsive: true,
            processing: true,
            destroy: true,
            stateSave: true,
            searching: true, paging: true, info: true,
            lengthChange: true,
            order: [[1, "asc"], [2, "asc"]],
            lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
            ajax: { "url": "/FileMaintenance/fnLoadReligionTable" },
            columns:
                [
                    { data: "ReligionID", title: "", visible: false, searchable: false },
                    { data: "Description", title: "Grade Level", sClass: "dt-body-left", orderable: false },
                    {
                        "data": "ReligionID", title: "ACTION", "autoWidth": true, "render": function (data) {
                            return "<div class='d-flex justify-content-center'><div style='text-align:center' class='ml-3'><button type='button' class='tbl_btnDeleteRel btn btn-danger btn-sm ml-3/*fa fa-trash*/'>Delete</button></div></div>"
                        }
                    },
                ]
        });

        $('#ReligionTable tbody').off().on('click', 'tr', function (e) {
            // GlobalReligionID = ReligionsTable.row(this).data().ReligionID;
            if (!$(e.target).is(".tbl_btnDeleteRel")) {
                if (!$(this).hasClass("dtactive")) {
                    $(this).parent().find("tr").removeClass("dtactive");
                    $(this).addClass("dtactive");
                    GlobalReligionID = fnGetRowData("ReligionTable", "ReligionID");
                }
                else {
                    $(this).removeClass("dtactive");
                    $(this).addClass("dtactive");
                    $("#frmReligion").trigger("reset");
                }
            }
        }).click();
    }

    $(document).on('click', '.tbl_btnDeleteRel', function () {
        if (GlobalReligionID != 0) {
            $("#MessageBoxYesNoRE").modal("show");
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    });

    $("#btnYesRE").on('click', function (e) {
        e.preventDefault();
        fnDeleteReligion(GlobalReligionID);
    });

    function fnDeleteReligion() {
        debugger;
        if (GlobalReligionID != 0) {
            $.ajax({
                url: "/FileMaintenance/fnDeleteReligion",
                type: "POST",
                data: "&ReligionID=" + GlobalReligionID,
                success: function (result) {
                    if (result.success) {
                        MessageBox("Successfully Deleted!", "Information", "btn btn-danger", "Close", "bg-primarys");
                        $("#frmReligion").trigger("reset");
                        fnLoadReligionTable();
                    }
                }
            });
        }
        else {
            MessageBox("Select a record first!", "Information", "btn btn-danger", "Ok", "bg-warning");
        }
    }


    /////////////////////////////////////////////////////////////////
    //function fnValidateForm(formIDs) {
    //    if (!$(formIDs).parsley().validate()) {
    //        return false;
    //    }
    //    else {
    //        return true;
    //    }
    //}
});   //end of document

